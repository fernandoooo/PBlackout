using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PBServer.network.serverpackets;
using PBServer.src.model.accounts;

namespace PBServer.network.clientpacket
{
    public class PROTOCOL_LOBBY_GET_PLAYERINFO_REQ : ReceiveBaseGamePacket
    {
        private int id_p;
        public PROTOCOL_LOBBY_GET_PLAYERINFO_REQ(GameClient Client, byte[] data)
        {
            this.makeme(Client, data);
        }

        protected internal override void read()
        {
            this.readH();
            this.id_p = (int)this.readC();
        }

        protected internal override void run()
        {
            if (this.getClient() == null)
                return;
            this.getClient().sendPacket((SendBaseGamePacket)new S_LOBBY_GET_PLAYERINFO(this.id_p));
        }
    }
}
