using PBServer;
using PBServer.network.Game.packets.serverpackets;
using PBServer.model.clans;
using PBServer.managers;
using PBServer.src.model.accounts;
using PBServer.src.managers;


namespace PBServer.network.Game.packets.clientpackets
{
    public class opcode_1304_REQ : ReceiveBaseGamePacket
    {
        private int unk;
        public opcode_1304_REQ(GameClient gc, byte[] buff)
        {
            this.makeme(gc, buff);
        }

        protected internal override void read()
        {
            unk = this.readH();
            CLogger.getInstance().info("UNK: " + unk);
        }

        protected internal override void run()
        {
            Account p = this.getClient().getPlayer();
            this.getClient().getPlayer().sendPacket(new opcode_1304_ACK());//ClanManager.getInstance().get(p.getClanId()).getClanName(), AccountManager.getInstance().getAccountInPlayerId(ClanManager.getInstance().get(p.getClanId()).getOwnerId()).getPlayerName(), ClanManager.getInstance().get(p.getClanId()).getClanInfo()));
        }
    }
}
