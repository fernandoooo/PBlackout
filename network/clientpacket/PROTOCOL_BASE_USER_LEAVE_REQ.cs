using PBServer;
using PBServer.network;
using PBServer.network.serverpackets;

namespace PBServer.network.clientpacket
{
  internal class PROTOCOL_BASE_USER_LEAVE_REQ : ReceiveBaseLoginPacket
  {
    public PROTOCOL_BASE_USER_LEAVE_REQ(LoginClient Client, byte[] data)
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
      this.getClient().sendPacket((SendBaseLoginPacket) new S_BASE_USER_LEAVE(this.getClient().getPlayer()));
    }
  }
}
