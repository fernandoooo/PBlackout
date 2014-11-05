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
    public class S_SHOP_TEST_1 : SendBaseGamePacket
    {
        public S_SHOP_TEST_1()
        {
            base.makeme();
        }
        protected internal override void write()
        {
            base.writeH(0x22F);
            base.writeD(0);
            base.writeD(0);
            base.writeD(0);
            base.writeD(353);
        }
    }
}