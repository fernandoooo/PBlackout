using PBServer;
using PBServer.network.Game.packets.serverpackets;

namespace PBServer.network.Game.packets.clientpackets
{
    public class opcode_534_REQ : ReceiveBaseGamePacket
    {
        private int unk;
        private byte[] unk2;
        public opcode_534_REQ(GameClient Client, byte[] data)
        {
            this.makeme(Client, data);
        }

        protected internal override void read()
        {
            this.readH();
            unk2 = this.readB(8);
            CLogger.getInstance().extra_info("unk: " + unk2[0]);
            CLogger.getInstance().extra_info("unk: " + unk2[1]);
            CLogger.getInstance().extra_info("unk: " + unk2[2]);
            CLogger.getInstance().extra_info("unk: " + unk2[3]);
            CLogger.getInstance().extra_info("unk: " + unk2[4]);
            CLogger.getInstance().extra_info("unk: " + unk2[5]);
            CLogger.getInstance().extra_info("unk: " + unk2[6]);
            CLogger.getInstance().extra_info("unk: " + unk2[7]);
            CLogger.getInstance().extra_info("unk: " + unk2[8]);
        }

        protected internal override void run()
        {
            this.getClient().sendPacket((SendBaseGamePacket)new SM_INVENTORY_EQUIP_NOTUSED(unk2));
        }
    }
}
