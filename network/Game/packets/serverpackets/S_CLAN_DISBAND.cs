using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PBServer.network.Game.packets.serverpackets
{
    public class S_CLAN_DISBAND : SendBaseGamePacket
    {
        public S_CLAN_DISBAND()
        {
            this.makeme();
        }

        protected internal override void write()
        {
            CLogger.getInstance().info("Send: opcode_1312_ACK");
            this.writeH(1313);
            this.writeD(0);
            this.writeC(0x60);
            this.writeC(0x41);
            this.writeH(0);

            this.writeS("", 16);
            this.writeH(0);
            this.writeC(1);
            this.writeB(new byte[] { 0, 0, 0, 0, 0 });
            this.writeB(new byte[14]);
            this.writeC(2);
            this.writeC(0x17);
            this.writeC(6);
            this.writeB(new byte[5]);

            this.writeS("", 33); //лидер клана
            this.writeC(0x0D);

            this.writeS("", 120);
            this.writeB(new byte[12]);
        }
    }
}
