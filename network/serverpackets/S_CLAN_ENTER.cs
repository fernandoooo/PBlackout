using PBServer;
using PBServer.model.clans;
using PBServer.model.players;
using PBServer.managers;
using PBServer.src.model.accounts;
using System;
using System.Globalization;
using System.Collections.Generic;

namespace PBServer.network.serverpackets
{
    public class S_CLAN_ENTER : SendBaseGamePacket
    {
        private int clan;
        private List<Clan> clanList;
        private Account player;
        public S_CLAN_ENTER(int c, Account p, List<Clan> clanList)
        {
            this.makeme();
            this.clan = c;
            this.clanList = clanList;
            this.player = p;
        }

        protected internal override void write()
        {
            this.writeH(0x5A2);
            this.writeD(clan); // Página do Clan - this._pag_id
            if (this.player.getClanId() == 0)
            {
                this.writeD(0);
            }
            if (this.player.getClanId() > 0)
            {
                Clan c = ClanManager.getInstance().get(player.getClanId());
                this.writeD(c != null ? c.getOwnerId() == player.getPlayerId() ? 1 : 2 : 0); // Privilégios - 1 [DONO] | 2 [AUX] | 3 [MEMBRO]
            }
            this.writeD(clanList.Count); // número de clans - 0
            this.writeB(ConvertHexStringToByteArray("AA 01 00 80 6C 44 37"));
        }

        public static byte[] ConvertHexStringToByteArray(string hexString)
        {
            if (hexString.Length % 2 != 0)
            {
                ;
            }
            byte[] numArray = new byte[hexString.Length / 2];
            for (int index = 0; index < numArray.Length; ++index)
            {
                string s = hexString.Substring(index * 2, 2);
                numArray[index] = byte.Parse(s, NumberStyles.HexNumber, (IFormatProvider)CultureInfo.InvariantCulture);
            }
            return numArray;
        }
    }
}
