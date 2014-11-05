// Type: PBServer.network.Login.packets.clientpacket.opcode_2667_REQ
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.network;
using PBServer.network.Login.packets.serverpackets;

namespace PBServer.network.Login.packets.clientpacket
{
  public class opcode_2667_REQ : ReceiveBaseLoginPacket
  {
    public opcode_2667_REQ(LoginClient lc, byte[] buff)
    {
      this.makeme(lc, buff);
    }

    protected internal override void run()
    {
      this.getClient().sendPacket((SendBaseLoginPacket) new opcode_2667_ACK());
    }

    protected internal override void read()
    {
    }
  }
}
