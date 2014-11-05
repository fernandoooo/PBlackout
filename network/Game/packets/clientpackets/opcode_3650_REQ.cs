using PBServer;
using PBServer.network;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;

namespace PBServer.network.Game.packets.clientpackets
{
  public class opcode_3650_REQ : ReceiveBaseGamePacket
  {
    private string _mapName;

    public opcode_3650_REQ(GameClient gc, byte[] buff)
    {
      this.makeme(gc, buff);
    }

    protected internal override void run()
    {
    }

    protected internal override void read()
    {
      int num = (int) this.readH();
      this._mapName = this.readS((int) this.readC());
      Account player = this.getClient().getPlayer();
      Room room = player.getRoom();
      room.changeSlotState(player.getSlot(), SLOT_STATE.SLOT_STATE_RENDEZVOUS, true);
      room.ChangeRoomState(ROOM_STATE.ROOM_STATE_RENDEZVOUS, player);
      CLogger.getInstance().info("Nome do mapa: " + this._mapName);
      this.getClient().getPlayer().getRoom().map_name = this._mapName;
    }
  }
}
