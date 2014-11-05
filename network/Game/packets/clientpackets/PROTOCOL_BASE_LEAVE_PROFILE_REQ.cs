using PBServer;
using PBServer.network;
using PBServer.network.Game.packets.serverpackets;
using PBServer.src.model.rooms;

namespace PBServer.network.Game.packets.clientpackets
{
  public class PROTOCOL_BASE_LEAVE_PROFILE_REQ : ReceiveBaseGamePacket
  {
    public PROTOCOL_BASE_LEAVE_PROFILE_REQ(GameClient gc, byte[] buff)
    {
      this.makeme(gc, buff);
    }

    protected internal override void run()
    {
      GameClient client = this.getClient();
      if (client.getPlayer() != null && client.getPlayer().getRoom() != null)
        client.getPlayer().getRoom().changeSlotState(client.getPlayer().getSlot(), SLOT_STATE.SLOT_STATE_NORMAL, true);
      client.sendPacket((SendBaseGamePacket) new S_BASE_LEAVE_PROFILE());
    }

    protected internal override void read()
    {
    }
  }
}
