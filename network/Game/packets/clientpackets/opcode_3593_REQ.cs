using PBServer;
using PBServer.network;
using PBServer.network.serverpackets;
using PBServer.src.model.accounts;
using PBServer.network.Game.packets.serverpackets;

namespace PBServer.network.Game.packets.clientpackets
{
    internal class opcode_3593_REQ : ReceiveBaseGamePacket
    {
        private string _pass;

        public opcode_3593_REQ(GameClient Client, byte[] data)
        {
            this.makeme(Client, data);
        }

        protected internal override void read()
        {
            int num = (int)this.readH();
            this._pass = this.readS(4);
        }

        protected internal override void run()
        {
            Account player = this.getClient().getPlayer();
            player.getRoom().password = this._pass;
            foreach (Account account in player.getRoom().getAllPlayers())
            account.sendPacket((SendBaseGamePacket)new S_ROOM_CHANGE_PASS(this._pass));
        }
    }
}
