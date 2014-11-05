// Type: PBServer.src.model.accounts.Account
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.model.players;
using PBServer.src.managers;
using PBServer.src.model;
using PBServer.src.model.rooms;
using System.Collections.Generic;
using PBServer.model.clans;
using System.Net;
using PBServer.managers;
using PBServer.src.model.friends;

namespace PBServer.src.model.accounts
{
  public class Account
  {
    private byte[] addr = new byte[4];
    private int _slotId = -1;
    private bool _online = false;
    private bool _isConnected = true;
    public string cookie = (string) null;
    public IPAddress customAddress = (IPAddress) null;
    private int _team = -1;
    public string name;
    public string password;
    public int id;
    public int name_color;
    public int access_level;
    public string player_name;
    public int count_friend;
    public int exp;
    public int gp;
    public int clan_id;
    public int money;
    public int player_id;
    public int pc_cafe;
    public int title_slot_count;
    private string addrEndPoint;
    public int _rank;
    public int _status;
    private Clan _clan;
    private PlayerInventory _inventory;
    private GameClient _connection;
    private Room _room;
    public Mission[] _mission;
    public PlayerStats _statistic;
    public ConfigP _config;
    public List<Friends> friends = new List<Friends>(); //AMIGOS
    public int char_red, char_blue, char_helmet, char_dino, char_beret; //Personagens do jogador
    public int weapon_primary, weapon_secondary, weapon_melee, weapon_thrown_normal, weapon_thrown_special; //Armas do jogador
    public int brooch, insignia, medal, blue_order; //Medalhas do jogador
    public int audio1, audio2, sensibilidade, visao, sangue, mira, mao, audio_enable; //Configurações do jogador

    public void setPrimaryWeapon(int primary)
    {
        this.weapon_primary = primary;
    }

    public int getPrimaryWeapon()
    {
        return this.weapon_primary;
    }

    public void setSecondaryWeapon(int secondary)
    {
        this.weapon_secondary = secondary;
    }

    public int getSecondaryWeapon()
    {
        return this.weapon_secondary;
    }

    public void setMeleeWeapon(int melee)
    {
        this.weapon_melee = melee;
    }

    public int getMeleeWeapon()
    {
        return this.weapon_melee;
    }

    public void setThrownNormalWeapon(int thrown_normal)
    {
        this.weapon_thrown_normal = thrown_normal;
    }

    public int getThrownNormalWeapon()
    {
        return this.weapon_thrown_normal;
    }

    public void setThrownSpecialWeapon(int thrown_special)
    {
        this.weapon_thrown_special = thrown_special;
    }

    public int getThrownSpecialWeapon()
    {
        return this.weapon_thrown_special;
    }

    public void setTitleSlotCount(int title_count)
    {
        this.title_slot_count = title_count;
    }

    public int getTitleSlotCount()
    {
        return this.title_slot_count;
    }

    public int getBrooch()
    {
        return this.brooch;
    }

    public int getInsignia()
    {
        return this.insignia;
    }

    public int getMedal()
    {
        return this.medal;
    }

    public int getBlueOrder()
    {
        return this.blue_order;
    }

    public void setCharRed(int character_red)
    {
        this.char_red = character_red;
    }

    public void setCharBlue(int character_blue)
    {
        this.char_blue = character_blue;
    }

    public void setCharHelmet(int helmet)
    {
        this.char_helmet = helmet;
    }

    public void setCharDino(int character_dino)
    {
        this.char_dino = character_dino;
    }

    public void setCharBeret(int beret)
    {
        this.char_beret = beret;
    }

    public int getCharRed()
    {
        return this.char_red;
    }

    public int getCharBlue()
    {
        return this.char_blue;
    }

    public int getCharHelmet()
    {
        return this.char_helmet;
    }

    public int getCharDino()
    {
        return this.char_dino;
    }

    public int getCharBeret()
    {
        return this.char_beret;
    }

    public Account()
    {
      this.player_name = "";
      this._statistic = new PlayerStats();
    }

    public void setInventory(PlayerInventory pi)
    {
      this._inventory = pi;
    }

    public bool isOnline()
    {
        return _online == true;
    }

    public void setPublicAddress(string address)
    {
      this.addrEndPoint = address;
    }

    public int getEquepItem(int slot)
    {
      return this._inventory.getEquipItemFromSlot(slot);
    }

    public List<ItemsModel> getInvetoryOnly(int type)
    {
      return this._inventory.getAllItemsOnType(type);
    }

    public List<ItemsModel> getInvetoryOnlyEquip(int type)
    {
      return this._inventory.getAllItemsOnTypeEquip(type);
    }

    public PlayerInventory getInventory()
    {
      return this._inventory;
    }

    public void setMoney(int money1)
    {
      this.money = money1;
    }

    public byte[] publicAdddress()
    {
      return IPAddress.Parse(this.addrEndPoint).GetAddressBytes();
    }

    public string toString()
    {
      return this.ToString();
    }

    public bool getOnline()
    {
      return this._online;
    }

    public Clan getClan()
    {
        if (this.clan_id == 0)
        {
            return null;
        }
        else
        {
            return ClanManager.getInstance().get(this.clan_id);
        }
    }

    public void setClanId(int id)
    {
        this.clan_id = id;
    }

    public int getClanId()
    {
        return this.clan_id;
    }

    public void setPcCafe(int pc_cafe)
    {
        this.pc_cafe = pc_cafe;
    }

    public int getPcCafe()
    {
        return this.pc_cafe;
    }

    public string getIP()
    {
      if (this._connection == null)
        return "<not connected>";
      else
        return this._connection.getIPString();
    }

    public byte[] getLocalAddress()
    {
      return this.addr;
    }

    public void setLocalAddress(byte[] address)
    {
      this.addr = address;
    }

    public void setOnlineStatus(bool isOnline)
    {
      this._online = isOnline;
    }

    public void setStatus(int status)
    {
        this._status = status;
    }

    public GameClient getClient()
    {
      return this._connection;
    }

    public void setClient(GameClient connection)
    {
      this._connection = connection;
    }

    public void close()
    {
      if (this._connection == null)
        return;
      this._connection.close();
    }

    public void sendPacket(SendBaseGamePacket sp)
    {
      if (this._connection == null)
        return;
      this._connection.sendPacket(sp);
    }

    public void setConnected(bool connected)
    {
      this._isConnected = connected;
    }

    public bool isConnected()
    {
      return this._isConnected;
    }

    public void setSlot(int slotId)
    {
      this._slotId = slotId;
    }

    public int getSlot()
    {
      return this._slotId;
    }

    public void setRoom(Room r)
    {
      this._room = r;
    }

    public Room getRoom()
    {
      return this._room;
    }

    public bool isRoomLeader()
    {
        return _room.getLeader() == this;
    }

    public int getMoney()
    {
      return this.money;
    }

    public void setExp(int exp)
    {
      this.exp = exp;
    }

    public int getExp()
    {
      return this.exp;
    }

    public void setCount_Friend(int count_friend)
    {
        this.count_friend = count_friend;
    }

    public int getCount_Friend()
    {
        return this.count_friend;
    }

    public void setGP(int gp)
    {
      this.gp = gp;
    }

    public int getGP()
    {
      return this.gp;
    }

    public void setRank(int rank)
    {
      this._rank = rank;
    }

    public int getRank()
    {
      return this._rank;
    }

    public string getPlayerName()
    {
      return this.player_name;
    }

    public void setPlayerName(string name)
    {
      this.player_name = name;
    }

    public int getNameColor()
    {
        return this.name_color;
    }

    public void setNameColor(int color)
    {
        this.name_color = color;
    }

    public void setPlayerId(int id)
    {
      this.player_id = id;
      this._inventory = new DaoManager(this._connection).getInventory(this.player_id);
      this._team = -1;
      this._mission = new Mission[3];
      if (ConfigManager.getInstance().getInfoItem(id) == null)
      {
          ConfigP confign = new ConfigP();
          confign.setOwnerId(id);
          confign.setOwnerName(this.player_name);
          confign.setMira(1);
          confign.setSensibilidade(50);
          confign.setSangue(1);
          confign.setVisao(70);
          confign.setAudio1(100);
          confign.setAudio2(100);
          ConfigManager.getInstance().AddConfig(confign);
          ConfigManager.getInstance().AddConfigDb(id, 100, 100, 50, 70, 1, 0, this.player_name, 0, 7);
          this._config = ConfigManager.getInstance().getInfoItem(id);
          this.audio1 = this.getConfig().getAudio1();
          this.audio2 = this.getConfig().getAudio2();
          this.sensibilidade = this.getConfig().getSensibilidade();
          this.visao = this.getConfig().getVisao();
          this.mira = this.getConfig().getMira();
          this.mao = this.getConfig().getMao();
          this.sangue = this.getConfig().getSangue();
          this.audio_enable = this.getConfig().getAudioEnable();
      }
      else
      {
          this._config = ConfigManager.getInstance().getInfoItem(id);
          this.audio1 = this.getConfig().getAudio1();
          this.audio2 = this.getConfig().getAudio2();
          this.sensibilidade = this.getConfig().getSensibilidade();
          this.visao = this.getConfig().getVisao();
          this.mira = this.getConfig().getMira();
          this.mao = this.getConfig().getMao();
          this.sangue = this.getConfig().getSangue();
          this.audio_enable = this.getConfig().getAudioEnable();
      }
    }

    public int getPlayerId()
    {
        return this.player_id;
    }

    public ConfigP getConfig()
    {
        return this._config;
    }

    public bool validatePassword(string p)
    {
      return p == this.password;
    }

    public int getTeam()
    {
      return this._team;
    }

    public void setTeam(int team)
    {
      this._team = team;
    }

    public void CheckCorrectInventory()
    {
        if (this._inventory == null)
            this._inventory = new PlayerInventory(this.player_id);
        this._inventory.CheckCorrectInventory(this.player_id);
    }

    public string getLogin()
    {
        return this.name;
    }

    public void addFriend(Friends friend)
    {
        this.friends.Add(friend);
    }
  }
}
