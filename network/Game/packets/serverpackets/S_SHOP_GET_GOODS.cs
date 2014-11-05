// Type: PBServer.network.Game.packets.serverpackets.opcode_523_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using System;
using System.Globalization;
using PBServer.src.templates;
using PBServer.src.data.xml.parsers;
using PBServer.src.data.xml.holders;
using System.Collections.Generic;
using PBServer.src.model;
using PBServer.src.managers;
using PBServer.src.model.accounts;

namespace PBServer.network.Game.packets.serverpackets
{
    public class S_SHOP_GET_GOODS : SendBaseGamePacket
    {
        public S_SHOP_GET_GOODS()
        {
            base.makeme();
        }
        protected internal override void write()
        {
            base.writeH(0x20B);
            List<ShopInfo> allWeapons = ShopInfoManager.getInstance().getShopItens();
            base.writeD(allWeapons.Count);
            base.writeD(allWeapons.Count);
            base.writeD(0);
            foreach (ShopInfo shop in allWeapons)
            {
                base.writeD(shop.getGoodId()); //WEAPON_ID
                base.writeC(1);
                base.writeC(1); //3
                base.writeD(shop.getItemGold()); //PRICE_MONEY
                base.writeD(shop.getItemCash()); //PRICE_PTS
                base.writeC(Convert.ToByte(shop.getTag()));
            }
            base.writeD(356);
        }
    }
}
