// Type: PBServer.network.Game.packets.serverpackets.opcode_525_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.src.templates;
using PBServer.src.data.xml.parsers;
using PBServer.src.data.xml.holders;
using System;
using System.Globalization;
using System.Collections.Generic;
using PBServer.src.managers;
using PBServer.src.model.accounts;

namespace PBServer.network.Game.packets.serverpackets
{
    public class S_SHOP_GET_ITEMS : SendBaseGamePacket
    {
        public S_SHOP_GET_ITEMS()
        {
            base.makeme();
        }
        protected internal override void write()
        {
            base.writeH(0x20D);
            //this._holder = ShopItemHolder.getInstance();
            List<ShopInfo> allWeapons = ShopInfoManager.getInstance().getShopItens();
            base.writeD(allWeapons.Count); //ARMORS.COUNT
            base.writeD(allWeapons.Count); //WEAPONS.COUNT
            base.writeD(0); //CUPONS.COUNT
            foreach (ShopInfo shop in allWeapons)
            {
                base.writeD(shop.getItemId());
                base.writeB(new byte[]
				{
					Convert.ToByte(shop.getBuyType2()), //1
					Convert.ToByte(shop.getBuyType()), //2 - 3 = Unidades| 2 - Permanente| 4 - ?| 5 - ?
					Convert.ToByte(shop.getBuyType()), //1 - 3 = Unidades| 2 - Permanente| 4 - ?| 5 - ?
					Convert.ToByte(shop.getTitleId()) //0
				});
            }
            base.writeD(356);
        }
    }
}
