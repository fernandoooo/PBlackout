using PBServer;

namespace PBServer.network.Game.packets.serverpackets
{
    public class S_BATTLE_END_ROUND : SendBaseGamePacket
    {
        public S_BATTLE_END_ROUND()
        {
            this.makeme();
        }

        protected internal override void write()
        {
            CLogger.getInstance().info("Recebendo: S_BATTLE_END_ROUND");
            this.writeH((short) 3869); //3869
            this.writeC(0);
            this.writeD(1);
            this.writeC(0);
        }
    }
}
