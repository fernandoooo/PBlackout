using PBServer;
using PBServer.network.serverpackets;
using PBServer.network.Game.packets.serverpackets;
using PBServer.src.managers;

namespace PBServer.network.clientpacket
{
    public class CM_FRIEND_REMOVE : ReceiveBaseGamePacket
    {
        private int unk;
        public CM_FRIEND_REMOVE(GameClient Client, byte[] data)
        {
            this.makeme(Client, data);
        }

        protected internal override void read()
        {
            unk = base.readH();
            CLogger.getInstance().extra_info("unk: " + this.unk);
        }

        protected internal override void run()
        {
            this.getClient().getPlayer().sendPacket((SendBaseGamePacket)new SM_FRIEND_REMOVE());
        }
    }
}
