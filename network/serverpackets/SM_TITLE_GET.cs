using PBServer;
using System;
using PBServer.src.managers;
using PBServer.src.model.accounts;

namespace PBServer.network.serverpackets
{
    public class SM_TITLE_GET : SendBaseGamePacket
    {
        private int _errNo, _OpenedSlot;
        public SM_TITLE_GET(int errNo, int OpenedSlot)
        {
            this.makeme();
            this._errNo = errNo;
            this._OpenedSlot = OpenedSlot;
        }

        protected internal override void write()
        {
            this.writeH((short)0xA3C);
            this.writeD(this._errNo);
            this.writeD(this._OpenedSlot);
        }
    }
}
