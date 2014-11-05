using PBServer;

namespace PBServer.network.Game.packets.serverpackets
{
    public class SM_CUPON_EQUIP2 : SendBaseGamePacket
    {

        public SM_CUPON_EQUIP2()
        {
            this.makeme();
        }

        protected internal override void write()
        {
            CLogger.getInstance().info("Recebendo: SM_CUPON_EQUIP2");
            this.writeH((short)537);
            //writeC(4);
            //writeC(7);
            writeB(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x01, 0x00, 0x00, 0x00 });
            /*writeB(new byte [] {0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01,
                    0x00, 0x00, 0x00,(byte) 0xef,(byte) 0xba, 0x14, 0x00, 0x00, 0x00, 0x00, 0x00,(byte) 0x90,
                    (byte)0xf1,(byte) 0x86, 0x47, 0x02, 0x53,(byte) 0xa3,(byte) 0xb1, 0x53 });*/
        }
    }
}
