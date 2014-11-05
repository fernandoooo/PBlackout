using PBServer.network.serverpackets;
using PBServer;
using PBServer.network;
using PBServer.src.data.xml.parsers;
using PBServer.src.model.accounts;
using PBServer.src.model;
using System.Collections.ObjectModel;
using PBServer.managers;
using PBServer.threading;
using System.Linq;
using PBServer.src.data.xml.holders;

namespace PBServer.network.clientpacket
{
    internal class PROTOCOL_ROOM_GET_LOBBY_USER_LIST_REQ : ReceiveBaseGamePacket
    {
        public PROTOCOL_ROOM_GET_LOBBY_USER_LIST_REQ(GameClient Client, byte[] data)
        {
            this.makeme(Client, data);
        }

        protected internal override void read()
        {

        }

        protected internal override void run()
        {
            Account p = this.getClient().getPlayer();
            if (p != null)
            {
                Channel ch = ChannelInfoHolder.getChannel(this.getClient().getChannelId());
                p.sendPacket((SendBaseGamePacket)new S_ROOM_LOBBY_USER_LIST(ch));
            }
        }
    }
}

