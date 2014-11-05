using PBServer.model.clans;

namespace PBServer.model.players
{
  public class PlayerStats
  {
    private int _fights_s;
    private int _fights_win_s;
    private int _fights_lost_s;
    private int _kills_count_s;
    private int _deaths_count_s;
    private int _escape_s;
    private int _fights_ns;
    private int _fights_win_ns;
    private int _fights_lost_ns;
    private int _kills_count_ns;
    private int _deaths_count_ns;
    private int _escape_ns;
    private long _create_date;
    private Clan _clanInfo;

    public PlayerStats()
    {
      this._fights_s = 0;
      this._fights_win_s = 0;
      this._fights_lost_s = 0;
      this._kills_count_s = 0;
      this._deaths_count_s = 0;
      this._escape_s = 0;
      this._fights_ns = 0;
      this._fights_win_ns = 0;
      this._fights_lost_ns = 0;
      this._kills_count_ns = 0;
      this._deaths_count_ns = 0;
      this._escape_ns = 0;
      this._create_date = 0L;
    }

    public long getCreateDate()
    {
        return _create_date;
    }

    public void setCreateDate(long val)
    {
        _create_date = val;
    }

    public int getFights(bool season)
    {
      return season ? this._fights_s : this._fights_ns;
    }

    public void setFights(int val, bool season)
    {
      if (season)
        this._fights_s = val;
      else
        this._fights_ns = val;
    }

    public int getWinFights(bool season)
    {
      return season ? this._fights_win_s : this._fights_win_ns;
    }

    public void setWinFights(int val, bool season)
    {
      if (season)
        this._fights_win_s = val;
      else
        this._fights_win_ns = val;
    }

    public int getLostFights(bool season)
    {
      return season ? this._fights_lost_s : this._fights_lost_ns;
    }

    public void setLostFights(int val, bool season)
    {
      if (season)
        this._fights_lost_s = val;
      else
        this._fights_lost_ns = val;
    }

    public int getKills(bool season)
    {
      return season ? this._kills_count_s : this._kills_count_ns;
    }

    public void setKills(int val, bool season)
    {
      if (season)
        this._kills_count_s += val;
      else
        this._kills_count_ns += val;
    }

    public int getDeaths(bool season)
    {
      return season ? this._deaths_count_s : this._deaths_count_ns;
    }

    public void setDeaths(int val, bool season)
    {
      if (season)
        this._deaths_count_s += val;
      else
        this._deaths_count_ns += val;
    }

    public int getEscapes(bool season)
    {
      return season ? this._escape_s : this._escape_ns;
    }

    public void setEscapes(int val, bool season)
    {
      if (season)
        this._escape_s = val;
      else
        this._escape_ns = val;
    }

    public Clan getClanInfo()
    {
        return _clanInfo == null ? new Clan() : _clanInfo;
    }

    public void setClanInfo(Clan clanInfo)
    {
        _clanInfo = clanInfo;
    }
  }
}
