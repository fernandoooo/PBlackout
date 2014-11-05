using PBServer;
using PBServer.network.Game.packets.serverpackets;

namespace PBServer.network.Game.packets.clientpackets
{
    public class opcode_536_REQ : ReceiveBaseGamePacket
    {
        public opcode_536_REQ(GameClient Client, byte[] data)
        {
            this.makeme(Client, data);
        }

        protected internal override void read()
        {
            this.readH();
        }

        protected internal override void run()
        {
            this.getClient().sendPacket((SendBaseGamePacket)new opcode_536_ACK());
        }
    }
}
