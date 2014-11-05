// Type: PBServer.network.serverpackets.PROTOCOL_BASE_USER_LEAVE_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.src.model.accounts;
using PBServer.src.managers;

namespace PBServer.network.serverpackets
{
    internal class S_BASE_USER_LEAVE : SendBaseLoginPacket
    {
        private Account _p;
        public S_BASE_USER_LEAVE(Account p)
        {
            this.makeme();
            this._p = p;
        }

        protected internal override void write()
        {
            this.writeH((short)0xA12);
            this.writeD(0);
            this._p.setStatus(0);
            AccountManager.getInstance().UpdateStatus(this._p.getPlayerId(), 0); //ADICIONAR STATUS OFFLINE
        }
    }
}
