// Type: PBServer.network.serverpackets.PROTOCOL_LOBBY_GET_ROOMLIST_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.src.model;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;
using PBServer.model.clans;
using PBServer.managers;
using System.Text;
using System;

namespace PBServer.network.serverpackets
{
  public class S_LOBBY_GET_ROOMLIST : SendBaseGamePacket
  {
    private Channel _client;

    public S_LOBBY_GET_ROOMLIST(Channel ch)
    {
      this._client = ch;
      this.makeme();
    }

    protected internal override void write()
    {
      this.writeH((short) 3074);
      this.writeD(this._client.getRooms().Count);
      this.writeD(0);
      this.writeD(this._client.getRooms().Count);
      for (int index = 0; index < this._client.getRooms().Count; ++index)
      {
        Room room = this._client.getRooms()[index];
        this.writeD(this._client.getRooms()[index].getRoomId());
        this.writeS(room.name, 23);
        this.writeC((byte) room.map_id);
        this.writeC((byte) 0);
        this.writeC((byte) 0);
        this.writeC((byte) room.room_type);
        this.writeC((byte) room.isBattleInt());
        this.writeC((byte) room.getAllPlayers().Count);
        this.writeC((byte) room.getSlotCount());
        this.writeC((byte) 5); //Ping da sala
        this.writeC((byte) room.allweapons);
        this.writeC((byte) room.isPasswordInt());
        this.writeC((byte) room.special);
      }
      this.writeD(this._client.getAllPlayers().Count);
      this.writeD(0);
      this.writeD(this._client.getAllPlayers().Count);
      int num = 0;
      for (int index = 0; index < this._client.getWaitPlayers().Count; ++index)
      {
        Account account = this._client.getWaitPlayers()[index];
        Clan clan = ClanManager.getInstance().get(account.getClanId());
        if (account != null && account.getPlayerName() != "")
        {
          this.writeD(account.getPlayerId());
          if (account.getClanId() == 0)
          {
              this.writeC((byte)255);
              this.writeC((byte)255);
              this.writeC((byte)255);
              this.writeC((byte)255);
              this.writeS("", 17);
          }
          if (account.getClanId() > 0)
          {
              this.writeC((byte)clan.getLogo1());
              this.writeC((byte)clan.getLogo2());
              this.writeC((byte)clan.getLogo3());
              this.writeC((byte)clan.getLogo4());
              this.writeS(clan.getClanName(), 17);
          }
          this.writeH((short)account.getRank());
          this.writeS(account.getPlayerName(), 33);
          this.writeC((byte)account.getNameColor());
          ++num;
        }
      }
    }
  }
}
