using PBServer;

namespace PBServer.network.serverpackets
{
    public class SM_TITLE_DETACH : SendBaseGamePacket
    {
        private int _slot;
        public SM_TITLE_DETACH(int slot)
        {
            this.makeme();
            _slot = slot;
        }

        protected internal override void write()
        {
            this.writeH(0xA40);
            this.writeD(0);
        }
    }
}
