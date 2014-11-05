using PBServer;
using PBServer.network.Game.packets.serverpackets;
using PBServer.managers;
using PBServer.model.clans;
using PBServer.src.managers;

namespace PBServer.network.Game.packets.clientpackets
{
    public class opcode_1310_REQ : ReceiveBaseGamePacket
    {
        private int length_c_name;
        private int length_c_info;
        private int length_info;
        private string clan_name;
        private string clan_info;
        private string info;
        private int unk;
        private Clan clan;
        public opcode_1310_REQ(GameClient gc, byte[] buff)
        {
            this.makeme(gc, buff);
        }

        protected internal override void read()
        {
            clan = new Clan();
            this.readH();
            this.length_c_name = readC();
            this.length_c_info = readC();
            this.length_info = readC();
            this.clan_name = readS(length_c_name);
            this.clan_info = readS(length_c_info);
            this.info = readS(length_info);
            this.unk = readD();
            clan.setClanId(this.getClient().getPlayerId());
            clan.setClanName(this.clan_name);
            clan.setClanRank(0);
            clan.setLogo1(0);
            clan.setLogo2(0);
            clan.setLogo3(0);
            clan.setLogo4(0);
            clan.setOwnerId(this.getClient().getPlayerId());
            clan.setLogoColor(0);
            CLogger.getInstance().info("Nome do clã: " + this.clan_name);
            CLogger.getInstance().info("Informação do clã: " + this.clan_info);
            ClanManager.getInstance().AddClan(clan);
            this.getClient().getPlayer().setClanId(this.getClient().getPlayerId());
            AccountManager.getInstance().UpdateClan(this.getClient().getPlayerId(), this.getClient().getPlayerId());
        }

        protected internal override void run()
        {
            if (this.getClient() == null)
                return;
            this.getClient().getPlayer().sendPacket((SendBaseGamePacket)new S_CLAN_CREATE(this.clan_name, this.getClient().getPlayer(), this.clan_info));
        }
    }
}
