// Type: PBServer.src.model.rooms.Room
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.data.xml.holders;
using PBServer.model.ENUMS;
using PBServer.network.Game.packets.serverpackets;
using PBServer.src.data.xml.parsers;
using PBServer.src.managers;
using PBServer.src.model.accounts;
using PBServer.src.network.Game.packets.serverpackets;
using PBServer.src.network.gsPacket.serverpackets;
using System;
using System.Collections.Generic;
using PBServer.src.data.xml.holders;

namespace PBServer.src.model.rooms
{
    public class Room
    {
        private Chat chat;
        public int GMTimeOut = -1;
        public int refreshCheck = 0;
        public int server_type = 1;
        private SLOT[] _slots = new SLOT[16];
        private bool _isInFight = true;
        private int _redKills = 0;
        private int _redDeaths = 0;
        private int _blueKills = 0;
        private int _blueDeaths = 0;
        public int _aiCount = 1;
        public int _aiLevel = 0;
        public int bomb = 5;
        public static int NET_ROOM_NAME_SIZE = 23;
        public static int NET_ROOM_PW = 4;
        private static int[] TIMES = new int[9]
    {
      3,
      5,
      7,
      5,
      10,
      15,
      20,
      25,
      30
    };
        private static int[] KILLS = new int[6]
    {
      60,
      80,
      100,
      120,
      140,
      160
    };
        private static int[] ROUNDS = new int[5]
    {
      0,
      3,
      5,
      7,
      9
    };
    public int[] RED_TEAM = new int[]{0, 2, 4, 6, 8, 10, 12, 14};
	public int[] BLUE_TEAM = new int[]{1, 3, 5, 7, 9, 11, 13, 15};
        public string name;
        public string password;
        public int map_id;
        public string map_name;
        public int stage4v4;
        public int allweapons;
        public int limit;
        public int seeConf;
        public int autobalans;
        public int killtime;
        public int room_type;
        public int special;
        public int random_map;
        public int unkc1;
        public int unkc2;
        private int _roomId;
        public int _channelId;
        private int _timeRoom;
        private int _leader;
        private ROOM_STATE _room_state;

        static Room()
        {
        }

        public Room(int roomId, int channelId)
        {
            this.chat = new Chat();
            this._roomId = roomId;
            for (int index = 0; index < this._slots.Length; ++index)
                this._slots[index] = new SLOT();
            this._room_state = ROOM_STATE.ROOM_STATE_READY;
            this._channelId = channelId;
        }

        public int getRoomId()
        {
            return this._roomId;
        }

        public void CalculateBattleResult(Account ac)
        {
            for (int index = 0; index < 16; ++index)
            {
                Account playerBySlot = this.getPlayerBySlot(index);
                if (playerBySlot != null)
                {
                    int num1;
                    int num2;
                    if (Config.TrainigExpEnable)
                    {
                        num1 = playerBySlot.getRoom().getSlots()[playerBySlot.getSlot()].allKills * 25;
                        num2 = playerBySlot.getRoom().getSlots()[playerBySlot.getSlot()].allKills * 50;
                    }
                    else
                    {
                        num1 = playerBySlot.getRoom().getSlots()[playerBySlot.getSlot()].allKills * 8;
                        num2 = playerBySlot.getRoom().getSlots()[playerBySlot.getSlot()].allKills;
                    }
                    playerBySlot.setExp(playerBySlot.getExp() + num2);
                    playerBySlot.setGP(playerBySlot.getGP() + num1);
                    this.getSlot(index).gp = num1;
                    this.getSlot(index).exp = num2;
                    playerBySlot._statistic.setKills(playerBySlot.getRoom().getSlot(playerBySlot.getSlot()).allKills, true);
                    playerBySlot._statistic.setDeaths(playerBySlot.getRoom().getSlot(playerBySlot.getSlot()).allDeaths, true);
                    int num3 = RankExpInfoHolder.getRankExpInfo(playerBySlot.getRank())._onAllExp;
                    int num4 = RankExpInfoHolder.getRankExpInfo(playerBySlot.getRank())._onGPUp;
                    int itemid = RankExpInfoHolder.getRankExpInfo(playerBySlot.getRank())._itemid;
                    if (playerBySlot.getExp() >= num3 && playerBySlot.getRank() < 51)
                    {
                        playerBySlot.setRank(playerBySlot.getRank() + 1);
                        playerBySlot.setGP(playerBySlot.getGP() + num4);
                        playerBySlot.sendPacket((SendBaseGamePacket)new opcode_2614_ACK(playerBySlot.getRank(), itemid));
                        CLogger.getInstance().extra_info("O jogador " + playerBySlot.getPlayerName() + " upou de nível!");
                    }
                    AccountManager.getInstance().updatePlayer(playerBySlot);
                }
            }
            this.updateInfo();
        }

        public int isBattleInt()
        {
            return this._room_state > ROOM_STATE.ROOM_STATE_COUNTDOWN ? 1 : 0;
        }

        public int isPasswordInt()
        {
            return this.password.Length > 0 ? 1 : 0;
        }

        public ROOM_STATE getState()
        {
            return this._room_state;
        }

        public void CMDChangeMap(int map, Account acc)
        {
            this.map_id = map;
            if (map != 18)
                this.chat.playername = "|[SERVER]|";
                this.chat.chat = "Mapa mudado para Training Camp";

            if (map != 47)
                this.chat.playername = "|[SERVER]|";
                this.chat.chat = "Mapa mudado para Dragon Alley";

            if (map != 57)
                this.chat.playername = "|[SERVER]|";
                this.chat.chat = "Mapa mudado para Angkor Ruins";

            if (map != 60)
                this.chat.playername = "|[SERVER]|";
                this.chat.chat = "Mapa mudado para Ghost Town";

            if (map != 61)
                this.chat.playername = "|[SERVER]|";
                this.chat.chat = "Mapa mudado para Grand Bazaar";

            if (map != 62)
                this.chat.playername = "|[SERVER]|";
                this.chat.chat = "Mapa mudado para Dino Lab";

            CLogger.getInstance().extra_info("|[Room]| Mapa " + map.ToString() + " | Jogador " + acc.getPlayerName());
            this.SendRoomInfo();
        }

        public void SendRoomInfo()
        {
            for (int slot = 0; slot < 15; ++slot)
            {
                Account playerBySlot = this.getPlayerBySlot(slot);
                if (playerBySlot != null)
                    playerBySlot.sendPacket((SendBaseGamePacket)new S_BATTLE_ROOMINFO(this));
            }
        }

        public void setState(ROOM_STATE _state)
        {
            this._room_state = _state;
        }

        public bool getStateBattle()
        {
            return this._room_state != ROOM_STATE.ROOM_STATE_READY;
        }

        public void initSlotCount(int count)
        {
            if (count == 0)
                ++count;
            for (int index = 0; index < this._slots.Length; ++index)
            {
                if (index >= count)
                    this._slots[index].state = SLOT_STATE.SLOT_STATE_CLOSE;
            }
        }

        public int getSlotCount()
        {
            int num = 0;
            for (int index = 0; index < this._slots.Length; ++index)
            {
                if (this._slots[index].state != SLOT_STATE.SLOT_STATE_CLOSE)
                    ++num;
            }
            return num;
        }

        public void setNewSlot(Account player, int team)
        {
            int[] numArray;
            if (team == 0)
                numArray = new int[8]
        {
          0,
          2,
          4,
          6,
          8,
          10,
          12,
          14
        };
            else
                numArray = new int[8]
        {
          1,
          3,
          5,
          7,
          9,
          11,
          13,
          15
        };
            int slot = player.getSlot();
            int num = slot % 8;
            if (team == num)
                return;
            foreach (int index in numArray)
            {
                if (this._slots[index]._playerId == 0 && this._slots[index].state == SLOT_STATE.SLOT_STATE_EMPTY)
                {
                    this.changeSlotState(index, SLOT_STATE.SLOT_STATE_NORMAL, false);
                    this._slots[index]._playerId = player.player_id;
                    player.setSlot(index);
                    if (slot == this._leader)
                    {
                        this._leader = index;
                        break;
                    }
                    else
                        break;
                }
            }
            this.changeSlotState(slot, SLOT_STATE.SLOT_STATE_EMPTY, false);
            this.updateInfo();
        }

        public SLOT_STATE getSlotState(int slot)
        {
            return this._slots[slot].state;
        }

        public SLOT getSlotPlayerId(int playerId)
        {
            for (int index = 0; index < 16; ++index)
            {
                if (this._slots[index]._playerId == playerId)
                    return this._slots[index];
            }
            return (SLOT)null;
        }

        public void changeSlotState(int slot, SLOT_STATE state, bool sendinfo)
        {
            this._slots[slot].state = state;
            if (state == SLOT_STATE.SLOT_STATE_EMPTY || state == SLOT_STATE.SLOT_STATE_CLOSE)
            {
                this._slots[slot].allDeaths = 0;
                this._slots[slot].allKills = 0;
                this._slots[slot]._playerId = 0;
            }
            if (!sendinfo)
                return;
            this.updateInfo();
        }

        public int getDeaths(TeamEnum t)
        {
            switch (t)
            {
                case TeamEnum.CHARACTER_TEAM_RED:
                    return this._redDeaths;
                case TeamEnum.CHARACTER_TEAM_BLUE:
                    return this._blueDeaths;
                default:
                    return 0;
            }
        }

        public int getKills(TeamEnum t)
        {
            switch (t)
            {
                case TeamEnum.CHARACTER_TEAM_RED:
                    return this._redKills;
                case TeamEnum.CHARACTER_TEAM_BLUE:
                    return this._blueKills;
                default:
                    return 0;
            }
        }

        public List<Account> getPlayerInRoom(GameClient client)
        {
            List<Account> list = new List<Account>();
            for (int id = 0; id < 15; ++id)
            {
                int playerId = this.getSlot(id)._playerId;
                if (playerId > 0)
                {
                    Account playerFromPlayerId = ChannelInfoHolder.getChannel(client.getChannelId()).getPlayerFromPlayerId(playerId);
                    list.Add(playerFromPlayerId);
                }
            }
            return list;
        }

        public Account getPlayerBySlot(int slot)
        {
            int num = this._slots[slot]._playerId;
            for (int index = 0; index < this.getAllPlayers().Count; ++index)
            {
                if (num == this.getAllPlayers()[index].player_id)
                    return this.getAllPlayers()[index];
            }
            return (Account)null;
        }

        public int getTimeByMask()
        {
            return Room.TIMES[this.killtime >> 4];
        }

        public int getKillsByMask()
        {
            if (this.killtime >> 4 < 3)
                return Room.ROUNDS[this.killtime & 15];
            else
                return Room.KILLS[this.killtime & 15];
        }

        public void updateInfo()
        {
            foreach (Account objId in this.getAllPlayers())
            {
                Account pl = AccountManager.getInstance().get(objId.getLogin());
                if (pl != null)
                {
                    pl.sendPacket(new S_SLOT_INFO(this));
                }
            }
            //foreach (Account account in this.getAllPlayers().ToArray())
            //{
            //    if (account != null)
            //    {
            //        account.sendPacket(new S_SLOT_INFO(this));
            //    }
            //}
        }

        public void setTimeLost(int time)
        {
            this._timeRoom = time;
        }

        public int getTimeLost()
        {
            return this._timeRoom;
        }

        public void setLeader(int slotId)
        {
            this._leader = slotId;
        }

        public Account getLeader()
        {
            if (this.getAllPlayers().Count <= 0)
                return (Account)null;
            if (this._leader == -1)
                this.setNewLeader(-1);
            return ChannelInfoHolder.getChannel(this._channelId).getPlayerFromPlayerId(this.getSlot(this._leader)._playerId);
        }

        public void setNewLeader(int newLeader)
        {
            if (newLeader == -1)
            {
                for (int index = 0; index < 15; ++index)
                {
                    if (this.getSlot(index)._playerId > 0)
                        this.setLeader(index);
                }
            }
            else
                this.setLeader(newLeader);
            this.updateInfo();
        }

        public SLOT changeTeam(Account player, int team)
        {
            SLOT slot = this.getSlot(player.getSlot());
            foreach (int teamSlot in team == 0 ? RED_TEAM : BLUE_TEAM)
            {
                foreach (SLOT rslot in new SLOT[teamSlot])
                {
                    //RoomSlot rslot = ROOM_SLOT[teamSlot];
                       // if (player.equals(rslot.getPlayer()) || rslot.state == SLOT_STATE.SLOT_STATE_EMPTY)
                      //  {
                      //      slot.setPlayer(null);
                      //      slot.state = SLOT_STATE.SLOT_STATE_EMPTY;
                      //      rslot.setPlayer(player);
                      //      rslot.state = SLOT_STATE.SLOT_STATE_NORMAL;
                      //      return rslot;
                      //  }
                }
            }
            return null;
        }

        public void setInFight(bool b)
        {
            this._isInFight = b;
        }

        public bool isInFight()
        {
            return this._isInFight;
        }

        public void removePlayer(Account player)
        {
            int num = player.player_id;
            if (this == null)
                return;
            if (this.getAllPlayers().Count > 1 && this.getLeader().player_id == player.player_id)
                this.setNewLeader(-1);
            int slot = player.getSlot();
            if (this.getSlot(slot).state == SLOT_STATE.SLOT_STATE_BATTLE)
            {
                for (int index = 0; index < 15; ++index)
                {
                    if (this.getSlot(index)._playerId > 0 && this.getSlot(index)._playerId != player.player_id)
                        this.getPlayerBySlot(index).sendPacket((SendBaseGamePacket)new opcode_3850_ACK(slot));
                }
            }
            for (int index = 0; index < this._slots.Length; ++index)
            {
                try
                {
                    if (this._slots[index]._playerId > 0 && this._slots[index]._playerId == player.player_id)
                    {
                        this._slots[index].playername = "";
                        this._slots[index].botScore = 0;
                        this._slots[index]._playerId = 0;
                        this._slots[index].state = SLOT_STATE.SLOT_STATE_EMPTY;
                        this._slots[index].setPlayer(null);
                        player.setRoom((Room)null);
                        player.setSlot(-1);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    CLogger.getInstance().warning(ex.ToString());
                }
            }
            this.updateInfo();
        }

        public SLOT[] getSlots()
        {
            return this._slots;
        }

        public SLOT getSlot(int id)
        {
            return this._slots[id];
        }

        public int addPlayer(Account player)
        {
            int player_id = player.player_id;
            int result;
            for (int i = 0; i < 15; i++)
            {
                if (this._slots[i]._playerId == 0 && this._slots[i].state == SLOT_STATE.SLOT_STATE_EMPTY)
                {
                    this._slots[i].setId(i);
                    this._slots[i]._playerId = player_id;
                    this._slots[i].playername = player.getPlayerName();
                    this._slots[i].state = SLOT_STATE.SLOT_STATE_NORMAL;
                    this._slots[i].setPlayer(player);
                    player.setRoom(this);
                    player.setSlot(i);
                    this.updateInfo();
                    result = i;
                    return result;
                }
            }
            result = -1;
            return result;
        }

        public List<Account> getAllPlayers()
        {
            List<Account> list = new List<Account>();
            for (int index = 0; index < this._slots.Length; ++index)
            {
                if (this._slots[index]._playerId > 0)
                {
                    Account playerFromPlayerId = ChannelInfoHolder.getChannel(this._channelId).getPlayerFromPlayerId(this._slots[index]._playerId);
                    list.Add(playerFromPlayerId);
                }
            }
            return list;
        }

        public void ChangeRoomState(ROOM_STATE room, Account p)
        {
            if (this.getState() == ROOM_STATE.ROOM_STATE_PRE_BATTLE)
            {
                if (p.player_id == this.getLeader().player_id)
                {
                    this._aiLevel = 1;
                    this._redKills = 0;
                    this._redDeaths = 0;
                    this._blueKills = 0;
                    this._blueDeaths = 0;
                    this._timeRoom = this.GMTimeOut != -1 ? this.GMTimeOut : this.getTimeByMask() * 60;
                }
                for (int slot = 0; slot < 15; ++slot)
                {
                    Account playerBySlot = this.getPlayerBySlot(slot);
                    if (this._slots[slot].state == SLOT_STATE.SLOT_STATE_BATTLE_READY && this._slots[slot]._playerId > 0 && playerBySlot != null)
                    {
                        this.changeSlotState(slot, SLOT_STATE.SLOT_STATE_BATTLE, true);
                        p.sendPacket((SendBaseGamePacket)new opcode_3879_ACK(this));
                        CLogger.getInstance().info("Send: PROTOCOL_BATTLE_STARTBATTLE_ACK info_player=" + playerBySlot.getPlayerName() + " send=" + playerBySlot.getPlayerName());
                        playerBySlot.sendPacket((SendBaseGamePacket)new S_BATTLE_STARTBATTLE(playerBySlot));
                        if (p.player_id != playerBySlot.player_id)
                        {
                            CLogger.getInstance().info("Send: PROTOCOL_BATTLE_STARTBATTLE_ACK info_player=" + p.getPlayerName() + " send=" + playerBySlot.player_name);
                            playerBySlot.sendPacket((SendBaseGamePacket)new S_BATTLE_STARTBATTLE(p));
                            CLogger.getInstance().info("Send: PROTOCOL_BATTLE_STARTBATTLE_ACK info_player=" + playerBySlot.getPlayerName() + " send=" + p.player_name);
                            p.sendPacket((SendBaseGamePacket)new S_BATTLE_STARTBATTLE(playerBySlot));
                        }
                        playerBySlot.sendPacket((SendBaseGamePacket)new S_STOP_BATTLE());
                        playerBySlot.sendPacket((SendBaseGamePacket)new S_START_BATTLE(this));
                    }
                }
                this.setState(ROOM_STATE.ROOM_STATE_BATTLE);
                this.updateInfo();
            }
            else
            {
                if (this.getState() != ROOM_STATE.ROOM_STATE_BATTLE || p.getRoom().getSlotState(p.getSlot()) != SLOT_STATE.SLOT_STATE_BATTLE_READY)
                    return;
                this.changeSlotState(p.getSlot(), SLOT_STATE.SLOT_STATE_BATTLE, true);
                CLogger.getInstance().info("Send: PROTOCOL_BATTLE_STARTBATTLE_ACK info_player=" + p.getPlayerName() + " send=" + p.getPlayerName());
                p.sendPacket((SendBaseGamePacket)new S_BATTLE_STARTBATTLE(p));
                for (int slot = 0; slot < 15; ++slot)
                {
                    Account playerBySlot = this.getPlayerBySlot(slot);
                    if (this._slots[slot].state == SLOT_STATE.SLOT_STATE_BATTLE_READY && this._slots[slot]._playerId > 0)
                    {
                        if (p.player_id != playerBySlot.player_id)
                        {
                            CLogger.getInstance().info("Send: PROTOCOL_BATTLE_STARTBATTLE_ACK info_player=" + p.getPlayerName() + " send=" + playerBySlot.player_name);
                            playerBySlot.sendPacket((SendBaseGamePacket)new S_BATTLE_STARTBATTLE(p));
                        }
                        CLogger.getInstance().info("Send: PROTOCOL_BATTLE_STARTBATTLE_ACK info_player=" + playerBySlot.getPlayerName() + " send=" + p.player_name);
                        p.sendPacket((SendBaseGamePacket)new S_BATTLE_STARTBATTLE(playerBySlot));
                    }
                }
                this.updateInfo();
                p.sendPacket((SendBaseGamePacket)new S_STOP_BATTLE());
                p.sendPacket((SendBaseGamePacket)new S_START_BATTLE(this));
            }
        }

        public void stopBattle(Account p)
        {
            if (p.getSlot() == this._leader)
            {
                this._aiLevel = 1;
                this._redKills = 0;
                this._redDeaths = 0;
                this._blueKills = 0;
                this._blueDeaths = 0;
                this._timeRoom = this.getTimeByMask() * 60;
                this.setState(ROOM_STATE.ROOM_STATE_READY);
                for (int slot = 0; slot < 15; ++slot)
                {
                    if (this._slots[slot].state == SLOT_STATE.SLOT_STATE_BATTLE || this._slots[slot].state == SLOT_STATE.SLOT_STATE_PRESTART)
                        this.changeSlotState(slot, SLOT_STATE.SLOT_STATE_NORMAL, true);
                    this._slots[slot].allDeaths = 0;
                    this._slots[slot].allKills = 0;
                    this._slots[slot].killMessage = 0;
                    this._slots[slot].killsOnLife = 0;
                    this._slots[slot].lastKillState = 0;
                    this._slots[slot].repeatLastState = false;
                }
            }
            this.updateInfo();
        }

        public void addDeaths(TeamEnum t)
        {
            this.addDeaths(t, 1);
        }

        public void addDeaths(TeamEnum t, int value)
        {
            switch (t)
            {
                case TeamEnum.CHARACTER_TEAM_RED:
                    this._redDeaths += value;
                    break;
                case TeamEnum.CHARACTER_TEAM_BLUE:
                    this._blueDeaths += value;
                    break;
            }
        }

        public void addKills(TeamEnum t)
        {
            this.addKills(t, 1);
        }

        public void addKills(TeamEnum t, int value)
        {
            switch (t)
            {
                case TeamEnum.CHARACTER_TEAM_RED:
                    this._redKills += value;
                    break;
                case TeamEnum.CHARACTER_TEAM_BLUE:
                    this._blueKills += value;
                    break;
            }
        }
    }
}
