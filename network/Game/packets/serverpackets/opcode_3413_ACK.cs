// Type: PBServer.network.Game.packets.serverpackets.opcode_3079_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;

namespace PBServer.network.Game.packets.serverpackets
{
    public class opcode_3413_ACK : SendBaseGamePacket
    {
        public opcode_3413_ACK()
        {
            this.makeme();
        }

        protected internal override void write()
        {
            this.writeH((short)3413);
            this.writeD(1);
            this.writeC(2);
            this.writeS("Created by MoMz", 18);
        }
    }
}
