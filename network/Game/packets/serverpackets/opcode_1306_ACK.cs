using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PBServer.network.Game.packets.serverpackets
{
    public class opcode_1306_ACK : SendBaseGamePacket
    {
        public opcode_1306_ACK()
        {
            this.makeme();
        }

        protected internal override void write()
        {
            CLogger.getInstance().info("Send: opcode_1306_ACK");
            this.writeH((short)1307);
            this.writeD(0);
            this.writeD(1);
        }
    }
}
