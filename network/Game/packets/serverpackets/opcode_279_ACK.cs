// Type: PBServer.network.Game.packets.serverpackets.opcode_279_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.src.model.accounts;

namespace PBServer.network.Game.packets.serverpackets
{
  public class opcode_279_ACK : SendBaseGamePacket
  {
    private Account _p;

    public opcode_279_ACK(Account p)
    {
      this._p = p;
      this.makeme();
    }

    protected internal override void write()
    {
      this.writeH((short) 279);
      this.writeC((byte) this._p.getRank());
      this.writeC((byte) 0);
      this.writeC((byte) this._p.getPlayerName().Length);
      this.writeS(this._p.getPlayerName(), this._p.getPlayerName().Length);
      this.writeB(new byte[17]
      {
        (byte) 0,
        (byte) 0,
        (byte) 6,
        (byte) 2,
        (byte) 80,
        (byte) 32,
        (byte) 32,
        (byte) 1,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 25,
        (byte) 159,
        (byte) 73,
        (byte) 0
      });
    }
  }
}
