using PBServer;
using PBServer.network;
using PBServer.network.serverpackets;
using PBServer.src.data.xml.parsers;
using PBServer.src.model;
using PBServer.src.model.accounts;

namespace PBServer.network.clientpacket
{
    internal class PROTOCOL_OUTPOST_LEAVE_REQ : ReceiveBaseGamePacket
    {
        public PROTOCOL_OUTPOST_LEAVE_REQ(GameClient Client, byte[] data)
        {
            this.makeme(Client, data);
        }

        protected internal override void read()
        {
        }

        protected internal override void run()
        {
            if (this.getClient() == null)
                return;
            this.getClient().sendPacket((SendBaseGamePacket)new S_OUTPOST_LEAVE());
        }
    }
}
