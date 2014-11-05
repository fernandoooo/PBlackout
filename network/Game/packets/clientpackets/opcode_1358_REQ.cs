using PBServer;
using PBServer.network.Game.packets.serverpackets;

namespace PBServer.network.Game.packets.clientpackets
{
    public class opcode_1358_REQ : ReceiveBaseGamePacket
    {
        private string message;
        public opcode_1358_REQ(GameClient gc, byte[] buff)
        {
            this.makeme(gc, buff);
        }

        protected internal override void read()
        {
            int messageLength = this.readH();
            this.message = this.readS(messageLength);
            CLogger.getInstance().info("message: " + this.message);
        }

        protected internal override void run()
        {
            if (this.getClient() == null)
                return;
        }
    }
}
