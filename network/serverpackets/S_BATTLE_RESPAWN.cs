// Type: PBServer.network.serverpackets.PROTOCOL_BATTLE_RESPAWN_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe
using PBServer.src.managers;
using PBServer;
using PBServer.network.clientpacket;
using PBServer.src.model.accounts;

namespace PBServer.network.serverpackets
{
  public class S_BATTLE_RESPAWN : SendBaseGamePacket
  {
    private Account _p;
    private ResInfo _info;
    public S_BATTLE_RESPAWN(ResInfo info, Account p)
    {
      this.makeme();
      this._info = info;
      this._p = p;
    }

    protected internal override void write()
    {
      this.writeH((short) 3338);
      this.writeD(this._p.getSlot());
      this.writeD(1);
      this.writeD(1);
      this.writeD(this._info.first1);
      this.writeD(this._info.second1);
      this.writeD(this._info.third1);
      this.writeD(this._info.fourth1);
      this.writeD(this._info.fifth1);
      this.writeD(this._info.id1);
      this.writeB(new byte[6] {0x64, 0x64, 0x64, 0x64, 0x64, 0x01});
      this.writeD(this._info.red1);
      this.writeD(this._info.blue1);
      this.writeD(this._info.head1);
      this.writeD(this._info.beret1); //beret1
      this.writeD(this._info.dino1); //dino1
    }
  }
}
