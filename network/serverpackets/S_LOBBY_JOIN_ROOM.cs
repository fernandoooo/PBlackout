// Type: PBServer.network.serverpackets.PROTOCOL_LOBBY_JOIN_ROOM_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;
using PBServer.model.clans;
using PBServer.managers;

namespace PBServer.network.serverpackets
{
    public class S_LOBBY_JOIN_ROOM : SendBaseGamePacket
    {
        public long _id;
        private Room _room;
        private Account _player;
        public S_LOBBY_JOIN_ROOM(long id, Account player)
        {
            this.makeme();
            this._id = id;
            this._player = player;
            if (this._player == null)
                return;
            this._room = this._player.getRoom();
        }

        protected internal override void write()
        {
            this.writeH((short)3082);
            if (this._player != null)
            {
                this.writeD(12);
                this.writeD(this._player.getSlot());
                this.writeD(this._room.getRoomId());
                this.writeS(this._room.name, 23);
                this.writeC((byte)this._room.map_id);
                this.writeC((byte)0);
                this.writeC((byte)0);
                this.writeC((byte)this._room.room_type);
                this.writeC((byte)5);
                this.writeC((byte)this._room.getAllPlayers().Count);
                this.writeC((byte)this._room.getSlotCount());
                this.writeC((byte)5); //Ping da sala
                this.writeC((byte)this._room.allweapons);
                this.writeC((byte)this._room.random_map);
                this.writeC((byte)this._room.special);
                this.writeS(this._room.getLeader().getPlayerName(), 33);
                this.writeC((byte)this._room.killtime);
                this.writeC((byte)0);
                this.writeC((byte)0);
                this.writeC((byte)0);
                this.writeC((byte)this._room.limit);
                this.writeC((byte)this._room.seeConf);
                this.writeH((short)this._room.autobalans);
                this.writeD(0);
                this.writeC(0);
                this.writeD(this._room.getLeader().getSlot());
                for (int slot = 0; slot < 16; ++slot)
                {
                    Account playerBySlot = this._room.getPlayerBySlot(slot);
                    if (playerBySlot != null)
                    {
                        this.writeC((byte)this._room.getSlotState(playerBySlot.getSlot()));
                        this.writeC((byte)playerBySlot.getRank());
                        this.writeD(0);
                        this.writeD(0);
                        if (playerBySlot.getClanId() == 0)
                        {
                            this.writeC((byte)255);
                            this.writeC((byte)255);
                            this.writeC((byte)255);
                            this.writeC((byte)255);
                            this.writeC((byte)0);
                            this.writeB(new byte[6]);
                            this.writeS("", 17);
                        }
                        if (playerBySlot.getClanId() > 0)
                        {
                            this.writeC((byte)ClanManager.getInstance().get(playerBySlot.getClanId()).getLogo1());
                            this.writeC((byte)ClanManager.getInstance().get(playerBySlot.getClanId()).getLogo2());
                            this.writeC((byte)ClanManager.getInstance().get(playerBySlot.getClanId()).getLogo3());
                            this.writeC((byte)ClanManager.getInstance().get(playerBySlot.getClanId()).getLogo4());
                            this.writeC((byte)ClanManager.getInstance().get(playerBySlot.getClanId()).getLogoColor());
                            this.writeB(new byte[6]);
                            this.writeS(ClanManager.getInstance().get(playerBySlot.getClanId()).getClanName(), 17);
                        }
                        this.writeD(0);
                    }
                    else
                    {
                        this.writeC((byte)this._room.getSlotState(slot));
                        this.writeB(new byte[41]);
                    }
                }
                this.writeC((byte)this._room.getAllPlayers().Count);
                foreach (Account account in this._room.getAllPlayers())
                {
                    this.writeC((byte)account.getSlot());
                    this.writeC((byte)33);
                    this.writeS(account.getPlayerName(), 33);
                    this.writeC((byte)account.getNameColor());
                }
                this.writeC((byte)this._room._aiCount);
                this.writeC((byte)this._room._aiLevel);
            }
            else
            {
                this.writeQ(2147487748L);
            }
        }
    }
}
