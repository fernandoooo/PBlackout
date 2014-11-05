using PBServer;
using PBServer.network;
using PBServer.network.Game.packets.serverpackets;

namespace PBServer.network.Game.packets.clientpackets
{
    public class opcode_2901_REQ : ReceiveBaseGamePacket
    {
        public opcode_2901_REQ(GameClient gc, byte[] buff)
        {
            this.makeme(gc, buff);
        }

        protected internal override void run()
        {
            this.getClient().sendPacket((SendBaseGamePacket)new opcode_2901_ACK());
        }

        protected internal override void read()
        {
        }
    }
}
