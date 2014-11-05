using PBServer;
using PBServer.network;
using PBServer.network.serverpackets;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;
using PBServer.src.data.xml.parsers;

namespace PBServer.network.clientpacket
{
    internal class PROTOCOL_ROOM_CHANGE_HOST_REQ : ReceiveBaseGamePacket
    {
        private int slot;
        public PROTOCOL_ROOM_CHANGE_HOST_REQ(GameClient Client, byte[] data)
        {
            this.makeme(Client, data);
        }

        protected internal override void read()
        {
            this.readH();
            this.slot = this.readD();
            CLogger.getInstance().info("Slot do novo dono: " + slot);
        }

        protected internal override void run()
        {
            Account player = this.getClient().getPlayer();
            player.getRoom().setNewLeader(this.slot);
            player.sendPacket((SendBaseGamePacket)new S_ROOM_CHANGE_HOST(this.slot));
        }
    }
}