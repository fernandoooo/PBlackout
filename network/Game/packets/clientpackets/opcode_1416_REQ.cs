using PBServer;
using PBServer.network;
using PBServer.network.Game.packets.serverpackets;

namespace PBServer.network.Game.packets.clientpackets
{
    public class opcode_1416_REQ : ReceiveBaseGamePacket
    {

        public opcode_1416_REQ(GameClient gc, byte[] buff)
        {
            this.makeme(gc, buff);
        }

        protected internal override void read()
        {
        }

        protected internal override void run()
        {
            if (this.getClient() == null)
                return;
            this.getClient().sendPacket((SendBaseGamePacket)new S_CLAN_1417());
        }
    }
}