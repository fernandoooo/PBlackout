using PBServer;

namespace PBServer.network.Game.packets.serverpackets
{
    public class opcode_544_ACK : SendBaseGamePacket
    {

        public opcode_544_ACK()
        {
            this.makeme();
        }

        protected internal override void write()
        {
            CLogger.getInstance().info("Send: opcode_544_ACK");
            this.writeD(1); //if ( v21 >= 0 )
            this.writeD(1024);
            this.writeD(1025);
        }
    }
}
