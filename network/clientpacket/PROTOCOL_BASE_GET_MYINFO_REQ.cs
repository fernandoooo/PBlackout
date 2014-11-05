using PBServer;
using PBServer.network;
using PBServer.network.serverpackets;
using PBServer.network.Game.packets.serverpackets;
using PBServer.model.clans;
using System.Collections.Generic;
using PBServer.src.model.accounts;
using PBServer.managers;
using PBServer.src.managers;

namespace PBServer.network.clientpacket
{
  internal class PROTOCOL_BASE_GET_MYINFO_REQ : ReceiveBaseLoginPacket
  {
    public PROTOCOL_BASE_GET_MYINFO_REQ(LoginClient Client, byte[] data)
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
      this.getClient().sendPacket((SendBaseLoginPacket)new S_BASE_GET_MYCLANP(this.getClient()));
      this.getClient().sendPacket((SendBaseLoginPacket)new S_BASE_GET_MYINFO(this.getClient()));
    }
  }
}
