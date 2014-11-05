// Type: PBServer.src.model.Channel
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.src.managers;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;
using System;
using System.Collections.Generic;
using PBServer.src.model.ENUMS;

namespace PBServer.src.model
{
  public class Channel
  {
    private int id;
    private int _type;
    private string _announce;
    private int _max_players;
    private List<int> _players;
    private List<Room> _rooms;

    public Channel(int id1, int type, string announce, int max_players)
    {
      this.id = id1;
      this._type = type;
      this._announce = announce;
      this._max_players = max_players;
      this._players = new List<int>();
      this._rooms = new List<Room>();
    }

    public int getId()
    {
      return this.id;
    }

    public int getTypeChannel()
    {
      return this._type;
    }

    public string getAnnounce()
    {
        return this._announce;
    }

    public int getAnnounceSize()
    {
        return this._announce.Length;
    }

    public int getMaxPlayers()
    {
        return this._max_players;
    }

    public void addPlayer(Account p)
    {
      for (int index = 0; index < this._players.Count; ++index)
      {
        if (p.player_id == this._players[index])
          return;
      }
      CLogger.getInstance().info("O jogador " + p.getPlayerName() + " foi adicionado ao lobby.");
      this._players.Add(p.player_id);
    }

    public List<int> getAllPlayers()
    {
      return this._players;
    }

    public void RemoveEmptyRooms()
    {
      for (int index = 0; index < this.getRooms().Count; ++index)
      {
        Room r = this.getRooms()[index];
        if (r.getAllPlayers().Count <= 0)
        {
            this.removeRoom(r);
        }
      }
    }

    public List<Room> getRooms()
    {
      return this._rooms;
    }

    public Room getRoomInId(int id)
    {
      for (int index = 0; index < this._rooms.Count; ++index)
      {
        if (this._rooms[index].getRoomId() == id)
          return this._rooms[index];
      }
      return (Room) null;
    }

    public Account getPlayerFromPlayerId(int playerId)
    {
      for (int index = 0; index < this._players.Count; ++index)
      {
        if (this._players[index] == playerId)
          return AccountManager.getInstance().getAccountInObjectId(playerId);
      }
      return (Account) null;
    }

    public void removeRoom(Room r)
    {
      for (int index = 0; index < this._rooms.Count; ++index)
      {
        if (r.getRoomId() == this._rooms[index].getRoomId())
        {
          this._rooms.RemoveAt(index);
          break;
        }
      }
    }

    public void addRoom(Room r)
    {
      this._rooms.Add(r);
    }

    public List<Account> getWaitPlayers()
    {
      List<Account> list = new List<Account>();
      for (int index = 0; index < this._players.Count; ++index)
      {
        Account playerFromPlayerId = this.getPlayerFromPlayerId(this._players[index]);
        if (playerFromPlayerId != null && playerFromPlayerId.getRoom() == null)
          list.Add(playerFromPlayerId);
      }
      return list;
    }

    public void removePlayer(Account p)
    {
      for (int index = 0; index < this._players.Count; ++index)
      {
        try
        {
          if (p.player_id == this._players[index])
          {
            Account playerFromPlayerId = this.getPlayerFromPlayerId(this._players[index]);
            Console.WriteLine(string.Concat(new object[4]
            {
              (object) "O jogador ",
              (object) playerFromPlayerId.getPlayerName(),
              (object) " foi removido do canal ",
              (object) p.getClient().getChannelId()
            }));
            playerFromPlayerId.getClient().setChannelId(-1);
            this._players.Remove(p.player_id);
            break;
          }
        }
        catch (Exception ex)
        {
          CLogger.getInstance().warning(ex.ToString());
        }
      }
    }
  }
}
