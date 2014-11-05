using PBServer.managers;
using PBServer.network;
using PBServer.network.clientpacket;
using PBServer.network.Login.packets.clientpacket;
using PBServer.network.serverpackets;
using PBServer.src.managers;
using PBServer.src.model.accounts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace PBServer
{
  public class LoginClient
  {
    public bool networkDebug = true;
    private static bool _isConnectedAviable = true;
    public EndPoint _address;
    public TcpClient _client;
    public NetworkStream _stream;
    private byte[] _buffer;
    private string login;
    private int clan_id;
    private Account _player;

    public int CRYPT_KEY { get; set; }

    static LoginClient()
    {
    }

    public LoginClient(TcpClient tcpClient)
    {
      CLogger.getInstance().info("Cliente conectado.");
      this._client = tcpClient;
      this._stream = tcpClient.GetStream();
      this._address = tcpClient.Client.RemoteEndPoint;
      this._player = new Account();
      new Thread(new ThreadStart(this.init)).Start();
      new Thread(new ThreadStart(this.read)).Start();
    }

    public void setClan(int clan_id)
    {
        this.clan_id = clan_id;
    }

    public int getClan()
    {
        return this.clan_id;
    }

    public int getMyId()
    {
        return this.getPlayer().getPlayerId();
    }

    public void setLogin(string lo)
    {
      this.login = lo;
    }

    public string getLogin()
    {
      return this.login;
    }

    public void setPlayer(Account p)
    {
      this._player = p;
    }

    public Account getPlayer()
    {
      return this._player;
    }

    public void init()
    {
      this.sendPacket((SendBaseLoginPacket) new S_BASE_GET_SCHANNELLIST(this));
    }

    public int getCryptKey()
    {
      return 29890;
    }

    public int getID()
    {
      return 5404;
    }

    public Account restorePlayer(string acc)
    {
      return new DaoManager((GameClient) null).getPlayerInfo(_player.getPlayerId());
    }

    public void sendPacket(SendBaseLoginPacket bp)
    {
      bp.write();
      byte[] numArray1 = bp.ToByteArray();
      short num = Convert.ToInt16(numArray1.Length - 2);
      List<byte> list = new List<byte>(numArray1.Length + 2);
      list.AddRange((IEnumerable<byte>) BitConverter.GetBytes(num));
      list.AddRange((IEnumerable<byte>) numArray1);
      byte[] buffer = list.ToArray();
      byte[] numArray2 = new byte[2]
      {
        buffer[2],
        buffer[3]
      };
      CLogger.getInstance().extra_info(string.Concat(new object[4]
      {
        (object) "Send :",
        (object) buffer.Length,
        (object) "; Opcode: ",
        (object) BitConverter.ToUInt16(numArray2, 0)
      }));
      if (this.networkDebug)
      {
        string[] strArray = BitConverter.ToString(buffer).Split('-', ',', '.', ':', '\t');
        string str1 = "";
        foreach (string str2 in strArray)
          str1 = str1 + "0x" + str2 + " ";
      }
      if (buffer.Length <= 0)
        return;
      this._stream.BeginWrite(buffer, 0, buffer.Length, new AsyncCallback(this.EndSendCallBackStatic), (object) null);
    }

    public void EndSendCallBackStatic(IAsyncResult result)
    {
      try
      {
        this._stream.EndWrite(result);
        this._stream.Flush();
      }
      catch
      {
      }
    }

    public void read()
    {
      try
      {
        if (this._stream == null || !this._stream.CanRead)
          return;
        this._buffer = new byte[2];
        this._stream.BeginRead(this._buffer, 0, 2, new AsyncCallback(this.OnReceiveCallbackStatic), (object) null);
      }
      catch (Exception ex)
      {
        CLogger.getInstance().info("|[LC]| read() Exception: \n" + (object) ex);
        this.close();
      }
    }

    private void OnReceiveCallbackStatic(IAsyncResult result)
    {
      try
      {
        if (this._stream.EndRead(result) <= 0)
          return;
        byte num = this._buffer[0];
        if (this._stream.DataAvailable)
        {
          this._buffer = new byte[(int) num + 2];
          this._stream.BeginRead(this._buffer, 0, (int) num + 2, new AsyncCallback(this.OnReceiveCallback), result.AsyncState);
        }
      }
      catch (Exception ex)
      {
        CLogger.getInstance().warning(string.Concat(new object[4]
        {
          (object) "|[LC]| ",
          (object) this._address,
          (object) " was closed by force: ",
          (object) ex
        }));
        this.close();
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
      Array.Copy((Array) numArray, 0, (Array) data1, 0, data1.Length);
      return LoginClient.decrypt(data1, num);
    }

    public static byte[] decrypt(byte[] data, int shift)
    {
      byte num = data[data.Length - 1];
      for (int index = data.Length - 1; index > 0; --index)
        data[index] = (byte) (((int) data[index - 1] & (int) byte.MaxValue) << 8 - shift | ((int) data[index] & (int) byte.MaxValue) >> shift);
      data[0] = (byte) ((int) num << 8 - shift | ((int) data[0] & (int) byte.MaxValue) >> shift);
      return data;
    }

    public void close()
    {
      if (Config.debug)
        CLogger.getInstance().info("Jogador desconectado.");
      LoginClientManager.getInstance().removeClient(this);
      this._stream.Dispose();
    }

    private void OnReceiveCallback(IAsyncResult result)
    {
      this._stream.EndRead(result);
      byte[] data = new byte[this._buffer.Length];
      this._buffer.CopyTo((Array) data, 0);
      if (data.Length >= 2)
        this.handlePacket(this.decryptC(data, data.Length));
      new Thread(new ThreadStart(this.read)).Start();
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
      List<ReceiveBaseLoginPacket> list = new List<ReceiveBaseLoginPacket>();
      if (!LoginClient._isConnectedAviable)
        return;
      ushort num2 = num1;
        switch (num2)
        {
          case (ushort) 0:
            LoginClient._isConnectedAviable = false;
            goto label_17;
          case (ushort) 2561:
          case (ushort) 2563:
            Thread.Sleep(500);
            list.Add((ReceiveBaseLoginPacket)new PROTOCOL_BASE_LOGIN_WEBKEY_RUSSIA_REQ(this, buff));
            goto label_17;
          case (ushort) 2565:
            list.Add((ReceiveBaseLoginPacket)new PROTOCOL_BASE_GET_MYINFO_REQ(this, buff));
            goto label_17;
          case (ushort) 2567:
            list.Add((ReceiveBaseLoginPacket)new PROTOCOL_BASE_GET_MYFRIENDS_REQ(this, buff));
            goto label_17;
            case (ushort) 2575:
            list.Add((ReceiveBaseLoginPacket)new opcode_2575_REQ(this, buff));
            goto label_17;
          case (ushort) 2577:
            list.Add((ReceiveBaseLoginPacket)new PROTOCOL_BASE_USER_LEAVE_REQ(this, buff));
            goto label_17;
            case (ushort) 2579:
            list.Add((ReceiveBaseLoginPacket)new opcode_2579_REQ(this, buff));
            goto label_17;
          case (ushort) 2661:
            list.Add((ReceiveBaseLoginPacket)new opcode_2660_REQ(this, buff));
            goto label_17;
          case (ushort) 2666:
            list.Add((ReceiveBaseLoginPacket)new opcode_2667_REQ(this, buff));
            goto label_17;
          case (ushort) 2678:
            list.Add((ReceiveBaseLoginPacket)new opcode_2678_REQ(this, buff));
            goto label_17;
          case (ushort) 20746:
            break;
          default:
            goto label_16;
        }
label_16:
      CLogger.getInstance().info_red("|[LC]| Opcode não encontrado: " + (object) num1);
label_17:
      if (list == null || list.ToArray().Length <= 0)
        return;
      foreach (ReceiveBaseLoginPacket receiveBaseLoginPacket in list)
        ThreadManager.runNewThread(new Thread(new ThreadStart(receiveBaseLoginPacket.run)));
    }
  }
}
