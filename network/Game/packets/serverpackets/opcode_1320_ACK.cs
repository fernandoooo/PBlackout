using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PBServer.network.Game.packets.serverpackets
{
    public class opcode_1320_ACK : SendBaseGamePacket
    {
        public opcode_1320_ACK()
        {
            this.makeme();
        }

        protected internal override void write()
        {
            CLogger.getInstance().info("Send: opcode_1320_ACK");
            this.writeH((short)1321);
            this.writeD(0);
            this.writeD(1);
            this.writeC(50);
            this.writeS("Blue", 33);
            this.writeD(09122014);
        }
    }
}
