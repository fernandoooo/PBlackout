// Type: PBServer.network.serverpackets.PROTOCOL_LOBBY_GET_ROOMINFO_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.src.model.rooms;

namespace PBServer.network.serverpackets
{
    public class S_LOBBY_GET_ROOMINFO : SendBaseGamePacket
    {
        private Room _r;

        public S_LOBBY_GET_ROOMINFO(Room r)
        {
            this.makeme();
            this._r = r;
        }

        protected internal override void write()
        {
            this.writeH((short)3088);
            this.writeS(this._r.getLeader().getPlayerName(), 33);
            this.writeD(this._r.killtime);
            this.writeC((byte)this._r.limit);
            this.writeC((byte)this._r.seeConf);
            this.writeH((short)this._r.autobalans);
        }
    }
}