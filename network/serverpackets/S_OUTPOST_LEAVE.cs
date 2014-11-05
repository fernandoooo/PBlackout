using PBServer;

namespace PBServer.network.serverpackets
{
    public class S_OUTPOST_LEAVE : SendBaseGamePacket
    {

        public S_OUTPOST_LEAVE()
        {
            this.makeme();
        }

        protected internal override void write()
        {
            this.writeH((short)0xB54);
            this.writeD(0);
        }
    }
}
