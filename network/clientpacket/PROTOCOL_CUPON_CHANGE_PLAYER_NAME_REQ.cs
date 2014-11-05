using PBServer;
using PBServer.network;
using PBServer.network.serverpackets;
using PBServer.src.model.rooms;

namespace PBServer.network.clientpacket
{
    internal class PROTOCOL_CUPON_CHANGE_PLAYER_NAME_REQ : ReceiveBaseGamePacket
    {
        private string unk1;
        public PROTOCOL_CUPON_CHANGE_PLAYER_NAME_REQ(GameClient Client, byte[] data)
        {
            this.makeme(Client, data);
        }

        protected internal override void read()
        {
            this.readH();
            this.unk1 = readS();
            CLogger.getInstance().info("Unk1: " + this.unk1); 
        }

        protected internal override void run()
        {
            if (this.getClient() == null)
                return;
            this.getClient().sendPacket((SendBaseGamePacket)new PROTOCOL_CUPON_CHANGE_PLAYER_NAME_ACK());
        }
    }
}
