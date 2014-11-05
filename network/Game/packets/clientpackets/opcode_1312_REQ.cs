using PBServer;
using PBServer.network.Game.packets.serverpackets;
using PBServer.network.serverpackets;
using PBServer.src.model.accounts;
using PBServer.src.managers;
using PBServer.managers;

namespace PBServer.network.Game.packets.clientpackets
{
    public class opcode_1312_REQ : ReceiveBaseGamePacket
    {

        public opcode_1312_REQ(GameClient gc, byte[] buff)
        {
            this.makeme(gc, buff);
        }

        protected internal override void read()
        {
        }

        protected internal override void run()
        {
            this.getClient().getPlayer().setClanId(0);
            AccountManager.getInstance().UpdateClan(this.getClient().getPlayerId(), 0);
            ClanManager.getInstance().excludeClanInDb(this.getClient().getPlayerId());
            if (this.getClient() == null)
                return;
            this.getClient().sendPacket((SendBaseGamePacket)new S_CLAN_DISBAND());
            this.getClient().sendPacket((SendBaseGamePacket)new S_CLAN_LEAVE());
        }
    }
}
