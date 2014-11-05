using PBServer;
using PBServer.src.model.rooms;
using PBServer.src.model.accounts;
using PBServer.network.Game.packets.serverpackets;
using System.IO;

namespace PBServer.network.Game.packets.clientpackets
{
    public class opcode_3620_REQ : ReceiveBaseGamePacket
    {

        private int unk;
        public opcode_3620_REQ(GameClient Client, byte[] data)
        {
            this.makeme(Client, data);
        }

        protected internal override void read()
        {
            this.unk = readH();
        }

        protected internal override void run()
        {
            Account p = this.getClient().getPlayer();
            p.sendPacket((SendBaseGamePacket)new opcode_3620_ACK());
        }
    }
}
