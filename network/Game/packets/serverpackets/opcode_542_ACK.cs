using PBServer;

namespace PBServer.network.Game.packets.serverpackets
{
    public class opcode_542_ACK : SendBaseGamePacket
    {

        public opcode_542_ACK()
        {
            this.makeme();
        }

        protected internal override void write()
        {
            CLogger.getInstance().info("Send: opcode_542_ACK");
            this.writeH((short)542);
            this.writeD(5);
            this.writeD(6);
            this.writeD(4);
            this.writeD(7);
        }
    }
}
