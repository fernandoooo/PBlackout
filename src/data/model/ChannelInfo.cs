// Type: PBServer.src.data.model.ChannelInfo
// Assembly: PBServer, Version=1.15.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

namespace PBServer.src.data.model
{
  internal class ChannelInfo
  {
    public int _id;
    public int _type;
    public string _name;
    private int _maxPlayers;

    public ChannelInfo(string name, int id, int type, int max_players)
    {
      this._name = name;
      this._id = id;
      this._type = type;
      this._maxPlayers = max_players;
    }

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
