// Type: PBServer.network.Game.packets.serverpackets.PROTOCOL_ROOM_SLOT_INFO_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;
using System;
using PBServer.model.clans;
using PBServer.managers;
namespace PBServer.network.Game.packets.serverpackets
{
    public class S_SLOT_INFO : SendBaseGamePacket
    {
        private Room room;
        public S_SLOT_INFO(Room r)
        {
            this.room = r;
            this.makeme();
        }

        protected internal override void write()
        {
            base.writeH((short)0xF15);
            if (room.getLeader() == null)
                room.setNewLeader(0);

            base.writeD(room.getLeader().getSlot());
            for (int i = 0; i < 16; i++)
            {
                Account playerBySlot = room.getPlayerBySlot(i);
                base.writeC((byte)room.getSlotState(i));
                base.writeC(Convert.ToByte(playerBySlot == null ? 0 : playerBySlot.getRank()));
                base.writeB(new byte[9]);
                base.writeC(Convert.ToByte(playerBySlot == null || ClanManager.getInstance().get(playerBySlot.getClanId()) == null ? 255 : ClanManager.getInstance().get(playerBySlot.getClanId()).getLogo1()));
                base.writeC(Convert.ToByte(playerBySlot == null || ClanManager.getInstance().get(playerBySlot.getClanId()) == null ? 255 : ClanManager.getInstance().get(playerBySlot.getClanId()).getLogo2()));
                base.writeC(Convert.ToByte(playerBySlot == null || ClanManager.getInstance().get(playerBySlot.getClanId()) == null ? 255 : ClanManager.getInstance().get(playerBySlot.getClanId()).getLogo3()));
                base.writeC(Convert.ToByte(playerBySlot == null || ClanManager.getInstance().get(playerBySlot.getClanId()) == null ? 255 : ClanManager.getInstance().get(playerBySlot.getClanId()).getLogo4()));
                base.writeC(Convert.ToByte(playerBySlot == null || ClanManager.getInstance().get(playerBySlot.getClanId()) == null ? 0 : ClanManager.getInstance().get(playerBySlot.getClanId()).getLogoColor()));
                base.writeB(new byte[6]);
                base.writeS(playerBySlot == null || ClanManager.getInstance().get(playerBySlot.getClanId()) == null ? "" : ClanManager.getInstance().get(playerBySlot.getClanId()).getClanName(), 17);
                base.writeH(0);
                base.writeC(0);
            }
        }
    }
}
