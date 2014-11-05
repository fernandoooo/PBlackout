// Type: PBServer.src.model.rooms.SLOT
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe
using PBServer.src.model.accounts;

namespace PBServer.src.model.rooms
{
  public class SLOT
  {
    public SLOT_STATE state = SLOT_STATE.SLOT_STATE_EMPTY;
    public int _playerId;
    public string playername;
    public int oneTimeKills;
    private int id = 0;
    public int allKills;
    public int allDeaths;
    public int gp;
    public int exp;
    public int botScore;
    public int lastKillState;
    public int killsOnLife;
    public bool repeatLastState;
    public int killMessage;
    public int headshots;
    private Account player;

    public int getId()
    {
        return id;
    }

    public void setId(int id)
    {
        this.id = id;
    }

    public void resetkillsOnLife()
    {
      this.killsOnLife = 0;
    }

    public Account getPlayer()
    {
        return player;
    }

    public void setPlayer(Account player)
    {
        this.player = player;
    }
  }
}
