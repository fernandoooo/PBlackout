using PBServer;
using PBServer.network;
using PBServer.network.serverpackets;
using System;

namespace PBServer.network.clientpacket
{
  internal class PROTOCOL_SERVER_MESSAGE_ANNOUNCE_REQ : ReceiveBaseGamePacket
  {
    private int id_channel;

    public PROTOCOL_SERVER_MESSAGE_ANNOUNCE_REQ(GameClient Client, byte[] data)
    {
      this.makeme(Client, data);
    }

    protected internal override void read()
    {
      int num = (int) this.readH();
      this.id_channel = this.readD();
      this.getClient().setChannelId(this.id_channel);
    }

    protected internal override void run()
    {
      if (this.getClient() == null)
        return;
      this.getClient().sendPacket((SendBaseGamePacket) new SM_ANNOUNCE_GET(this.id_channel));
    }
  }
}
