using PBServer;
using PBServer.src.model.rooms;
using PBServer.src.model.accounts;
using PBServer.network.Game.packets.serverpackets;
using System.IO;

namespace PBServer.network.Game.packets.clientpackets
{
    public class opcode_3608_REQ : ReceiveBaseGamePacket
    {

        private int limit;
        private int seeConf;
        private int autobalans;
        private int killtime;
        private int unk;
        private byte[] unk2;

        public opcode_3608_REQ(GameClient Client, byte[] data)
        {
            this.makeme(Client, data);
        }

        protected internal override void read()
        {
            this.unk = readH();
            this.unk2 = readB(33);
            this.killtime = readD();
            this.limit = readC();
            this.seeConf = readC();
            this.autobalans = readH();
        }

        protected internal override void run()
        {
            Account p = this.getClient().getPlayer();
            p.getRoom().autobalans = this.autobalans;
            p.getRoom().seeConf = this.seeConf;
            p.getRoom().killtime = this.killtime;
            p.getRoom().limit = this.limit;
            foreach (Account account in p.getRoom().getAllPlayers())
            account.sendPacket((SendBaseGamePacket)new opcode_3608_ACK(this.autobalans, this.seeConf, this.killtime, this.limit));
        }
    }
}
