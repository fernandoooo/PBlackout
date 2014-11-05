// Type: PBServer.src.data.xml.holders.GameServerInfoHolder
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.src.commons.data.holders;
using PBServer.src.data.model;
using System.Collections.Generic;

namespace PBServer.src.data.xml.holders
{
  internal class GameServerInfoHolder : IHolder
  {
    private static GameServerInfoHolder _instance;
    private static SortedDictionary<int, GameServerInfo> _servers;

    public GameServerInfoHolder()
    {
      GameServerInfoHolder._servers = new SortedDictionary<int, GameServerInfo>();
    }

    public static GameServerInfoHolder getInstance()
    {
      if (GameServerInfoHolder._instance == null)
        GameServerInfoHolder._instance = new GameServerInfoHolder();
      return GameServerInfoHolder._instance;
    }

    public void addGameServerInfo(GameServerInfo server)
    {
      GameServerInfoHolder._servers.Add(server._id, server);
    }

    public GameServerInfo getGameServerInfo(int id)
    {
      return GameServerInfoHolder._servers[id];
    }

    public SortedDictionary<int, GameServerInfo> getAllGameserverInfos()
    {
      return GameServerInfoHolder._servers;
    }

    public void log()
    {
      CLogger.getInstance().info("|[GSH]| Foram carregados " + (object) GameServerInfoHolder._servers.Count + " servidores.");
    }

    public void clear()
    {
      GameServerInfoHolder._servers.Clear();
    }
  }
}
