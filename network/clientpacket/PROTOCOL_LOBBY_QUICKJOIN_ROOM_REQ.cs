using PBServer;
using PBServer.network;
using PBServer.network.serverpackets;
using PBServer.src.data.xml.parsers;
using PBServer.src.model;
using PBServer.src.model.accounts;
using PBServer.src.data.xml.holders;

namespace PBServer.network.clientpacket
{
    internal class PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ : ReceiveBaseGamePacket
    {
        //private int unk1;
        public PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ(GameClient Client, byte[] data)
        {
            this.makeme(Client, data);
        }

        protected internal override void read()
        {
            //this.unk1 = readD();
            //CLogger.getInstance().extra_info("unk1: " + this.unk1);
        }

        protected internal override void run()
        {
            if (this.getClient() == null)
                return;
            Channel channel = ChannelInfoHolder.getChannel(this.getClient().getChannelId());
            this.getClient().sendPacket((SendBaseGamePacket)new S_LOBBY_QUICKJOIN(channel));
        }
    }
}
