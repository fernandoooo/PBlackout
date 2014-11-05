using PBServer;
using System;

namespace PBServer.network.serverpackets
{
    public class S_SHOP_ENTER : SendBaseGamePacket
    {
        public S_SHOP_ENTER()
        {
            this.makeme();
        }

        protected internal override void write()
        {
            this.writeH((short)0xB04);
            this.writeD(Convert.ToInt32(DateTime.Now.ToString("yyMMddHHmm")));
        }
    }
}
