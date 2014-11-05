using PBServer;
using PBServer.network.Game.packets.serverpackets;
using PBServer.network.serverpackets;
using PBServer.src.model.accounts;
using PBServer.src.managers;
using PBServer.managers;

namespace PBServer.network.Game.packets.clientpackets
{
    public class opcode_1320_REQ : ReceiveBaseGamePacket
    {

        public opcode_1320_REQ(GameClient gc, byte[] buff)
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
            this.getClient().sendPacket((SendBaseGamePacket)new opcode_1320_ACK());
        }
    }
}
