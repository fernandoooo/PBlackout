using PBServer;
using System;
using System.Globalization;

namespace PBServer.network.Game.packets.serverpackets
{
    public class SM_INVENTORY_EQUIP_NOTUSED : SendBaseGamePacket
    {
        private byte[] _unk;
        public SM_INVENTORY_EQUIP_NOTUSED(byte[] unk)
        {
            this.makeme();
            this._unk = unk;
        }

        protected internal override void write()
        {
            CLogger.getInstance().info("Send: opcode_534_ACK");
            this.writeH((short)535);
            this.writeB(new byte[] { 
                0x19,
                0x00,
                0x17,
                0x02,
                0x01,
                0x00,
                0x00,
                0x00,
                0x07,
                0xEF,
                0xFD,
                this._unk[3],
                this._unk[4],
                this._unk[5],
                this._unk[6],
                this._unk[7] });
            this.writeB(new byte[] { 
                0x00,
                0x00,
                0x00,
                0x00,
                0x7C,
                0x9B,
                0xD7,
                0x17,
                0x02,
                0xEC,
                0x11,
                0x00,
                0x54 });
        }
    }
}
