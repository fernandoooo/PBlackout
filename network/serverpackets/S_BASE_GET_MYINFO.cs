using PBServer;
using PBServer.data.model;
using PBServer.data.xml.holders;
using PBServer.src.data.xml.holders;
using PBServer.src.managers;
using PBServer.src.templates;
using PBServer.src.model.accounts;
using System;
using System.Collections.Generic;
using PBServer.model.players;
using PBServer.model.clans;
using PBServer.managers;
using PBServer.src.model;

namespace PBServer.network.serverpackets
{

    internal class S_BASE_GET_MYINFO : SendBaseLoginPacket
    {
        private LoginClient _lc;
        public S_BASE_GET_MYINFO(LoginClient lc)
        {
            this._lc = lc;
            base.makeme();
        }

        protected internal override void write()
        {
            int num;
            Account account = AccountManager.getInstance().get(this._lc.getLogin());
            Clan clan = ClanManager.getInstance().get(account.getClanId());
            if ((account.getPlayerName() == null) || (account.getPlayerName() == ""))
            {
                PlayerTemplate template = PlayerTemplateHolder.getPlayerTemplate(Config.PlayerTemplateId);
                account.setRank(template._rank);
                account.setExp(template._exp);
                account.setGP(template._gp);
            }
            base.writeH(2566);
            base.writeD(0);
            base.writeC(0xdd);
            base.writeS(account.getPlayerName(), 33);
            base.writeD(account.getExp());
            base.writeD(account.getRank());
            base.writeD(0);
            base.writeD(account.getGP());
            base.writeD(account.getMoney());
            base.writeB(new byte[] { 0x11, 1, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0 });
            base.writeD(0); //PCCAFE
            base.writeH(0);
            base.writeS(Convert.ToString(account == null || clan == null ? "" : clan.getClanName()), 16); //ClanName Test
            base.writeC((byte)0);
            base.writeH((short)(account == null || clan == null ? 0 : clan.getClanRank())); //Clan-Rank Test
            base.writeC(Convert.ToByte(account == null || clan == null ? 255 : clan.getLogo1())); //Logo1 Test
            base.writeC(Convert.ToByte(account == null || clan == null ? 255 : clan.getLogo2())); //Logo2 Test
            base.writeC(Convert.ToByte(account == null || clan == null ? 255 : clan.getLogo3())); //Logo3 Test
            base.writeC(Convert.ToByte(account == null || clan == null ? 255 : clan.getLogo4())); //Logo4 Test
            base.writeH((short)(account == null || clan == null ? 0 : clan.getLogoColor())); //Color Test - H
            base.writeD(0);
            base.writeD(0);
            base.writeD(0);
            base.writeD(account._statistic.getFights(true));
            base.writeD(account._statistic.getWinFights(true)); //Rodadas
            base.writeD(account._statistic.getLostFights(true)); //Vítorias
            base.writeD(0); //Derrotas
            base.writeD(account._statistic.getKills(true)); //Kills
            base.writeD(0);
            base.writeD(account._statistic.getDeaths(true)); //Deaths
            base.writeD(0);
            base.writeD(0);
            base.writeD(account._statistic.getEscapes(true)); //Desistências
            base.writeD(account._statistic.getFights(true)); //partidas
            base.writeD(account._statistic.getWinFights(true)); //PARTIDAS GANHAS FIXED
            base.writeD(account._statistic.getLostFights(true)); //PARTIDAS PERDIDAS FIXED
            base.writeD(0);
            base.writeD(account._statistic.getKills(true)); //KILL FIXED
            base.writeD(0);//
            base.writeD(account._statistic.getDeaths(true)); //DEATH FIXED
            base.writeD(0);
            base.writeD(0); //HEADSHOT TOMADOS
            base.writeD(account._statistic.getEscapes(true));
            //base.writeD(0);
            account.setStatus(1);
            AccountManager.getInstance().UpdateStatus(account.getPlayerId(), 1); //ADICIONAR STATUS ONLINE
            if ((account.getPlayerName() == null) || (account.getPlayerName() == ""))
            {
            }
            account.CheckCorrectInventory();
            base.writeD(account.getCharRed());//account.getEquepItem(6)
            base.writeD(account.getCharBlue());//account.getEquepItem(7)
            base.writeD(account.getCharHelmet());//account.getEquepItem(8)
            base.writeD(account.getCharBeret());//account.getEquepItem(10)
            base.writeD(account.getCharDino());//account.getEquepItem(9)
            base.writeD(account.getPrimaryWeapon());//account.getEquepItem(1)
            base.writeD(account.getSecondaryWeapon());//account.getEquepItem(2)
            base.writeD(account.getMeleeWeapon());//account.getEquepItem(3)
            base.writeD(account.getThrownNormalWeapon());//account.getEquepItem(4)
            base.writeD(account.getThrownSpecialWeapon());//account.getEquepItem(5)
            base.writeB(new byte[41]);
            if ((account.getPlayerName() == null) || (account.getPlayerName() == ""))
            {
                base.writeC(1);
            }
            else
            {
                base.writeC(1);
            }
            if ((account.getPlayerName() == null) || (account.getPlayerName() == ""))
            {
                base.writeD(account.getInvetoryOnlyEquip(2).Count);
                base.writeD(account.getInvetoryOnlyEquip(1).Count);
                base.writeD(account.getInvetoryOnlyEquip(3).Count);
                base.writeD(0);
            }
            else
            {
                base.writeD(account.getInvetoryOnly(2).Count);
                base.writeD(account.getInvetoryOnly(1).Count);
                base.writeD(account.getInvetoryOnly(3).Count);
                base.writeD(0);
            }
            if ((account.getPlayerName() == null) || (account.getPlayerName() == ""))
            {
                for (num = 0; num < account.getInvetoryOnlyEquip(2).Count; num++)
                {
                    base.writeD(account.getInvetoryOnlyEquip(2)[num].id); //0
                    base.writeD(account.getInvetoryOnlyEquip(2)[num].id); //0
                    base.writeD(account.getInvetoryOnlyEquip(2)[num].id);
                    this.writeC((byte)account.getInvetoryOnlyEquip(2)[num].equip_type);
                    this.writeD(account.getInvetoryOnlyEquip(2)[num].count);
                }
                for (num = 0; num < account.getInvetoryOnlyEquip(1).Count; num++)
                {
                    base.writeD(account.getInvetoryOnlyEquip(1)[num].id); //0
                    base.writeD(account.getInvetoryOnlyEquip(1)[num].id); //0
                    base.writeD(account.getInvetoryOnlyEquip(1)[num].id);
                    this.writeC((byte)account.getInvetoryOnlyEquip(1)[num].equip_type);
                    this.writeD(account.getInvetoryOnlyEquip(1)[num].count);
                }
                for (num = 0; num < account.getInvetoryOnlyEquip(3).Count; num++)
                {
                    base.writeD(account.getInvetoryOnlyEquip(3)[num].id); //0
                    base.writeD(account.getInvetoryOnlyEquip(3)[num].id); //0
                    base.writeD(account.getInvetoryOnlyEquip(3)[num].id);
                    this.writeC((byte)account.getInvetoryOnlyEquip(3)[num].equip_type);
                    this.writeD(account.getInvetoryOnlyEquip(3)[num].count);
                }
                for (num = 0; num < account.getInvetoryOnlyEquip(4).Count; num++)
                {
                    base.writeD(account.getInvetoryOnlyEquip(4)[num].id); //0
                    base.writeD(account.getInvetoryOnlyEquip(4)[num].id); //0
                    base.writeD(account.getInvetoryOnlyEquip(4)[num].id);
                    this.writeC((byte)account.getInvetoryOnlyEquip(4)[num].equip_type);
                    this.writeD(account.getInvetoryOnlyEquip(4)[num].count);
                }
            }
            else
            {
                num = 0;
                while (num < account.getInvetoryOnly(2).Count)
                {
                    base.writeD(account.getInvetoryOnly(2)[num].id); //0
                    base.writeD(account.getInvetoryOnly(2)[num].id); //0
                    base.writeD(account.getInvetoryOnly(2)[num].id);
                    this.writeC((byte)account.getInvetoryOnly(2)[num].equip_type);
                    this.writeD(account.getInvetoryOnly(2)[num].count);
                    num++;
                }
                for (num = 0; num < account.getInvetoryOnly(1).Count; num++)
                {
                    base.writeD(account.getInvetoryOnly(1)[num].id); //0
                    base.writeD(account.getInvetoryOnly(1)[num].id); //0
                    base.writeD(account.getInvetoryOnly(1)[num].id);
                    this.writeC((byte)account.getInvetoryOnly(1)[num].equip_type);
                    this.writeD(account.getInvetoryOnly(1)[num].count);
                }
                for (num = 0; num < account.getInvetoryOnly(3).Count; num++)
                {
                    base.writeD(account.getInvetoryOnly(3)[num].id); //0
                    base.writeD(account.getInvetoryOnly(3)[num].id); //0
                    base.writeD(account.getInvetoryOnly(3)[num].id);
                    this.writeC((byte)account.getInvetoryOnly(3)[num].equip_type);
                    this.writeD(account.getInvetoryOnly(3)[num].count);
                }
                for (num = 0; num < account.getInvetoryOnly(4).Count; num++)
                {
                    base.writeD(account.getInvetoryOnly(4)[num].id); //0
                    base.writeD(account.getInvetoryOnly(4)[num].id); //0
                    base.writeD(account.getInvetoryOnly(4)[num].id);
                    this.writeC((byte)account.getInvetoryOnly(4)[num].equip_type);
                    this.writeD(account.getInvetoryOnly(4)[num].count);
                }
            }
            base.writeC((byte)Config.OutpostEnable); //OUTPOST ENABLE
            base.writeD(account.getBrooch()); //Broche
            base.writeD(account.getInsignia()); //Insígnia
            base.writeD(account.getMedal()); //Medalha
            base.writeD(account.getBlueOrder()); //Ordem Azul
            base.writeB(new byte[] { 
              0x00, //текущий номер карточки
				0x01, 0x00, 0x00, 0x00,
				(byte) 0xFF, (byte) 0xFF, (byte) 0xFF, (byte) 0xFF,
				(byte) 0xFF, (byte) 0xFF, (byte) 0xFF, (byte) 0xFF,
				(byte) 0xFF, (byte) 0xFF, (byte) 0xFF, (byte) 0xFF,
				(byte) 0xFF, (byte) 0xFF, (byte) 0xFF, (byte) 0xFF,
				(byte) 0xFF, (byte) 0xFF, (byte) 0xFF, (byte) 0xFF,
				0x00, 0x00, 0x00, 0x00,
				0x00, 0x00, 0x00, 0x00,
				0x00, 0x00, 0x00, 0x00,
				0x00, 0x00, 0x00, 0x00,
				0x00, 0x00, 0x00, 0x00,
				0x00, 0x00, 0x00, 0x00,
				0x00, 0x00, 0x00, 0x00,
				0x00, 0x00, 0x00, 0x00,
				0x00, 0x00, 0x00, 0x00,
				0x00, 0x00, 0x00, 0x00,
				0x00, 0x00, 0x00, 0x00,
				0x00, 0x00, 0x00, 0x00,
				0x00, 0x00, 0x00, 0x00,
				0x00, 0x00, 0x00, 0x00,
				0x00, 0x00, 0x01, 0x00,
				0x00, 0x00, 0x00, 0x00,
             });
            base.writeB(new byte[] { 
               0x01, 0x01, 0x01, 0x01, 0x02, 0x02, //выполнение карточек(килов входов(количество)
				0x01, 0x01, 0x02, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x02, 0x02, 0x01, 0x01, 0x01, 0x01, 0x02,
				0x01, 0x03, 0x01, 0x00, 0x01, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, //0x00
				0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
             });
            base.writeB(new byte[8]);
            base.writeB(new byte[3]);
            base.writeD(1); //QUANTIDADE DE SLOTS DE TITULOS
            base.writeB(new byte[] { 
                0x00, 0x00, 0x00, 0x00, 0x39, 0x00, 0x00, 0x00, 0x19, 0x00, 0x00, 0x00, 0x23, 0x00, 0x00, 0x00,
				0x01, 0x00, 0x00, 0x00, 0x27, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, //52 байта
				0x00, 0x28, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x2C, 0x00, 0x00,
				0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
             });
            base.writeC(60); //60
            base.writeC(2); //TYPE_WEAPON? - 2
            base.writeB(new byte[] { 254, 255, 254, 191, 207, 117, 7, 2 }); //MAPAS SÃO CONTROLADOS AQUI! - 254, 255, 254, 191, 207, 117, 7, 2(NOVOS MAPAS DEATHMATCH |GrandBazzar, GhostTown|)
            base.writeB(new byte[] { 
                0, 0, 0x8d, 1, 0x88, 0, 0x89, 0, 0x8d, 0, 0x8d, 0, 0x8d, 0, 0x8d, 1, 
                9, 0, 1, 0, 0, 0, 0x8d, 0, 0x80, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 140, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 0, 0, 0, 0, 0, 
                8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0
             });
            base.writeB(new byte[] { 
                0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, //0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, //0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, //0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0,
                1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0
             });
            base.writeB(new byte[] { 
                1, 0xee, 0xdf, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 5, 0, 
                110, 0x6f, 110, 0x65, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
             });
        }
    }
}

