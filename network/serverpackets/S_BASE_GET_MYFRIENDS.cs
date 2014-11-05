using PBServer;
using PBServer.src.model.accounts;
using PBServer.src.model.friends;
using PBServer.src.managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PBServer.network.serverpackets
{
    public class S_BASE_GET_MYFRIENDS : SendBaseLoginPacket
    {
        private LoginClient _lc;
        public S_BASE_GET_MYFRIENDS(LoginClient lc)
        {
            this.makeme();
            this._lc = lc;
        }

        protected internal override void write()
        {
            Account account = AccountManager.getInstance().get(this._lc.getLogin());
            if (account.getCount_Friend() == 0)
            {

            }
            else
            {
                foreach (Friends friend in FriendManager.getInstance().getFriends(account.getPlayerId()))
                {
                    account.addFriend(friend);
                }
            }
            this.writeH((short)0x112);
            this.writeC((byte)account.getCount_Friend()); //QUANTIDADE DE JOGADORES
            if (account.getCount_Friend() == 0)
            {
                this.writeC((byte)33);
                this.writeS("", 33); //Nome do jogador
                this.writeC(47);
                this.writeC(66);
                this.writeC(9);
                this.writeC(0);
                this.writeC(0);
                this.writeC(0);
                this.writeC(0);
                this.writeC(0);
                this.writeC(0);
                this.writeC(0);
                this.writeC(0);
                this.writeC(48);
                this.writeC(0); //Rank do jogador
                this.writeC(84);
                this.writeC(195);
                this.writeC(164);
            }
            else
            {
                foreach (Friends friend in account.friends)
                {
                    this.writeC((byte)33);
                    this.writeS(Convert.ToString(AccountManager.getInstance().getAccountInPlayerId(friend.getFriendId()) == null || account == null ? "" : AccountManager.getInstance().getAccountInPlayerId(friend.getFriendId()).getPlayerName()), 33);
                    this.writeC(1);
                    this.writeC(1);
                    this.writeC(3);
                    this.writeC(0); //Colocado agr
                    this.writeC(0);
                    this.writeC(0); //0
                    this.writeC(0);
                    this.writeC(0);
                    this.writeC(0);
                    this.writeC(0);
                    this.writeC(0);
                    this.writeC(48);
                    this.writeC(Convert.ToByte(AccountManager.getInstance().getAccountInPlayerId(friend.getFriendId()) == null || account == null ? 0 : AccountManager.getInstance().getAccountInPlayerId(friend.getFriendId()).getRank())); // Ранк (Звание)
                    this.writeC(84);
                    this.writeC(195);
                    this.writeC(164);
                }
            }
        }
    }
}
