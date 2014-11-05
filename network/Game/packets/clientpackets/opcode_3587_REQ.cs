using PBServer;
using PBServer.network.Game.packets.serverpackets;

namespace PBServer.network.Game.packets.clientpackets
{
    public class opcode_3587_REQ : ReceiveBaseGamePacket
    {
        private int _slot;

        public opcode_3587_REQ(GameClient gc, byte[] buff)
        {
            this.makeme(gc, buff);
        }

        protected internal override void read()
        {
            this.readH();
            this._slot = this.readD();
        }

        protected internal override void run()
        {
            this.getClient().sendPacket((SendBaseGamePacket)new S_ROOM_GET_PLAYERINFO(this._slot, this.getClient().getPlayer()));
        }
    }
}
