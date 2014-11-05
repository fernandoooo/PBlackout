using PBServer;
using PBServer.network;
using PBServer.network.serverpackets;
using PBServer.src.data.xml.parsers;
using PBServer.src.model.rooms;
using PBServer.src.data.xml.holders;

namespace PBServer.network.clientpacket
{
  internal class PROTOCOL_LOBBY_GET_ROOMINFO_REQ : ReceiveBaseGamePacket
  {
    private int roomId;

    public PROTOCOL_LOBBY_GET_ROOMINFO_REQ(GameClient Client, byte[] data)
    {
      this.makeme(Client, data);
    }

    protected internal override void read()
    {
      int num = (int) this.readH();
      this.roomId = (int) this.readC();
    }

    protected internal override void run()
    {
      if (this.getClient() == null)
        return;
      Room roomInId = ChannelInfoHolder.getChannel(this.getClient().getChannelId()).getRoomInId(this.roomId);
      if (roomInId == null || roomInId.getLeader() == null)
        return;
      this.getClient().sendPacket((SendBaseGamePacket) new S_LOBBY_GET_ROOMINFO(roomInId));
    }
  }
}
