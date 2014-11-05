using PBServer;
using PBServer.network;
using PBServer.network.Game.packets.serverpackets;
using serverpackets;

namespace PBServer.network.Game.packets.clientpackets
{
    public class opcode_2662_REQ : ReceiveBaseGamePacket
    {

        public opcode_2662_REQ(GameClient gc, byte[] buff)
        {
            this.makeme(gc, buff);
        }

        protected internal override void run()
        {
            if (this.getClient() == null)
                return;
            this.getClient().sendPacket((SendBaseGamePacket)new opcode_2562_ACK());
        }

        protected internal override void read()
        {
            if (this.getClient() == null)
                return;
            this.getClient().sendPacket((SendBaseGamePacket)new opcode_2562_ACK());

        }


    }
}
