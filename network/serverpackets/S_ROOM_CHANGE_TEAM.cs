using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PBServer;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;

namespace PBServer.network.serverpackets
{
    public class S_ROOM_CHANGE_TEAM : SendBaseGamePacket
    {
        private Account _p;
        private Room _r;
        private int _oldSlot;

        public S_ROOM_CHANGE_TEAM(int oldSlot, Account player)
        {
            this.makeme();
            this._p = player;
            this._r = this._p.getRoom();
            this._oldSlot = oldSlot;
        }

        protected internal override void write()
        {
            this.writeH((short)0xF25);
            this.writeC((byte)0);
            this.writeC((byte)this._r.getLeader().getSlot());
            this.writeC((byte)1);
            this.writeC((byte)this._oldSlot);
            this.writeC((byte)this._p.getSlot());
            this.writeC((byte)this._r.getSlotState(this._oldSlot));
            this.writeC((byte)this._r.getSlotState(this._p.getSlot()));
        }
    }
}
