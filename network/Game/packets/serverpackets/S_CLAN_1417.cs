using PBServer;

namespace PBServer.network.Game.packets.serverpackets
{
    public class S_CLAN_1417 : SendBaseGamePacket
    {

        public S_CLAN_1417()
        {
            this.makeme();
        }

        protected internal override void write()
        {
            this.writeH((short)0x589);
            this.writeC((byte)Config.NecessaryRank);
            this.writeD(Config.NecessaryGold);
        }
    }
}