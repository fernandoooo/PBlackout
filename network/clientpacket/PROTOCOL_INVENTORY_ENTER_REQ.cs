using PBServer;
using PBServer.network;
using PBServer.network.serverpackets;
using PBServer.src.model.rooms;

namespace PBServer.network.clientpacket
{
  internal class PROTOCOL_INVENTORY_ENTER_REQ : ReceiveBaseGamePacket
  {
    public PROTOCOL_INVENTORY_ENTER_REQ(GameClient Client, byte[] data)
    {
      this.makeme(Client, data);
    }

    protected internal override void read()
    {
    }

    protected internal override void run()
    {
      if (this.getClient() == null)
        return;
      GameClient client = this.getClient();
      if (client.getPlayer() != null && client.getPlayer().getRoom() != null)
        client.getPlayer().getRoom().changeSlotState(client.getPlayer().getSlot(), SLOT_STATE.SLOT_STATE_INVENTORY, true);
      GameClient client2 = base.getClient();
      int playerId = client2.getPlayerId();
        client.sendPacket((SendBaseGamePacket)new S_INVENTORY_ENTER(playerId, client2));
        CLogger.getInstance().info("O jogador " + this.getClient().getPlayer().getPlayerName().ToString() + " entrou no inventário.");
    }
  }
}
