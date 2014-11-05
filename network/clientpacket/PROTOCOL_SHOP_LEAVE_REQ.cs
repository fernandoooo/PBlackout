using PBServer;
using PBServer.network;
using PBServer.network.serverpackets;
using PBServer.src.model.rooms;

namespace PBServer.network.clientpacket
{
  internal class PROTOCOL_SHOP_LEAVE_REQ : ReceiveBaseGamePacket
  {
    public PROTOCOL_SHOP_LEAVE_REQ(GameClient Client, byte[] data)
    {
      this.makeme(Client, data);
    }

    protected internal override void read()
    {
    }

    protected internal override void run()
    {
        CLogger.getInstance().info("O jogador " + this.getClient().getPlayer().getPlayerName().ToString() + " saiu da loja.");
      if (this.getClient() == null)
        return;
      GameClient client = this.getClient();
      if (client.getPlayer() != null && client.getPlayer().getRoom() != null)
        client.getPlayer().getRoom().changeSlotState(client.getPlayer().getSlot(), SLOT_STATE.SLOT_STATE_NORMAL, true);
      client.sendPacket((SendBaseGamePacket) new S_SHOP_LEAVE());
    }
  }
}
