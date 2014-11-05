using PBServer;
using PBServer.managers;
using PBServer.model.clans;
using System.Collections.Generic;
using PBServer.src.managers;

namespace PBServer.network.Game.packets.serverpackets
{
    public class S_CLAN_LIST : SendBaseGamePacket
    {
        private List<Clan> clans = null;
        public S_CLAN_LIST(List<Clan> clans)
        {
            this.makeme();
            this.clans = clans;
        }
        protected internal override void write()//Ответ проверки клана на существование
        {
            CLogger.getInstance().info("Recebendo: S_CLAN_LIST");
            this.writeH((short)0x5A6);
            this.writeD(0); //номер пачки
            foreach (Clan clan_random in clans)
            {
                this.writeC(0xAA);
                this.writeD(clan_random.getClanId());  //номер клана в списке? //0x41
                this.writeS(clan_random.getClanName(), 17);
                this.writeC((byte)clan_random.getClanRank()); //Rank do clã
                this.writeC((byte)ClanManager.getInstance().getClanPlayers(clan_random.getClanId()).Count); //Quantidade de jogadores no clã
                this.writeC(50); //макс состав...?
                this.writeD(clan_random.getDateCreated());//дата создания клана 2099-12-31(Epic Fail)
                this.writeB(new byte[] { 0x01, 0x08, 0x1e, 0x1a }); //остальные параметры наверное
            }
        }
    }
}
