using PBServer;
using PBServer.network.Game.packets.serverpackets;

namespace PBServer.network.Game.packets.clientpackets
{
    public class opcode_1306_REQ : ReceiveBaseGamePacket
    {

        public opcode_1306_REQ(GameClient gc, byte[] buff)
        {
            this.makeme(gc, buff);
        }

        protected internal override void read()
        {
           
        }

        protected internal override void run()
        {
            this.getClient().getPlayer().sendPacket(new opcode_1306_ACK());
        }
    }
}
