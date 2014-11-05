using PBServer;
using PBServer.network;
using PBServer.network.serverpackets;
using PBServer.src.data.xml.parsers;
using PBServer.src.model.rooms;
using PBServer.src.data.xml.holders;

namespace PBServer.network.clientpacket
{
  internal class PROTOCOL_LOBBY_CREATE_ROOM_REQ : ReceiveBaseGamePacket
  {
    private Room _room;
    private int _unk2;
    private int _unk4;
    private int _roomId;
    private int test1;
    private int test2;
    private string _player_name;
    private int unkc3;
    private int unkc4;
    private int unkc5;

    public PROTOCOL_LOBBY_CREATE_ROOM_REQ(GameClient client, byte[] data)
    {
      this.makeme(client, data);
    }

    protected internal override void read()
    {
        CLogger.getInstance().info("O jogador " + this.getClient().getPlayer().getPlayerName().ToString() + " criou uma sala.");
      int num = (int) this.readH();
      this.getClient().getPlayer();
      this._room = new Room(ChannelInfoHolder.getChannel(this.getClient().getChannelId()).getRooms().Count, this.getClient().getChannelId());
      this._roomId = this.readD();
      this._room.name = this.readS(18);
      this.readB(5);
      this._room.map_id = (int) this.readC();
      this._unk2 = (int) this.readC();
      this._room.stage4v4 = (int) this.readC();
      this._room.room_type = (int) this.readC();
      this.test1 = (int) this.readC();
      this.test2 = (int) this.readC();
      this._room.initSlotCount((int) this.readC());
      this._unk4 = (int) this.readC();
      this._room.allweapons = (int) this.readC();
      this._room.random_map = (int) this.readC();
      this._room.special = (int) this.readC();
      this._player_name = this.readS(33);
      this._room.killtime = (int) this.readC();
      this.unkc3 = (int) this.readC();
      this.unkc4 = (int) this.readC();
      this.unkc5 = (int) this.readC();
      this._room.limit = (int) this.readC();
      this._room.seeConf = (int) this.readC();
      this._room.autobalans = (int) this.readH();
      this._room.password = this.readS(4);
      if (this._room.special == 6)
      {
        this._room._aiCount = (int) this.readC();
        this._room._aiLevel = (int) this.readC();
      }
      this._room._channelId = this.getClient().getChannelId();
      int slotId = this._room.addPlayer(this.getClient().getPlayer());
      this.getClient().getPlayer().setRoom(this._room);
      this.getClient().getPlayer().setSlot(slotId);
      ChannelInfoHolder.getChannel(this.getClient().getChannelId()).addRoom(this._room);
    }

    protected internal override void run()
    {
      if (this.getClient() == null)
        return;
      this.getClient().sendPacket((SendBaseGamePacket) new S_LOBBY_CREATE_ROOM(this._room));
    }
  }
}
