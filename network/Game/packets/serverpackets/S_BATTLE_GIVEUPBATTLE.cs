// Type: PBServer.network.Game.packets.serverpackets.PROTOCOL_BATTLE_GIVEUPBATTLE_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;

namespace PBServer.network.Game.packets.serverpackets
{
  public class S_BATTLE_GIVEUPBATTLE : SendBaseGamePacket
  {
    private int _slot;

    public S_BATTLE_GIVEUPBATTLE(int slot)
    {
      this._slot = slot;
      this.makeme();
    }

    protected internal override void write()
    {
      this.writeB(new byte[2]
      {
        (byte) 53,
        (byte) 15
      });
      this.writeD(this._slot);
    }
  }
}
