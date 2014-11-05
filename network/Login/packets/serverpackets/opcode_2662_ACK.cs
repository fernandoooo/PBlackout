using PBServer;
using System;
using System.Globalization;

namespace serverpackets
{

    public class opcode_2562_ACK : SendBaseGamePacket
    {
        private string hex = "00000000C01660A000000000000000000000000000000100000000000000000000000000000000000000020000000000000000000000000000000000000003000000000000000000000000000000000000000400000000000000000000000000000000000000050000000000000000000000000000000000000006000000000000000000000000000000000000000700000000000000000000000000000000000000080000000000000000000000000000000000000009000000000000000000000000000000000000000A000000000000000000000000000000000000000B000000000000000000000000000000000000000C000000000000000000000000000000000000000D000000000000000000000000000000000000000E000000000000000000000000000000000000000F0000000000000000000000000000000000000010000000000000000000000000000000000";

        public opcode_2562_ACK()
        {
            this.makeme();
        }

        protected internal override void write()
        {
            this.writeD(525);
            this.writeB(opcode_2562_ACK.ConvertHexStringToByteArray(this.hex));
        }

        public static byte[] ConvertHexStringToByteArray(string hexString)
        {
            if (hexString.Length % 2 != 0)
            {
                ;
            }
            byte[] numArray = new byte[hexString.Length / 2];
            for (int index = 0; index < numArray.Length; ++index)
            {
                string s = hexString.Substring(index * 2, 2);
                numArray[index] = byte.Parse(s, NumberStyles.HexNumber, (IFormatProvider)CultureInfo.InvariantCulture);
            }
            return numArray;
        }
    }
}