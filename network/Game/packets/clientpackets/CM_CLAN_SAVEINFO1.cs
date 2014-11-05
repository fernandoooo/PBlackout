using PBServer;
using PBServer.network.Game.packets.serverpackets;
using PBServer.model.clans;
using PBServer.managers;
using PBServer.src.model.accounts;
using PBServer.src.managers;


namespace PBServer.network.Game.packets.clientpackets
{
    public class CM_CLAN_SAVEINFO1 : ReceiveBaseGamePacket
    {
        private string unk;
        public CM_CLAN_SAVEINFO1(GameClient gc, byte[] buff)
        {
            this.makeme(gc, buff);
        }

        protected internal override void read()
        {
            this.readH();
            this.unk = this.readS(this.readC());
            CLogger.getInstance().extra_info("Unk: " + this.unk);
        }

        protected internal override void run()
        {
        }
    }
}