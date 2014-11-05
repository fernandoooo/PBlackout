using PBServer;
using PBServer.network.Game.packets.serverpackets;

namespace PBServer.network.Game.packets.clientpackets
{
    public class opcode_3604_REQ : ReceiveBaseGamePacket
    {

        private int unk;
        private int unk1;
        private string unk2;
        public opcode_3604_REQ(GameClient Client, byte[] data)
        {
            this.makeme(Client, data);
        }

        protected internal override void read()
        {
            this.unk = readH();
            this.unk1 = readD();
            this.unk2 = readS();
            CLogger.getInstance().info("unk " + this.unk);
            CLogger.getInstance().info("unk1 " + this.unk1);
            CLogger.getInstance().info("unk2 " + this.unk2);
        }

        protected internal override void run()
        {
            this.getClient().sendPacket((SendBaseGamePacket)new S_RINVITE_FROM_ROOM());
        }
    }
}
