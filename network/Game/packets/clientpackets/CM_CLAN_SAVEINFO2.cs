using PBServer;
using PBServer.network.Game.packets.serverpackets;
using PBServer.model.clans;
using PBServer.managers;
using PBServer.src.model.accounts;
using PBServer.src.managers;


namespace PBServer.network.Game.packets.clientpackets
{
    public class CM_CLAN_SAVEINFO2 : ReceiveBaseGamePacket
    {
        private string clan_info;
        public CM_CLAN_SAVEINFO2(GameClient gc, byte[] buff)
        {
            this.makeme(gc, buff);
        }

        protected internal override void read()
        {
            this.readH();
            this.clan_info = this.readS(this.readC());
            CLogger.getInstance().extra_info("As informações do clã foram salvas com sucesso.");
        }

        protected internal override void run()
        {
            Account player = this.getClient().getPlayer();
            ClanManager.getInstance().UpdateClanInfo(player.getClanId(), this.clan_info);
        }
    }
}