using PBServer;
using PBServer.src.model.rooms;

namespace PBServer.network.Game.packets.serverpackets
{
    public class opcode_3608_ACK : SendBaseGamePacket
    {

        private int _autobalans;
        private int _seeConf;
        private int _killtime;
        private int _limit;

        public opcode_3608_ACK(int autobalans, int seeConf, int killtime, int limit)
        {
            this.makeme();
            this._autobalans = autobalans;
            this._seeConf = seeConf;
            this._killtime = killtime;
            this._limit = limit;
        }

        protected internal override void write()
        {
            this.writeH((short)3609);
            this.writeB(new byte[33]);
            this.writeD(this._killtime);
            this.writeC((byte)this._limit);
            this.writeC((byte)this._seeConf);
            this.writeH((short)this._autobalans);
        }
    }
}
