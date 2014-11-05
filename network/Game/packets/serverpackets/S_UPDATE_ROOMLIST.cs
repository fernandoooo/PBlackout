using PBServer;

namespace PBServer.network.Game.packets.serverpackets
{
    public class S_UPDATE_ROOMLIST : SendBaseGamePacket
    {

        public S_UPDATE_ROOMLIST()
        {
            this.makeme();
        }

        protected internal override void write()
        {
            CLogger.getInstance().info("Recebendo: S_UPDATE_ROOMLIST");
            this.writeH((short)2576);
            this.writeD(0); //PRIMARY
        }
    }
}
