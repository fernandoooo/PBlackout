using PBServer;
using PBServer.network.serverpackets;
using PBServer.network.Game.packets.serverpackets;
using PBServer.src.managers;

namespace PBServer.network.clientpacket
{
    public class CM_FRIEND_INVITE : ReceiveBaseGamePacket
    {
        private string _player_name;
        public CM_FRIEND_INVITE(GameClient Client, byte[] data)
        {
            this.makeme(Client, data);
        }

        protected internal override void read()
        {
            base.readH();
            this._player_name = base.readS(33);
            CLogger.getInstance().info("Nome do amigo: " + this._player_name);
        }

        protected internal override void run()
        {
            if (this.getClient() == null)
                return;
            if (AccountManager.getInstance().getAccountInName(this._player_name) != null)
            {
                FriendManager.getInstance().AddFriend(AccountManager.getInstance().getAccountInName(this._player_name).getPlayerId(), this.getClient().getPlayerId());
                FriendManager.getInstance().UpdateFriendsCount(this.getClient().getPlayer().getPlayerId(), this.getClient().getPlayer().getCount_Friend() + 1);
                this.getClient().sendPacket((SendBaseGamePacket)new SM_FRIEND_INVITE(this._player_name, 0));
            }
            else
            {
                this.getClient().sendPacket((SendBaseGamePacket)new SM_FRIEND_INVITE("", int.MaxValue));
            }
        }
    }
}
