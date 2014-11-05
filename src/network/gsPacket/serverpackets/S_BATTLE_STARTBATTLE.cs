using PBServer;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;

namespace PBServer.src.network.gsPacket.serverpackets
{
    internal class S_BATTLE_STARTBATTLE : SendBaseGamePacket
    {
        private Account _player;
        private Room _room;

        public S_BATTLE_STARTBATTLE(Account p)
        {
            this.makeme();
            this._player = p;
            this._room = p.getRoom();
        }

        protected internal override void write()
        {
            this.writeH((short)0xD06);
            this.writeD(this._room.isBattleInt());
            this.writeD(this._player.getSlot());
            this.writeD(this._player.getCharRed());
            this.writeD(this._player.getCharBlue());
            this.writeD(this._player.getCharHelmet());
            this.writeD(this._player.getCharDino());
            this.writeD(this._player.getCharBeret());
            this.writeC((byte)0);
            this.writeC((byte)0);
            this.writeC((byte)0);
            this.writeC((byte)1);
        }
    }
}
