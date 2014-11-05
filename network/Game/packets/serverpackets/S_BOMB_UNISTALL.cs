// Type: PBServer.network.Game.packets.serverpackets.opcode_3872_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.src.model.rooms;

namespace PBServer.network.Game.packets.serverpackets
{
  public class S_BOMB_UNISTALL : SendBaseGamePacket
  {
    private int _slot;
    private Room room;
    public S_BOMB_UNISTALL(int slot, Room room)
    {
      this.makeme();
      this._slot = slot;
      this.room = room;
      this.room.bomb = 2;
    }

    protected internal override void write()
    {
        CLogger.getInstance().info("Send: opcode_3872_ACK");
      this.writeH((short) 3873);
      this.writeD(this._slot);
    }
  }
}
