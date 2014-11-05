// Type: PBServer.network.serverpackets.PROTOCOL_INVENTORY_LEAVE_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;

namespace PBServer.network.serverpackets
{
  public class S_INVENTORY_LEAVE : SendBaseGamePacket
  {
      private int type;
      public S_INVENTORY_LEAVE(int type)
      {
          this.makeme();
          this.type = type;
      }

    protected internal override void write()
    {
        this.writeH((short)0xE06);
        this.writeD(this.type); //0
        if (type < 0)
        {
            this.writeD(0); //0
        }
    }
  }
}
