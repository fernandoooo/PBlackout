using PBServer;
using PBServer.network.Game.packets.serverpackets;

namespace PBServer.network.Game.packets.clientpackets
{
    public class opcode_1360_REQ : ReceiveBaseGamePacket
    {
        private int unk1;
        private int unk2;
        private int unk3;
        private int unk4;
        public opcode_1360_REQ(GameClient gc, byte[] buff)
        {
            this.makeme(gc, buff);
        }

        protected internal override void read()
        {
            this.readH();
            this.unk1 = this.readC();
            this.unk2 = this.readC();
            this.unk3 = this.readC();
            this.unk4 = this.readC();
            CLogger.getInstance().info("unk1 " + this.unk1);
            CLogger.getInstance().info("unk2 " + this.unk2);
            CLogger.getInstance().info("unk3 " + this.unk3);
            CLogger.getInstance().info("unk4 " + this.unk4);
        }

        protected internal override void run()
        {
            if (this.getClient() == null)
                return;
            this.getClient().sendPacket((SendBaseGamePacket)new opcode_1360_ACK());
        }
    }
}
