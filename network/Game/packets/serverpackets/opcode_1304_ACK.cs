using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PBServer.network.Game.packets.serverpackets
{
    public class opcode_1304_ACK : SendBaseGamePacket
    {
        private string clan_name, owner_name, clan_info;
        public opcode_1304_ACK()//string clan_name, string owner_name, string clan_info)
        {
            this.makeme();
            //this.clan_name = clan_name;
            //this.owner_name = owner_name;
            //this.clan_info = clan_info;
        }

        protected internal override void write()
        {
            this.writeH((short)1305);
            this.writeD(0);
            this.writeC(0x60);
            this.writeC(0x41);
            this.writeH(0);
            this.writeS("Exatuo", 16);
            //this.writeS(this.clan_name, 16);
            this.writeH(0);
            this.writeC(1);
            this.writeB(new byte[] { 1, 1, 1, 2, 1 });
            this.writeB(new byte[14]);
            this.writeC(2);
            this.writeC(0x17);
            this.writeC(6);
            this.writeB(new byte[5] { 2, 2, 2, 2, 1 });

            this.writeS("nome", 33);
            //this.writeS(this.owner_name, 33); //лидер клана
            this.writeC(0x0D);

            this.writeS("Baicon", 120);
            //this.writeS(this.clan_info, 120);
            this.writeB(new byte[12]);
        }
    }
}
