using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PBServer.network.Game.packets.serverpackets
{
    public class opcode_1360_ACK : SendBaseGamePacket
    {

        public opcode_1360_ACK()
        {
            this.makeme();
        }

        protected internal override void write()
        {
            CLogger.getInstance().info("Send: opcode_1360_ACK");
            this.writeH((short)1361);
            this.writeD(0);
        }
    }
}
