// Type: PBServer.network.Game.packets.serverpackets.opcode_3867_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.src.model.rooms;

namespace PBServer.network.Game.packets.serverpackets
{
    public class S_START_BATTLE : SendBaseGamePacket
    {
        private Room _r;

        public S_START_BATTLE(Room r)
        {
            this._r = r;
            this.makeme();
        }

        protected internal override void write()
        {
            this.writeH((short)0xD2B);
            this.writeC((byte)2); //1
            this.writeD(this._r.getTimeLost());
            this.writeH((short)3); //1
        }
    }
}
