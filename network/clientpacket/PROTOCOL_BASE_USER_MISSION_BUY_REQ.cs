using PBServer;
using PBServer.network;
using PBServer.network.serverpackets;
using PBServer.src.managers;
using System;
using System.Net;
using PBServer.network.serverpacket;

namespace PBServer.network.clientpacket
{
    public class PROTOCOL_BASE_USER_MISSION_BUY_REQ : ReceiveBaseGamePacket
    {
        private int missionId;
        public PROTOCOL_BASE_USER_MISSION_BUY_REQ(GameClient Client, byte[] data)
        {
            this.makeme(Client, data);
        }

        protected internal override void read()
        {
            this.readH();
            this.missionId = this.readC();
            CLogger.getInstance().info("MissionId: " + this.missionId);
        }

        protected internal override void run()
        {
            if (this.getClient() == null)
                return;
            this.getClient().sendPacket((SendBaseGamePacket)new PROTOCOL_BASE_USER_MISSION_BUY_ACK(this.missionId));
        }
    }
}
