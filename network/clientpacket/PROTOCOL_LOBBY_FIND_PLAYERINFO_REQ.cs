using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PBServer.network.serverpackets;
using PBServer.src.model.accounts;

namespace PBServer.network.clientpacket
{
    public class PROTOCOL_LOBBY_FIND_PLAYERINFO_REQ : ReceiveBaseGamePacket
    {
        private int unk1;
        private int unk2;
        private string unk3;
        public PROTOCOL_LOBBY_FIND_PLAYERINFO_REQ(GameClient Client, byte[] data)
        {
            this.makeme(Client, data);
        }

        protected internal override void read()
        {
            readH();
            unk1 = readC();
            unk2 = readC();
            unk3 = readS();
            CLogger.getInstance().info("Unk1: " + unk1);
            CLogger.getInstance().info("Unk2: " + unk2);
            CLogger.getInstance().info("Unk3: " + unk3);
        }

        protected internal override void run()
        {
            this.getClient().sendPacket(new S_LOBBY_FIND_PLAYERINFO());
        }
    }
}
