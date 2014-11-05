using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PBServer.src.data.xml.parsers;
using PBServer.src.model.accounts;
using PBServer.model.players;
using PBServer.src.model;
using PBServer.src.model.rooms;
using PBServer.src.managers;
using PBServer.managers;

namespace PBServer.network.serverpackets
{
    public class S_LOBBY_FIND_PLAYERINFO : SendBaseGamePacket
    {

        public S_LOBBY_FIND_PLAYERINFO()
        {
            this.makeme();
        }

        protected internal override void write()
        {
            CLogger.getInstance().info("Send: PROTOCOL_LOBBY_FIND_PLAYERINFO_ACK");
            this.writeH(297);
            this.writeB(new byte[] { 4, 24, 0, 0x80 });
        }
    }
}
