using PBServer;
using PBServer.network;
using PBServer.network.serverpackets;
using PBServer.src.managers;
using System;
using System.Net;
using PBServer.network.serverpacket;

namespace PBServer.network.clientpacket
{
    public class PROTOCOL_BASE_USER_TITLE_USE_REQ : ReceiveBaseGamePacket
    {
        private byte slot;
        private byte titleId;
        public PROTOCOL_BASE_USER_TITLE_USE_REQ(GameClient Client, byte[] data)
        {
            this.makeme(Client, data);
        }

        protected internal override void read()
        {
            readH();
            slot = readB(1)[0];
            titleId = readB(1)[0];
        }

        protected internal override void run()
        {
            CLogger.getInstance().info("O jogador " + this.getClient().getPlayer().getPlayerName().ToString() + " usou um título.");
            if (this.getClient() == null)
                return;
            this.getClient().sendPacket((SendBaseGamePacket)new PROTOCOL_BASE_USER_TITLE_USE_ACK(new byte[] { slot, titleId }));
        }
    }
}
