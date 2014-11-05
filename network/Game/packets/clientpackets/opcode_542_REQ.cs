using PBServer;
using PBServer.network.Game.packets.serverpackets;

namespace PBServer.network.Game.packets.clientpackets
{
    public class opcode_542_REQ : ReceiveBaseGamePacket
    {
        private int unk;
        private int unk2;
        private int unk3;
        public opcode_542_REQ(GameClient Client, byte[] data)
        {
            this.makeme(Client, data);
        }

        protected internal override void read()
        {
            unk = this.readH();
            unk2 = this.readC();
            unk3 = this.readD();
            CLogger.getInstance().extra_info("Unk: " + this.unk);
            CLogger.getInstance().extra_info("Unk2: " + this.unk2);
            CLogger.getInstance().extra_info("Unk3: " + this.unk3);
        }

        protected internal override void run()
        {
            this.getClient().sendPacket((SendBaseGamePacket)new opcode_542_ACK());
        }
    }
}
