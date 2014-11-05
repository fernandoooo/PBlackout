// Type: PBServer.network.Game.packets.serverpackets.PROTOCOL_BATTLE_ROOMINFO_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.src.model.rooms;

namespace PBServer.network.Game.packets.serverpackets
{
  public class S_BATTLE_ROOMINFO : SendBaseGamePacket
  {
    private Room _room;

    public S_BATTLE_ROOMINFO(Room r)
    {
      this._room = r;
      this.makeme();
    }

    protected internal override void write()
    {
        this.writeH((short)0xF08);
      this.writeD(this._room.getRoomId());
      this.writeS(this._room.name, 23);
      this.writeC((byte) this._room.map_id);
      this.writeH((short) 0);
      this.writeC((byte) this._room.room_type);
      this.writeC((byte) 5);
      this.writeC((byte) this._room.getAllPlayers().Count);
      this.writeC((byte) this._room.getSlotCount());
      this.writeC((byte) 5);
      this.writeC((byte) this._room.allweapons);
      this.writeC((byte) this._room.random_map);
      this.writeC((byte) this._room.special);
      this.writeS(this._room.getLeader().getPlayerName(), 33);
      this.writeD(this._room.killtime);
      this.writeC((byte) this._room.limit);
      this.writeC((byte) this._room.seeConf);
      this.writeH((short) this._room.autobalans);
      if (this._room._aiCount <= 0 || this._room._aiLevel <= 0)
        return;
      this.writeC((byte) this._room._aiCount);
      this.writeC((byte) this._room._aiLevel);
    }
  }
}
