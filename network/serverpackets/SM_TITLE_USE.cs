using PBServer;

namespace PBServer.network.serverpacket
{
    public class PROTOCOL_BASE_USER_TITLE_USE_ACK : SendBaseGamePacket
    {
        public PROTOCOL_BASE_USER_TITLE_USE_ACK(byte[] info)
        {
            this.makeme();
        }

        protected internal override void write()
        {
            this.writeH(0xA3E);
            this.writeD(0);
        }
    }
}
