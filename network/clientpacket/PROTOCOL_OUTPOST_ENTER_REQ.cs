using PBServer;
using PBServer.network;
using PBServer.network.serverpackets;
using PBServer.src.data.xml.parsers;
using PBServer.src.model;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;

namespace PBServer.network.clientpacket
{
    internal class PROTOCOL_OUTPOST_ENTER_REQ : ReceiveBaseGamePacket
    {
        public PROTOCOL_OUTPOST_ENTER_REQ(GameClient Client, byte[] data)
        {
            this.makeme(Client, data);
        }

        protected internal override void read()
        {
        }

        protected internal override void run()
        {
            CLogger.getInstance().info("O jogador " + this.getClient().getPlayer().getPlayerName().ToString() + " entrou no outpost.");
            if (this.getClient() != null)
            {
                GameClient client = this.getClient();
                if (client.getPlayer() != null)
                {
                    if (client.getPlayer().getRoom() != null)
                    {
                        client.getPlayer().getRoom().changeSlotState(client.getPlayer().getSlot(), SLOT_STATE.SLOT_STATE_OUTPOST, true);
                    }
                }
                client.sendPacket(new S_OUTPOST_ENTER());
            }
        }
    }
}
