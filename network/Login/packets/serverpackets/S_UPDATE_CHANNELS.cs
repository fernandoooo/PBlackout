using PBServer;

namespace PBServer.network.Login.packets.serverpackets
{
    public class S_UPDATE_CHANNELS : SendBaseLoginPacket
    {
        public S_UPDATE_CHANNELS()
        {
            this.makeme();
        }

        protected internal override void write()
        {
            this.writeH(2575);
            this.writeD(0);
        }
    }
}
