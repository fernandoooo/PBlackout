using PBServer;

namespace PBServer.network.Game.packets.serverpackets
{
    public class SM_CLAN_SAVEINFO2 : SendBaseGamePacket
    {

        public SM_CLAN_SAVEINFO2()
        {
            this.makeme();
        }

        protected internal override void write()
        {
            CLogger.getInstance().info("Recebendo: SM_CLAN_SAVEINFO2");
            this.writeH((short)1365);
            this.writeD(0); //PRIMARY
        }
    }
}
