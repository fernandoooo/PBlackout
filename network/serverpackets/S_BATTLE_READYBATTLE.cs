// Type: PBServer.network.serverpackets.PROTOCOL_BATTLE_READYBATTLE_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.src.model.accounts;

namespace PBServer.network.serverpackets
{
    public class S_BATTLE_READYBATTLE : SendBaseGamePacket
    {
        private Account _player;

        public S_BATTLE_READYBATTLE(Account p)
        {
            this.makeme();
            this._player = p;
        }

        protected internal override void write()
        {
            this.writeH((short)3332);
            this.writeD((int)this._player.getRoom().getSlotState(this._player.getSlot()));
            this.writeH((short)this._player.getRoom().map_id);
            this.writeC((byte)this._player.getRoom().stage4v4);
            this.writeC((byte)this._player.getRoom().room_type);
            this.writeD(this._player.getCharRed());
            this.writeD(this._player.getCharBlue());
            this.writeD(this._player.getCharHelmet());
            this.writeD(this._player.getCharBeret());
            this.writeD(this._player.getCharDino());
        }
    }
}
