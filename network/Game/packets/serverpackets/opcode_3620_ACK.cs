using PBServer;
using PBServer.src.model.rooms;

namespace PBServer.network.Game.packets.serverpackets
{
    public class opcode_3620_ACK : SendBaseGamePacket
    {

        public opcode_3620_ACK()
        {
            this.makeme();
        }

        protected internal override void write()
        {
            this.writeH((short)3620);
            this.writeD(0);
        }
    }
}
