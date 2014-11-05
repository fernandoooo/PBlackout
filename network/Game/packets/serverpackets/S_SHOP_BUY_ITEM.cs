using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PBServer;
using PBServer.src.managers;
using PBServer.src.templates;
using PBServer.src.model.accounts;
using PBServer.model.players;
using MySql.Data.MySqlClient;
using PBServer.db;
using System.Data;
using System.Data.Common;

namespace PBServer.network.Game.packets.serverpackets
{
    public class S_SHOP_BUY_ITEM : SendBaseGamePacket
    {
        private ShopInfo item;
        private Account player;
        private int error;
        private int _slot;
        public S_SHOP_BUY_ITEM(int error, ShopInfo item, Account player)
        {
            base.makeme();
            this.error = error;
            this.item = item;
            this.player = player;
        }
        protected internal override void write()
        {
            if (this.item.getItemId() < 700000000 &&
            this.item.getItemId() > 600000000)
                _slot = 2; //PISTOLA

            if (this.item.getItemId() < 800000000 &&
            this.item.getItemId() > 700000000)
                _slot = 3; //FACA

            if (this.item.getItemId() < 900000000 &&
            this.item.getItemId() > 800000000)
                _slot = 4; //GRANADA

            if (this.item.getItemId() < 1000000000 &&
            this.item.getItemId() > 900000000)
                _slot = 5; //ESPECIAL

            if (this.item.getItemId() < 1001002000 &&
            this.item.getItemId() > 1001001000)
                _slot = 6; //TIME VERMELHO

            if (this.item.getItemId() < 1001003000 &&
            this.item.getItemId() > 1001002000)
                _slot = 7; //TIME AZUL

            if (this.item.getItemId() < 1104004000 &&
            this.item.getItemId() > 1104003000)
                _slot = 8; //MASCARA

            if (this.item.getItemId() < 1105004000 &&
            this.item.getItemId() > 1105003000)
                _slot = 8; //MASCARA

            if (this.item.getItemId() < 1102004000 &&
            this.item.getItemId() > 1102003000)
                _slot = 8; //MASCARA

            if (this.item.getItemId() < 1103004000 &&
            this.item.getItemId() > 1103003000)
                _slot = 10; //BOINA

            if (this.item.getItemId() < 1006004000 &&
            this.item.getItemId() > 1006001000)
                _slot = 9; //DINO

            if (this.item.getItemId() < 1301510000 &&
            this.item.getItemId() > 1300002000)
                _slot = 11; //CUPON

            base.writeH((short)531);
            if (this.error == 0)
            {
                base.writeD(1);
                base.writeD(Convert.ToInt32(DateTime.Now.ToString("yyMMddHHmm")));
                base.writeD(_slot >= 5 && _slot < 10 ? 1 : 0); //количество купленного - Оружие|солдат|купоны
                base.writeD(_slot < 5 ? 1 : 0); //количество купленного - Оружие|солдат|купоны
                base.writeD(0); //количество купленного - Оружие|солдат|купоны
                base.writeQ(0); //Постоянным айди не пишем
                base.writeD(0); //id
                base.writeC((byte)this.item.getItemEquip()); //settings weapon - type
                base.writeD(this.item.getItemCount()); //settings weapon - count
                base.writeD(this.player.getGP());
                base.writeD(this.player.getMoney());
            }
            else
            {
                base.writeD(this.error);
            }
        }
    }
}
