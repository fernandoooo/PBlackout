using PBServer;

namespace PBServer.network.Game.packets.serverpackets
{
    public class S_CLAN_CHECK_NAME : SendBaseGamePacket
    {
        private int error;

        public S_CLAN_CHECK_NAME(int error)
        {
            this.makeme();
            this.error = error;
        }
        protected internal override void write()//Ответ проверки клана на существование
        {
            this.writeH((short)1448);
            this.writeD(this.error); //если есть ошибка, то отрицательное число, в противном случае 0
        }
    }
}
