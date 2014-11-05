using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PBServer;
using PBServer.network.Game.packets.serverpackets;
using PBServer.src.managers;
using PBServer.network.serverpackets;
using PBServer.src.model.accounts;
using PBServer.model.players;
using PBServer.data.model;

namespace PBServer.network.Game.packets.clientpackets
{
    public class opcode_SHOP_BUY_REQ : ReceiveBaseGamePacket
    {
        private int _unk;
        private int _unk1;
        private int _item;
        private int _unk2;
        private int _unk3;
        private long _unk4;
        public int id_player;
        public string _item_name;
        public int _count;
        public int _equip;
        public int _pgold;
        public int _pcash;
        public opcode_SHOP_BUY_REQ(GameClient gc, byte[] buff)
        {
            base.makeme(gc, buff);
            GameClient client = base.getClient();
            this.id_player = client.getPlayerId();
        }
        protected internal override void read()
        {
            this._unk = this.readH();
            this._unk1 = (int)base.readC();
            this._item = base.readD();
            this._unk2 = (int)base.readC();
            this._unk3 = base.readD(); //base.readD()
            this._unk4 = base.readQ();
            if (ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() < 600000000)
                this._unk1 = 1;

            if (ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() < 700000000 &&
            ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() > 600000000)
                this._unk2 = 2; //PISTOLA

            if (ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() < 800000000 &&
            ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() > 700000000)
                this._unk2 = 3; //FACA

            if (ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() < 900000000 &&
            ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() > 800000000)
                this._unk2 = 4; //GRANADA

            if (ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() < 1000000000 &&
            ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() > 900000000)
                this._unk2 = 5; //ESPECIAL

            if (ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() < 1001002000 &&
            ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() > 1001001000)
                this._unk2 = 6; //TIME VERMELHO

            if (ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() < 1001003000 &&
            ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() > 1001002000)
                this._unk2 = 7; //TIME AZUL

            if (ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() < 1104004000 &&
            ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() > 1104003000)
                this._unk2 = 8; //MASCARA

            if (ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() < 1105004000 &&
            ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() > 1105003000)
                this._unk2 = 8; //MASCARA

            if (ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() < 1102004000 &&
            ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() > 1102003000)
                this._unk2 = 8; //MASCARA

            if (ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() < 1103004000 &&
            ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() > 1103003000)
                this._unk2 = 10; //BOINA

            if (ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() < 1006004000 &&
            ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() > 1006001000)
                this._unk2 = 9; //DINO

            if (ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() < 1301510000 &&
            ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() > 1300002000)
                this._unk2 = 11; //CUPON

            if (ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() < 600000000)
                this._unk1 = 1; //PISTOLA

            if (ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() < 700000000 &&
            ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() > 600000000)
                this._unk1 = 1; //PISTOLA

            if (ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() < 800000000 &&
            ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() > 700000000)
                this._unk1 = 1; //FACA

            if (ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() < 900000000 &&
            ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() > 800000000)
                this._unk1 = 1; //GRANADA

            if (ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() < 1000000000 &&
            ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() > 900000000)
                this._unk1 = 1; //ESPECIAL

            if (ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() < 1001002000 &&
            ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() > 1001001000)
                this._unk1 = 2; //TIME VERMELHO

            if (ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() < 1001003000 &&
            ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() > 1001002000)
                this._unk1 = 2; //TIME AZUL

            if (ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() < 1104004000 &&
            ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() > 1104003000)
                this._unk1 = 2; //MASCARA

            if (ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() < 1102004000 &&
            ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() > 1102003000)
                this._unk1 = 2; //MASCARA

            if (ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() < 1102004000 &&
            ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() > 1102003000)
                this._unk1 = 2; //MASCARA

            if (ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() < 1105004000 &&
            ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() > 1105003000)
                this._unk1 = 2; //CHAPEU

            if (ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() < 1103004000 &&
            ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() > 1103003000)
                this._unk1 = 2; //BOINA

            if (ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() < 1006004000 &&
            ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() > 1006001000)
                this._unk1 = 2; //DINO

            if (ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() < 1301510000 &&
            ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId() > 1300002000)
                this._unk1 = 3; //CUPON

            this._item_name = ShopInfoManager.getInstance().getInfoItem2(this._item).getItemName();
            this._count = ShopInfoManager.getInstance().getInfoItem2(this._item).getItemCount();
            this._equip = ShopInfoManager.getInstance().getInfoItem2(this._item).getItemEquip();
            this._pgold = ShopInfoManager.getInstance().getInfoItem2(this._item).getItemGold();
            this._pcash = ShopInfoManager.getInstance().getInfoItem2(this._item).getItemCash();
            CLogger.getInstance().warning("O jogador " + this.getClient().getPlayer().getPlayerName().ToString() + " comprou um equipamento NOME | " + ShopInfoManager.getInstance().getInfoItem(ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId()).getItemName() + " |");
        }
        protected internal override void run()
        {
            if (ShopInfoManager.getInstance().getInfoItem2(this._item) == null || this.getClient().getPlayer().getGP() < this._pgold || this.getClient().getPlayer().getMoney() < this._pcash)
            {
                this.getClient().sendPacket(new S_SHOP_BUY_ITEM(int.MaxValue, null, null));
            }
            else
            {
                this.getClient().getPlayer().setGP(this.getClient().getPlayer().getGP() - this._pgold);
                this.getClient().getPlayer().setMoney(this.getClient().getPlayer().getMoney() - this._pcash);
                AccountManager.getInstance().UpdateMGP(this.getClient().getPlayer().getPlayerId(), this.getClient().getPlayer().getGP(), this.getClient().getPlayer().getMoney());
                this.getClient().sendPacket((SendBaseGamePacket)new S_SHOP_BUY_ITEM(0, ShopInfoManager.getInstance().getInfoItem2(this._item), this.getClient().getPlayer()));
                this.getClient().sendPacket(new S_CREATE_EQUIPMENT(ShopInfoManager.getInstance().getInfoItem2(this._item).getItemId(), this._unk2, this.id_player, this._item_name, this._count, this._equip));
            }
        }
    }
}
