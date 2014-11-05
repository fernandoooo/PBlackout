// Type: PBServer.network.serverpackets.PROTOCOL_INVENTORY_ENTER_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.src.managers;
using System;

namespace PBServer.network.serverpackets
{
    public class S_INVENTORY_ENTER : SendBaseGamePacket
    {
        private int p_id;
        private GameClient gm;
        public S_INVENTORY_ENTER(int id, GameClient gm)
        {
            this.makeme();
            this.p_id = id;
            this.gm = gm;
        }

        protected internal override void write()
        {
            this.writeH((short)0xE02);
            this.writeD(Convert.ToInt32(DateTime.Now.ToString("yyMMddHHmm")));
            DaoManager daoManager = new DaoManager(this.gm);
            daoManager.getInventory(this.p_id);
        }
    }
}