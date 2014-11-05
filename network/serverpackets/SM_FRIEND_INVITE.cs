using PBServer;

namespace PBServer.network.serverpackets
{
    public class SM_FRIEND_INVITE : SendBaseGamePacket
    {
        private string name;
        private int error;
        public SM_FRIEND_INVITE(string player_name, int error)
        {
            this.makeme();
            this.name = player_name;
            this.error = error;
        }

        protected internal override void write()
        {
            CLogger.getInstance().info("Send: PROTOCOL_LOBBY_ADD_FRIEND_ACK");
            this.writeH((short)279);
            if (this.error == 0)
            {
                writeB(new byte[] { 0x1B, 0x00, 0x17, 1, 1, 0, 8 });
                writeS(this.name, this.name.Length);
                //writeB(new byte[] { 0x00, 0x0e, 0x47, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00 });
                //this.writeD(0);
                //this.writeC((byte)33);
                //this.writeS(this.name, 33);
                //this.writeD(50);
            }
            else
            {
                //this.writeD(this.error);
            }
        }
    }
}
