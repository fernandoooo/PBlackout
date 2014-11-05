using PBServer;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;
using System.Net;

namespace PBServer.src.network.gsPacket.serverpackets
{
    internal class S_BATTLE_PRESTARTBATTLE : SendBaseGamePacket
    {
        private Room _room;
        private Account _player;

        public S_BATTLE_PRESTARTBATTLE(Account p)
        {
            this.makeme();
            this._player = p;
            this._room = p.getRoom();
        }

        protected internal override void write()
        {
            this.writeH((short)0xD15);
            this.writeD(this._room.isBattleInt());
            this.writeD(this._player.getSlot());
            this.writeC((byte)this._room.server_type);
            this.writeB(this._room.getLeader().publicAdddress());
            this.writeH((short)29890);
            this.writeB(this._room.getLeader().publicAdddress());
            this.writeH((short)29890);
            this.writeC((byte)0);
            this.writeB(this._player.publicAdddress());
            this.writeH((short)29890);
            this.writeB(this._player.getLocalAddress()); //publicAdddress
            this.writeH((short)29890);
            this.writeC((byte)0);
            this.writeB(IPAddress.Parse(Config.UDPHost).GetAddressBytes());
            this.writeB(new byte[10]
      {
        (byte) 67,
        (byte) 156,
        (byte) 145,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 71,
        (byte) 6,
        (byte) 0,
        (byte) 0
      });
            this.writeC((byte)0);
            this.writeB(new byte[35]
      {
        (byte) 10,
        (byte) 34,
        (byte) 0,
        (byte) 1,
        (byte) 16,
        (byte) 3,
        (byte) 30,
        (byte) 5,
        (byte) 6,
        (byte) 7,
        (byte) 4,
        (byte) 9,
        (byte) 22,
        (byte) 11,
        (byte) 27,
        (byte) 8,
        (byte) 14,
        (byte) 15,
        (byte) 2,
        (byte) 17,
        (byte) 18,
        (byte) 33,
        (byte) 20,
        (byte) 21,
        (byte) 19,
        (byte) 23,
        (byte) 24,
        (byte) 25,
        (byte) 26,
        (byte) 12,
        (byte) 28,
        (byte) 29,
        (byte) 13,
        (byte) 31,
        (byte) 32
      });
        }
    }
}
