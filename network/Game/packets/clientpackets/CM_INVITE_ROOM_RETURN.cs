using PBServer;
using PBServer.network.Game.packets.serverpackets;

namespace PBServer.network.Game.packets.clientpackets
{
    public class CM_INVITE_ROOM_RETURN : ReceiveBaseGamePacket
    {

        public CM_INVITE_ROOM_RETURN(GameClient gc, byte[] buff)
        {
            this.makeme(gc, buff);
        }

        protected internal override void read()
        {

        }

        protected internal override void run()
        {
            this.getClient().getPlayer().sendPacket(new SM_INVITE_ROOM_RETURN());
        }
    }
}
