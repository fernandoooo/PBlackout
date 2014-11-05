using PBServer;

namespace PBServer.network.Game.packets.serverpackets
{
    public class SM_CLAN_SAVEINFO1 : SendBaseGamePacket
    {

        public SM_CLAN_SAVEINFO1()
        {
            this.makeme();
        }

        protected internal override void write()
        {
            CLogger.getInstance().info("Recebendo: SM_CLAN_SAVEINFO1");
            this.writeH((short)1363);
            this.writeD(0); //PRIMARY
        }
    }
}
