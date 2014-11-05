// Type: PBServer.network.Login.packets.serverpackets.opcode_2660_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;

namespace PBServer.network.Login.packets.serverpackets
{
  public class opcode_2678_ACK : SendBaseLoginPacket
  {
    public opcode_2678_ACK()
    {
      this.makeme();
    }

    protected internal override void write()
    {
      this.writeH((short) 2679);
      this.writeD(0);
    }
  }
}
