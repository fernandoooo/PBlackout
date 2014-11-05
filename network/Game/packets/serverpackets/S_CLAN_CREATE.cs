using PBServer;
using MySql.Data.MySqlClient;
using PBServer.db;
using PBServer.src.model.accounts;
using PBServer.managers;
namespace PBServer.network.Game.packets.serverpackets
{
    public class S_CLAN_CREATE : SendBaseGamePacket
    {
        private string _clan_name, _clan_info;
        private Account p;
        public S_CLAN_CREATE(string clan_name, Account player, string clan_info)
        {
            this.makeme();
            this._clan_name = clan_name;
            this.p = player;
            this._clan_info = clan_info;
        }

        protected internal override void write()
        {
            ClanManager.getInstance().createClanInDb(this._clan_name, p.getPlayerId());
            CLogger.getInstance().info("Recebendo: S_CREATE_CLAN");
            this.writeH(1311);
            this.writeD(0);
            this.writeC(0x60);
            this.writeC(0x41);
            this.writeH(0);
            this.writeS(_clan_name, 16);
            this.writeH(0);
            this.writeC(1);
            this.writeB(new byte[] { 0x32, 0x4b, 0x05, 0x33, 0x01 });
            this.writeB(new byte[14]);
            this.writeC(2);
            this.writeC(0x17);
            this.writeC(6);
            this.writeB(new byte[5]);

            this.writeS(this.p.getPlayerName(), 33); //лидер клана
            this.writeC(0x0D);

            this.writeS(this._clan_info, 120);
            this.writeB(new byte[12]);
        }
    }
}