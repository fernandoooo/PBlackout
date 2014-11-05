using PBServer;
using System;
using System.Globalization;

namespace PBServer.network.Game.packets.serverpackets
{
    public class opcode_536_ACK : SendBaseGamePacket
    {

        public opcode_536_ACK()
        {
            this.makeme();
        }

        protected internal override void write()
        {
            CLogger.getInstance().info("Send: opcode_536_ACK");
            this.writeH((short)537);
            this.writeD(1);
        }
    }
}
