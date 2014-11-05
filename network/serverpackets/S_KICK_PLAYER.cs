using PBServer;

namespace PBServer.network.serverpackets
{
    public class S_KICK_PLAYER : SendBaseGamePacket
    {
        private int _playerIdx;

        public S_KICK_PLAYER(int playerIdx)
        {
            this.makeme();
            this._playerIdx = playerIdx;
        }

        protected internal override void write()
        {
            this.writeH(3595);
            this.writeD(this._playerIdx);
        }
    }
}
