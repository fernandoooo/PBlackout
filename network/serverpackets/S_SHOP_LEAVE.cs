// Type: PBServer.network.serverpackets.PROTOCOL_SHOP_LEAVE_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;

namespace PBServer.network.serverpackets
{
  public class S_SHOP_LEAVE : SendBaseGamePacket
  {
    public S_SHOP_LEAVE()
    {
      this.makeme();
    }

    protected internal override void write()
    {
        this.writeH((short)0xB02);
        this.writeD(0);
        this.writeD(0);
    }
  }
}
