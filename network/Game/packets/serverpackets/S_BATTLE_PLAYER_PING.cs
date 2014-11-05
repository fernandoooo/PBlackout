// Type: PBServer.network.serverpackets.opcode_3860_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;

namespace PBServer.network.serverpackets
{
    public class opcode_3860_ACK : SendBaseGamePacket
    {
        private byte[] _slots;

        public opcode_3860_ACK(byte[] slots)
        {
            this._slots = slots;
            this.makeme();
        }

        protected internal override void write()
        {
            this.writeH((short)0xD11);
            this.writeB(this._slots);
        }
    }
}
