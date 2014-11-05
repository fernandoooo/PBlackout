using PBServer;
using PBServer.src.model;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;

namespace PBServer.network.serverpackets
{
    public class S_ROOM_LOBBY_USER_LIST : SendBaseGamePacket
    {
        private Channel client;
        public S_ROOM_LOBBY_USER_LIST(Channel ch)
        {
            this.makeme();
            this.client = ch;
        }

        protected internal override void write()
        {
            int count_users_list;
            if (this.client.getWaitPlayers().Count == 0)
            {
                count_users_list = 0;
            }
            else
                if (this.client.getWaitPlayers().Count > 8)
                {
                    count_users_list = 8;
                }
                else
                {
                    count_users_list = this.client.getWaitPlayers().Count;
                }
            this.writeH((short)3855);
            this.writeD(count_users_list);
            int len = count_users_list;
            for (int i = 0; i < len; i++)
            {
                Account account = this.client.getWaitPlayers()[i];
                this.writeD(1);
                this.writeD(account.getRank());
                this.writeC((byte)33);
                this.writeS(account.getPlayerName(), 33);
            }
        }
    }
}
