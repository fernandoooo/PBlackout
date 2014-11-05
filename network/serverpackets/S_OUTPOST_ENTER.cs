using PBServer;
using System;
using System.Globalization;

namespace PBServer.network.serverpackets
{
    public class S_OUTPOST_ENTER : SendBaseGamePacket
    {

        public S_OUTPOST_ENTER()
        {
            this.makeme();
        }

        protected internal override void write()
        {
            this.writeH((short)0xB52);
            this.writeD(0);
        }
    }
}
