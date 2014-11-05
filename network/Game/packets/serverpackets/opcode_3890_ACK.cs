// Type: PBServer.network.Game.packets.serverpackets.opcode_3890_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.src.model.rooms;
using System;

namespace PBServer.network.Game.packets.serverpackets
{
    public class opcode_3890_ACK : SendBaseGamePacket
    {
        private Room _room;

        public opcode_3890_ACK(Room r)
        {
            this._room = r;
            this.makeme();
        }

        protected internal override void write()
        {
            this.writeH((short)0xD31);
            this.writeC(Convert.ToByte(this._room._aiLevel <= 10 ? this._room._aiLevel : 10));
            for (int index = 0; index < 8; ++index)
            {
                this.writeD(1);
                this.writeD(1);
            }
        }
    }
}
