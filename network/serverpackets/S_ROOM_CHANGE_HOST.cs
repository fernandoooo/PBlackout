using PBServer;

namespace PBServer.network.serverpackets
{
    public class S_ROOM_CHANGE_HOST : SendBaseGamePacket
    {
        private int slot;
        public S_ROOM_CHANGE_HOST(int slot)
        {
            this.makeme();
            this.slot = slot;
        }

        protected internal override void write()
        {
            CLogger.getInstance().info("Send: PROTOCOL_ROOM_CHANGE_HOST_ACK");
            this.writeH((short)3618);
            this.writeD(this.slot);
        }
    }
}
