using PBServer;
using PBServer.network;
using PBServer.network.serverpackets;
using PBServer.src.model.rooms;
using PBServer.network.Game.packets.serverpackets;

namespace PBServer.network.clientpacket
{
  internal class PROTOCOL_SHOP_ENTER_REQ : ReceiveBaseGamePacket
  {
    public PROTOCOL_SHOP_ENTER_REQ(GameClient Client, byte[] data)
    {
      this.makeme(Client, data);
    }

    protected internal override void read()
    {
        this.readH();
        _v = readD();
    }

    protected internal override void run()
    {
        CLogger.getInstance().info("O jogador " + this.getClient().getPlayer().getPlayerName().ToString() + " entrou na loja.");
        if (base.getClient() != null)
        {
            GameClient client = base.getClient();
            if (client.getPlayer() != null)
            {
                if (client.getPlayer().getRoom() != null)
                {
                    client.getPlayer().getRoom().changeSlotState(client.getPlayer().getSlot(), SLOT_STATE.SLOT_STATE_SHOP, true);
                }
            }
            client.sendPacket(new S_SHOP_ENTER());
        }
    }
    private int _v;
  }
}
