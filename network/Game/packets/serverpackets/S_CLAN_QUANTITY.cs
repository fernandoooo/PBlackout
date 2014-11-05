using PBServer;
using PBServer.managers;
using PBServer.model.clans;
using System.Collections.Generic;

namespace PBServer.network.Game.packets.serverpackets
{
    public class S_CLAN_QUANTITY : SendBaseGamePacket
    {
        private int size;
        public S_CLAN_QUANTITY(int size)
        {
            this.makeme();
            this.size = size;
        }

        protected internal override void write()
        {
            CLogger.getInstance().info("Send: C_CLAN_QUANTITY");
            this.writeH(1452);
            this.writeD(this.size);
        }
    }
}
