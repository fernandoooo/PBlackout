using PBServer;
using PBServer.network;
using PBServer.network.serverpackets;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;
using PBServer.network.Game.packets.serverpackets;
using PBServer.src.network.gsPacket.serverpackets;

namespace PBServer.network.Game.packets.clientpackets
{
    internal class opcode_3859_REQ : ReceiveBaseGamePacket
    {

        public opcode_3859_REQ(GameClient Client, byte[] data)
        {
            this.makeme(Client, data);
        }

        protected internal override void read()
        {
            int num = (int)this.readH();
        }

        protected internal override void run()
        {
            Account player = this.getClient().getPlayer();
            Room room = player.getRoom();
            GameClient client = this.getClient();
            if (this.getClient() == null)
                return;
            room.changeSlotState(client.getPlayer().getSlot(), SLOT_STATE.SLOT_STATE_NORMAL, true);
            this.getClient().sendPacket((SendBaseGamePacket)new opcode_3859_ACK(16));
            this.getClient().sendPacket((SendBaseGamePacket)new S_BATTLE_ENDBATTLE(player));
        }
    }
}
