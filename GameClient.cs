using PBServer.managers;
using PBServer.network;
using PBServer.network.BattleConnect;
using PBServer.network.clientpacket;
using PBServer.network.Game.packets.clientpackets;
using PBServer.network.serverpackets;
using PBServer.src.data.xml.parsers;
using PBServer.src.managers;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;
using PBServer.src.network.gsPacket.clientpackets;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using PBServer.src.data.xml.holders;

namespace PBServer
{
    public class GameClient
    {
        private static bool cf = true;
        private int _channelID = -1;
        private int player_id = 0;
        private bool _prepareToClose = false;
        public bool networkDebug = true;
        private static bool _isConnectedAviable = true;
        public EndPoint _address;
        public TcpClient _client;
        public NetworkStream _stream;
        private byte[] _buffer;
        private string IPClient;

        public int CRYPT_KEY { get; set; }

        static GameClient()
        {
        }

        public GameClient(TcpClient tcpClient)
        {
            CLogger.getInstance().info("Cliente conectado. ");
            this._client = tcpClient;
            this._stream = tcpClient.GetStream();
            this._address = tcpClient.Client.RemoteEndPoint;
            this._stream.ReadTimeout = 10000;
            this.IPClient = this._address.ToString();
            new Thread(new ThreadStart(this.init)).Start();
            new Thread(new ThreadStart(this.read)).Start();
        }

        ~GameClient()
        {
            this.close();
        }

        public void setAccount(int playerid)
        {
            this.player_id = playerid;
        }

        public Account getPlayer()
        {
            return AccountManager.getInstance().getAccountInObjectId(this.player_id) ?? (Account)null;
        }

        public void form()
        {
            if (!GameClient.cf || Directory.Exists("unks_game"))
                return;
            Directory.CreateDirectory("unks_game");
        }

        public int getPlayerId()
        {
            return this.player_id;
        }

        public void setChannelId(int id)
        {
            CLogger.getInstance().warning("O jogador " + this.getPlayer().getPlayerName() + " entrou no canal: " + id.ToString());
            this._channelID = id;
        }

        public int getChannelId()
        {
            return this._channelID;
        }

        public void init()
        {
            this.sendPacket((SendBaseGamePacket)new S_BASE_GET_CHANNELLIST(this));
        }

        public string getIPString()
        {
            return IPAddress.Parse(this.IPClient.Split(new char[1]
      {
        ':'
      })[0]).ToString();
        }

        public int getCryptKey()
        {
            return 29890;
        }

        public int getID()
        {
            return 5404;
        }

        public Account restoreAccount(string acc)
        {
            Account account = AccountManager.getInstance().get(acc);
            if (account == null)
                return (Account)null;
            account.setConnected(true);
            this.setAccount(account.player_id);
            return account;
        }

        public void sendPacket(SendBaseGamePacket bp)
        {
            try
            {
                if (this._prepareToClose || this._stream == null)
                    return;
                bp.write();
                byte[] numArray1 = bp.ToByteArray();
                short num = Convert.ToInt16(numArray1.Length - 2);
                List<byte> list = new List<byte>(numArray1.Length + 2);
                list.AddRange((IEnumerable<byte>)BitConverter.GetBytes(num));
                list.AddRange((IEnumerable<byte>)numArray1);
                byte[] buffer = list.ToArray();
                byte[] numArray2 = new byte[2]
        {
          buffer[2],
          buffer[3]
        };
                if (this.networkDebug)
                {
                    ;
                }
                if (buffer.Length > 0)
                    this._stream.BeginWrite(buffer, 0, buffer.Length, new AsyncCallback(this.EndSendStaticPacket), (object)null);
            }
            catch (Exception ex)
            {
                CLogger.getInstance().info("[GAME]: read() Exception: \n" + (object)ex);
                this.close();
            }
        }

        public void EndSendStaticPacket(IAsyncResult result)
        {
            try
            {
                this._stream.EndWrite(result);
            }
            catch
            {
            }
        }

        public void read()
        {
            try
            {
                if (this._prepareToClose || (this._stream == null || !this._client.Connected || !this._stream.CanRead))
                    return;
                this._buffer = new byte[2];
                this._stream.BeginRead(this._buffer, 0, 2, new AsyncCallback(this.OnReceiveCallbackStatic), (object)null);
            }
            catch (Exception ex)
            {
                CLogger.getInstance().info("[GAME]: read() Exception: \n" + (object)ex);
                this.close();
            }
        }

        private void OnReceiveCallbackStatic(IAsyncResult result)
        {
            try
            {
                if (this._prepareToClose || this._stream == null || this._stream.EndRead(result) <= 0)
                    return;
                byte num = this._buffer[0];
                if (this._stream.DataAvailable)
                {
                    this._buffer = new byte[(int)num + 2];
                    this._stream.BeginRead(this._buffer, 0, (int)num + 2, new AsyncCallback(this.OnReceiveCallback), result.AsyncState);
                }
            }
            catch (Exception ex)
            {
                CLogger.getInstance().warning(string.Concat(new object[4]
        {
          (object) "[GAME]: ",
          (object) this._address,
          (object) " was closed by force: ",
          (object) ex
        }));
                this.close();
            }
        }

        public void close()
        {
            this._prepareToClose = true;
            try
            {
                AccountManager.getInstance().get(this.getPlayer().name).setOnlineStatus(false);
                if (this.getPlayer() != null)
                {
                    if (this.getPlayer().getRoom() != null)
                    {
                        UdpHandler.getInstance().RemovePlayerInRoom(this.getPlayer());
                        ChannelInfoHolder.getChannel(this.getChannelId()).getRooms()[this.getPlayer().getRoom().getRoomId()].removePlayer(this.getPlayer());
                        this.getPlayer().setRoom((Room)null);
                    }
                    if (this.getChannelId() >= 0)
                        ChannelInfoHolder.getChannel(this.getChannelId()).removePlayer(this.getPlayer());
                }
                AccountManager.getInstance().get(this.getPlayer().name).setClient((GameClient)null);
                GameClientManager.getInstance().removeClient(this);
                if (this._client.Connected)
                {
                    this._client.Close();
                    this._stream = (NetworkStream)null;
                }
                CLogger.getInstance().info("Jogador desconectado.");
            }
            catch (Exception ex)
            {
                CLogger.getInstance().warning(ex.ToString());
            }
        }

        private void OnReceiveCallback(IAsyncResult result)
        {
            try
            {
                this._stream.EndRead(result);
                byte[] data = new byte[this._buffer.Length];
                this._buffer.CopyTo((Array)data, 0);
                if (data.Length >= 2)
                    this.handlePacket(this.decryptC(data, data.Length));
                new Thread(new ThreadStart(this.read)).Start();
            }
            catch
            {
            }
        }

        public void setShift(int key)
        {
            this.CRYPT_KEY = key;
        }

        public int getShift()
        {
            return this.CRYPT_KEY;
        }

        public byte[] decryptC(byte[] data, int length)
        {
            int id = this.getID();
            int cryptKey = this.getCryptKey();
            int num = this.getShift();
            if (num <= 0)
            {
                num = (id + cryptKey) % 7 + 1;
                this.setShift(num);
            }
            byte[] numArray = data;
            byte[] data1 = new byte[data.Length];
            Array.Copy((Array)numArray, 0, (Array)data1, 0, data1.Length);
            return GameClient.decrypt(data1, num);
        }

        public static byte[] decrypt(byte[] data, int shift)
        {
            byte num = data[data.Length - 1];
            for (int index = data.Length - 1; index > 0; --index)
                data[index] = (byte)(((int)data[index - 1] & (int)byte.MaxValue) << 8 - shift | ((int)data[index] & (int)byte.MaxValue) >> shift);
            data[0] = (byte)((int)num << 8 - shift | ((int)data[0] & (int)byte.MaxValue) >> shift);
            return data;
        }

        private void handlePacket(byte[] buff)
        {
            ushort num1 = BitConverter.ToUInt16(new byte[2]
      {
        buff[0],
        buff[1]
      }, 0);
            BitConverter.ToString(buff).Replace("-", " ");
            if (this.networkDebug)
            {
                string[] strArray = BitConverter.ToString(buff).Split('-', ',', '.', ':', '\t');
                string str1 = "";
                foreach (string str2 in strArray)
                    str1 = str1 + "0x" + str2 + " ";
            }
            List<ReceiveBaseGamePacket> list = new List<ReceiveBaseGamePacket>();
            if (!GameClient._isConnectedAviable)
                return;
            ushort num2 = num1;
            if ((uint)num2 <= 10000U)
            {
                switch (num2)
                {
                    case (ushort)0xA13:
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_BASE_USER_ENTER_REQ(this, buff));
                        goto label_67;
                    case (ushort)0x212: //530
                        list.Add((ReceiveBaseGamePacket)new opcode_SHOP_BUY_REQ(this, buff));
                        goto label_67;
                    case (ushort)0xC07: //3079
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_LOBBY_ENTER_REQ(this, buff));
                        goto label_67;
                    case (ushort)0xC0B: //3079
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_LOBBY_LEAVE_REQ(this, buff));
                        goto label_67;
                    case (ushort)0xC01: //3079
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_LOBBY_GET_ROOMLIST_REQ(this, buff));
                        goto label_67;
                    case (ushort)0xC1D: //3101
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_LOBBY_CREATE_NICK_NAME_REQ(this, buff));
                        goto label_67;
                    case (ushort)0xF16: //3079
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_BASE_ENTER_PROFILE_REQ(this, buff));
                        goto label_67;
                    case (ushort)0xA0D: //3079
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_REQ(this, buff));
                        goto label_67;
                    case (ushort)0xB03: //2819
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_SHOP_ENTER_REQ(this, buff));
                        goto label_67;
                    case (ushort)2818:
                    case (ushort)2820:
                        break;
                    case (ushort)0xB01: //2817
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_SHOP_LEAVE_REQ(this, buff));
                        goto label_67;

                    case (ushort)0xB05: //2821
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_LOBBY_SHOP_LIST_REQ(this, buff));
                        new PROTOCOL_LOBBY_SHOP_LIST_REQ(this, buff).run();
                        goto label_67;

                    case (ushort)0xA3B: //2619
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_BASE_USER_TITLE_GET_REQ(this, buff));
                        goto label_67;
                    case (ushort)0xA3D: //2621
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_BASE_USER_TITLE_USE_REQ(this, buff));
                        goto label_67;
                    case (ushort)0xA3F: //2623
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_BASE_USER_TITLE_DETACH_REQ(this, buff));
                        goto label_67;

                    case (ushort)0xA0B: //2571
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_BASE_ENTER_CHANNELSELECT_REQ(this, buff));
                        goto label_67;

                    case (ushort)0xE01: //3585
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_INVENTORY_ENTER_REQ(this, buff));
                        goto label_67;
                    case (ushort)0xE05: //3589
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_INVENTORY_LEAVE_REQ(this, buff));
                        goto label_67;

                    case (ushort)0x5A1:
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_CS_CLAN_LOGIN_REQ(this, buff));
                        goto label_67;
                    case (ushort)0x5A3:
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_CS_CLAN_LOGOUT_REQ(this, buff));
                        goto label_67;
                    case (ushort)0x5A5:
                        list.Add((ReceiveBaseGamePacket)new opcode_1446_REQ(this, buff));
                        goto label_67;
                    case (ushort)0x5AB:
                        list.Add((ReceiveBaseGamePacket)new opcode_1446_REQ(this, buff));
                        goto label_67;
                    case (ushort)0xB55:
                        list.Add((ReceiveBaseGamePacket)new opcode_2901_REQ(this, buff));
                        goto label_67;
                    case (ushort)2627:
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_LOBBY_CHATTING_REQ(this, buff));
                        goto label_67;
                    case (ushort)0xB51:
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_OUTPOST_ENTER_REQ(this, buff));
                        goto label_67;
                    case (ushort)0xB53:
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_OUTPOST_LEAVE_REQ(this, buff));
                        goto label_67;
                    case (ushort)0xC11:
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_LOBBY_CREATE_ROOM_REQ(this, buff));
                        goto label_67;
                    case (ushort)2654:
                        list.Add((ReceiveBaseGamePacket)new opcode_2653_REQ(this, buff));
                        goto label_67;
                    case (ushort)534:
                        list.Add((ReceiveBaseGamePacket)new opcode_534_REQ(this, buff));
                        goto label_67;
                    case (ushort)3864:
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_BASE_LEAVE_PROFILE_REQ(this, buff));
                        goto label_67;
                    case (ushort)3845:
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_ROOM_CHANGE_TEAM_REQ(this, buff));
                        goto label_67;
                    //case (ushort)3904:
                    //    list.Add((ReceiveBaseGamePacket)new opcode_3650_REQ(this, buff));
                    //    goto label_67;
                    case (ushort)1416:
                        list.Add((ReceiveBaseGamePacket)new opcode_1416_REQ(this, buff));
                        goto label_67;
                    case (ushort)1447:
                        list.Add((ReceiveBaseGamePacket)new opcode_1447_REQ(this, buff));
                        goto label_67;
                    case (ushort)1310:
                        list.Add((ReceiveBaseGamePacket)new opcode_1310_REQ(this, buff));
                        goto label_67;
                    case (ushort)3854:
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_ROOM_GET_LOBBY_USER_LIST_REQ(this, buff));
                        goto label_67;
                    //case (ushort)3906:
                    //    list.Add((ReceiveBaseGamePacket)new opcode_3593_REQ(this, buff));
                    //    goto label_67;
                    case (ushort)2581:
                        list.Add((ReceiveBaseGamePacket)new opcode_2581_REQ(this, buff));
                        goto label_67;
                    case (ushort)1304:
                        list.Add((ReceiveBaseGamePacket)new opcode_1304_REQ(this, buff));
                        goto label_67;
                    case (ushort)3884:
                        list.Add((ReceiveBaseGamePacket)new CM_INVITE_ROOM_RETURN(this, buff));
                        goto label_67;
                    case (ushort)2605:
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_BASE_USER_MISSION_BUY_REQ(this, buff));
                        goto label_67;
                    case (ushort)3081:
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_LOBBY_JOIN_ROOM_REQ(this, buff));
                        goto label_67;
                    case (ushort)3087:
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_LOBBY_GET_ROOMINFO_REQ(this, buff));
                        goto label_67;
                    case (ushort)3849:
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_ROOM_CLOSE_SLOT_REQ(this, buff));
                        goto label_67;
                    case (ushort)1362:
                        list.Add((ReceiveBaseGamePacket)new CM_CLAN_SAVEINFO1(this, buff));
                        goto label_67;
                    case (ushort)1364:
                        list.Add((ReceiveBaseGamePacket)new CM_CLAN_SAVEINFO2(this, buff));
                        goto label_67;
                    case (ushort)548:
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_CUPON_CHANGE_PLAYER_NAME_REQ(this, buff));
                        goto label_67;
                    case (ushort)1360:
                        list.Add((ReceiveBaseGamePacket)new opcode_1360_REQ(this, buff));
                        goto label_67;
                    case (ushort)1320:
                        list.Add((ReceiveBaseGamePacket)new opcode_1320_REQ(this, buff));
                        goto label_67;
                    case (ushort)1306:
                        list.Add((ReceiveBaseGamePacket)new opcode_1306_REQ(this, buff));
                        goto label_67;
                    case (ushort)282:
                        list.Add((ReceiveBaseGamePacket)new CM_FRIEND_INVITE(this, buff));
                        goto label_67;
                    case (ushort)284:
                        list.Add((ReceiveBaseGamePacket)new CM_FRIEND_REMOVE(this, buff));
                        goto label_67;
                    case (ushort)536:
                        list.Add((ReceiveBaseGamePacket)new opcode_536_REQ(this, buff));
                        goto label_67;
                    case (ushort)0xD03:
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_BATTLE_READYBATTLE_REQ(this, buff));
                        goto label_67;
                    case (ushort)417:
                        list.Add((ReceiveBaseGamePacket)new opcode_417_REQ(this, buff));
                        goto label_67;
                    case (ushort)290:
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_AUTH_SEND_WHISPER_REQ(this, buff));
                        goto label_67;
                    case (ushort)1312:
                        list.Add((ReceiveBaseGamePacket)new opcode_1312_REQ(this, buff));
                        goto label_67;
                    case (ushort)3344:
                        list.Add((ReceiveBaseGamePacket)new opcode_3860_REQ(this, buff));
                        goto label_67;
                    case (ushort)3348:
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_BATTLE_PRESTARTBATTLE_REQ(this, buff));
                        goto label_67;
                    case (ushort)3904:
                        list.Add((ReceiveBaseGamePacket)new opcode_3650_REQ(this, buff));
                        goto label_67;
                    case (ushort)3384:
                        list.Add((ReceiveBaseGamePacket)new PROTOCOL_BATTLE_ENDBATTLE_REQ(this, buff));
                        goto label_67;
                }
            }
            CLogger.getInstance().info_red("|[GC]| Opcode não encontrado " + (object)num1);
        label_67:
            if (list == null || list.ToArray().Length <= 0)
                return;
            foreach (ReceiveBaseGamePacket receiveBaseGamePacket in list)
                ThreadManager.runNewThread(new Thread(new ThreadStart(receiveBaseGamePacket.run)));
        }
    }
}
