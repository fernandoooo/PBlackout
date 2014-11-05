using PBServer;
using PBServer.network;
using PBServer.network.Game.packets.serverpackets;
using serverpackets;
using PBServer.model.clans;
using PBServer.managers;
using System.Collections.Generic;

namespace PBServer.network.Game.packets.clientpackets
{
    public class opcode_1451_REQ : ReceiveBaseGamePacket
    {
        private List<Clan> _clans = null;
        public opcode_1451_REQ(GameClient gc, byte[] buff)
        {
            this.makeme(gc, buff);
        }


        protected internal override void run()
        {
            if (this.getClient() == null)
                return;
            this.getClient().sendPacket((SendBaseGamePacket)new S_CLAN_QUANTITY(this._clans.Count));
        }

        protected internal override void read()
        {
            this._clans = ClanManager.getInstance().getClans();
        }
    }
}
