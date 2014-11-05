using PBServer;
using PBServer.network;
using PBServer.network.Login.packets.serverpackets;

namespace PBServer.network.Login.packets.clientpacket
{
    public class opcode_2575_REQ : ReceiveBaseLoginPacket
    {
        public opcode_2575_REQ(LoginClient lc, byte[] buff)
        {
            this.makeme(lc, buff);
        }

        protected internal override void run()
        {
            this.getClient().sendPacket((SendBaseLoginPacket)new S_UPDATE_CHANNELS());
        }

        protected internal override void read()
        {
        }
    }
}
