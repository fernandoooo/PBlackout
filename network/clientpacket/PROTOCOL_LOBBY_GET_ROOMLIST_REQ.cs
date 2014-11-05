using PBServer;
using PBServer.network;
using PBServer.network.serverpackets;
using PBServer.src.data.xml.parsers;
using PBServer.src.model;
using PBServer.src.data.xml.holders;

namespace PBServer.network.clientpacket
{
  internal class PROTOCOL_LOBBY_GET_ROOMLIST_REQ : ReceiveBaseGamePacket
  {
    public PROTOCOL_LOBBY_GET_ROOMLIST_REQ(GameClient Client, byte[] data)
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
      Channel channel = ChannelInfoHolder.getChannel(this.getClient().getChannelId());
      channel.RemoveEmptyRooms();
      this.getClient().sendPacket((SendBaseGamePacket) new S_LOBBY_GET_ROOMLIST(channel));
    }
  }
}
