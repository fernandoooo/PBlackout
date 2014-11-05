using PBServer;

namespace PBServer.network.serverpacket
{
    public class PROTOCOL_BASE_USER_MISSION_SHOW_ACK : SendBaseGamePacket
    {
        private int mission;
        public PROTOCOL_BASE_USER_MISSION_SHOW_ACK(int mission)
        {
            this.makeme();
            this.mission = mission;
        }

        protected internal override void write()
        {
            this.writeH((short)2602);
            this.writeC(240);
            this.writeC(1);
        }
    }
}
