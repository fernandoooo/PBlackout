// Type: PBServer.src.network.Game.packets.serverpackets.opcode_3850_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;

namespace PBServer.src.network.Game.packets.serverpackets
{
  internal class opcode_3850_ACK : SendBaseGamePacket
  {
    private int _slot;

    public opcode_3850_ACK(int slot)
    {
      this._slot = slot;
      this.makeme();
    }

    protected internal override void write()
    {
      this.writeH((short) 3850);
      this.writeD(this._slot);
    }
  }
}
