using PBServer;
using PBServer.network.Game.packets.serverpackets;

namespace PBServer.network.Game.packets.clientpackets
{
    public class opcode_2845_REQ : ReceiveBaseGamePacket
    {
        public opcode_2845_REQ(GameClient gc, byte[] buff)
        {
            this.makeme(gc, buff);
        }

        protected internal override void run()
        {
            if (this.getClient() == null)
                return;
            this.getClient().sendPacket((SendBaseGamePacket)new opcode_2845_ACK());
        }

        protected internal override void read()
        {
        }
    }
}
