using PBServer;

namespace PBServer.network.Game.packets.serverpackets
{
    public class SM_INVITE_ROOM_RETURN : SendBaseGamePacket
    {

        public SM_INVITE_ROOM_RETURN()
        {
            this.makeme();
        }

        protected internal override void write()
        {
            CLogger.getInstance().info("Recebendo: SM_INVITE_ROOM_RETURN");
            this.writeH((short)3885);
            this.writeD(0);
        }
    }
}
