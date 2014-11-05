// Type: PBServer.network.Game.packets.serverpackets.opcode_3870_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.src.model.rooms;

namespace PBServer.network.Game.packets.serverpackets
{
  public class S_BOMB_INSTALL : SendBaseGamePacket
  {
    private int _slot;
    private int _zone;
    private int _x;
    private int _y;
    private int _z;
    private Room room;

    public S_BOMB_INSTALL(int slot, int zone, int x, int y, int z, Room room)
    {
      this.makeme();
      this._zone = zone;
      this._slot = slot;
      this._x = x;
      this._y = y;
      this._z = z;
      this.room = room;
      this.room.bomb = 1;
    }

    protected internal override void write()
    {
      CLogger.getInstance().info("Send: opcode_3870_ACK");
      this.writeH((short) 3871);
      this.writeD(this._slot);
      this.writeC((byte) this._zone);
      this.writeH((short) 42); //Tempo para explosão
      this.writeD(this._x);
      this.writeD(this._y);
      this.writeD(this._z);
    }
  }
}
