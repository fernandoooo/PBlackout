using PBServer;

namespace PBServer.network.Login.packets.serverpackets
{
    public class opcode_2579_ACK : SendBaseLoginPacket
    {
        private int _UNK;
        public opcode_2579_ACK(int val)
        {
            this.makeme();
            _UNK = val;
        }

        protected internal override void write()
        {
            this.writeH(2580);
            this.writeD(0);
        }
    }
}
