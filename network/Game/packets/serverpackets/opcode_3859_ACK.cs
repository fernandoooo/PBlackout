using PBServer;

namespace PBServer.network.Game.packets.serverpackets
{
    public class opcode_3859_ACK : SendBaseGamePacket
    {
        private int slot;
        public opcode_3859_ACK(int slot)
        {
            this.makeme();
            this.slot = slot;
        }

        protected internal override void write()
        {
            CLogger.getInstance().info("Send: opcode_3859_ACK");
            this.writeH((short)3859);
            this.writeD(slot);
            this.writeB(new byte[20]);
        }
    }
}
