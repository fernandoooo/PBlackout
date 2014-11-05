using PBServer;
using PBServer.network;
using PBServer.network.serverpackets;

namespace PBServer.network.clientpacket
{
  internal class PROTOCOL_BASE_GET_SCHANNELLIST_REQ : ReceiveBaseLoginPacket
  {
    private LoginClient _lc;

    public PROTOCOL_BASE_GET_SCHANNELLIST_REQ(LoginClient Client, byte[] data)
    {
      this.makeme(Client, data);
      this._lc = Client;
    }

    protected internal override void read()
    {
    }

    protected internal override void run()
    {
      if (this.getClient() == null)
        return;
      this.getClient().sendPacket((SendBaseLoginPacket)new S_BASE_GET_SCHANNELLIST(this._lc));
    }
  }
}
