using PBServer.network.serverpackets;
using PBServer;
using PBServer.network;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;

namespace PBServer.network.clientpacket
{
    internal class PROTOCOL_ROOM_CHANGE_TEAM_REQ : ReceiveBaseGamePacket
    {
        private int _team;
        private int _oldSlot;

        public PROTOCOL_ROOM_CHANGE_TEAM_REQ(GameClient Client, byte[] data)
        {
            this.makeme(Client, data);
        }

        protected internal override void read()
        {
            int num = (int)this.readH();
            this._team = this.readD();
            CLogger.getInstance().extra_info("slot: " + this._team);
        }

        protected internal override void run()
        {
            Account player = this.getClient().getPlayer();
            Room room = player.getRoom();
            if (this._team < 0 || this._team == player.getSlot() % 2 || player == null)
                return;
            this._oldSlot = player.getSlot();
            player.getRoom().setNewSlot(player, this._team);
            player.sendPacket((SendBaseGamePacket)new S_ROOM_CHANGE_TEAM(this._oldSlot, player));
        }
    }
}
