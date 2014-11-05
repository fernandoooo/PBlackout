using PBServer;
using PBServer.model.clans;
using System.Collections.Generic;
using PBServer.src.model.accounts;
using PBServer.managers;
using PBServer.src.managers;
using System;

namespace PBServer.network.Game.packets.serverpackets
{
    public class S_BASE_GET_MYCLANP : SendBaseLoginPacket
    {
        private List<Account> players_in_clan;
        private LoginClient _lc;
        public S_BASE_GET_MYCLANP(LoginClient lc)
        {
            this.makeme();
            this._lc = lc;
        }

        protected internal override void write()
        {
            this.writeH((short)1349);
            Account player = AccountManager.getInstance().get(this._lc.getLogin());
            this.players_in_clan = ClanManager.getInstance().getClanPlayers(player.getClanId());
            this.writeC(Convert.ToByte(this.players_in_clan.Count - 1));

            if (player.getClanId() == 0)
            {
                this.writeC((byte)33); //QUANTIDADE DE CARACTERES DO NOME DO JOGADOR
                this.writeS("", 33);
                this.writeB(new byte[17]);
            }
            else
            {
                foreach (Account players_in_clan2 in players_in_clan)
                {
                    if (players_in_clan2.getPlayerName() == player.getPlayerName())
                    {
                    }
                    else
                    {
                        this.writeC((byte)33);
                        this.writeS(players_in_clan2.getPlayerName(), 33);
                        this.writeB(new byte[16]);
                        this.writeC(Convert.ToByte(players_in_clan2.getRank()));
                    }
                }
            }
        }
    }
}
