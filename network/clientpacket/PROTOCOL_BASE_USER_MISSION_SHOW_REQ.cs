using PBServer.network.serverpacket;

namespace PBServer.network.clientpacket
{
    public class PROTOCOL_BASE_USER_MISSION_SHOW_REQ : ReceiveBaseGamePacket
    {
        private int unk1;
        private int unk2;
        private int unk3;
        public PROTOCOL_BASE_USER_MISSION_SHOW_REQ(GameClient Client, byte[] data)
        {
            this.makeme(Client, data);
        }

        protected internal override void read()
        {
            this.readH();
            this.unk1 = this.readC();
            this.unk2 = this.readC();
            this.unk3 = this.readC();
            CLogger.getInstance().info("Unk1: " + this.unk1);
            CLogger.getInstance().info("CardId: " + this.unk2);
            CLogger.getInstance().info("Unk3: " + this.unk3);
        }

        protected internal override void run()
        {
            if (this.getClient() == null)
                return;
            this.getClient().sendPacket((SendBaseGamePacket)new PROTOCOL_BASE_USER_MISSION_SHOW_ACK(this.unk2));
        }
    }
}
