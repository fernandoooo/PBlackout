using PBServer;
using PBServer.network;
using PBServer.network.Login.packets.serverpackets;
using System;

namespace PBServer.network.Login.packets.clientpacket
{
    public class opcode_2579_REQ : ReceiveBaseLoginPacket
    {
        private int _UNK;
        public opcode_2579_REQ(LoginClient lc, byte[] buff)
        {
            this.makeme(lc, buff);
        }

        protected internal override void read()
        {
            _UNK = readH(); // Постоянно приходит 2826

            int count;
            count = readC();

            string acc;
            acc = readS(count);

            long _long = readQ();
            int count2 = readC();
            byte[] ip = readB(4);

            /*
            _log.info("_UNK: " + _UNK);
            _log.info("count: " + count);
            _log.info("acc: " + acc);
            _log.info("_long: " + _long);
            _log.info("count2: " + count2);
            */

            try
            {
                //CLogger.getInstance().info("IP: " + InetAddress.getByAddress(ip).toString());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected internal override void run()
        {
            this.getClient().sendPacket((SendBaseLoginPacket)new opcode_2579_ACK(_UNK));
        }
    }
}
