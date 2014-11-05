using PBServer;

namespace PBServer.network.serverpacket
{
    public class PROTOCOL_BASE_USER_MISSION_BUY_ACK : SendBaseGamePacket
    {
        private int _missionId;
        public PROTOCOL_BASE_USER_MISSION_BUY_ACK(int missionId)
        {
            this.makeme();
            this._missionId = missionId;
        }

        protected internal override void write()
        {
            this.writeH((short)2606);
            this.writeD(0);
        }
    }
}