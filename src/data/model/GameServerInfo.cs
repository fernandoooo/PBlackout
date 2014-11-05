// Type: PBServer.src.data.model.GameServerInfo
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

namespace PBServer.src.data.model
{
  internal class GameServerInfo
  {
    public int _id;
    public string _name;
    public string _pass;
    public int _type;
    public string _ip;
    private int _maxPlayers;
    private bool _isOnline;

    public GameServerInfo(string name, int id, string pass, int type, int max_players, string ip)
    {
      this._name = name;
      this._id = id;
      this._pass = pass;
      this._type = type;
      this._maxPlayers = max_players;
      this._ip = ip;
    }

    public int getTypeGameServer()
    {
        return this._type;
    }

    public void setOnline(bool online)
    {
        _isOnline = online;
    }

    public bool isOnline()
    {
        return _isOnline;
    }

    public string getPassword()
    {
        return _pass;
    }

    /*public int getCurrentPlayerCount()
    {
        if (_gst == null)
            return 0;
        return _gst.getPlayerCount();
    }*/

    public void setMaxPlayers(int maxPlayers)
    {
        _maxPlayers = maxPlayers;
    }

    public int getMaxPlayers()
    {
        return _maxPlayers;
    }
  }
}
