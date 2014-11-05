using PBServer.data;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace PBServer
{
  internal class GameClientManager
  {
    private List<GameClient> _loggedClients = new List<GameClient>();
    private SortedList<string, LoginClient> _waitingAcc = new SortedList<string, LoginClient>();
    private static GameClientManager _instance = new GameClientManager();
    protected NetworkBlock _banned;

    static GameClientManager()
    {
    }

    public GameClientManager()
    {
      
    }

    public static GameClientManager getInstance()
    {
      return GameClientManager._instance;
    }

    public void addClient(TcpClient client)
    {
      if (this._banned == null)
        this._banned = NetworkBlock.getInstance();
      string ip = client.Client.RemoteEndPoint.ToString().Split(new char[1]
      {
        ':'
      })[0];
      if (!this._banned.allowed(ip))
      {
        client.Close();
        CLogger.getInstance().error("NetworkBlock: connection attemp failed. " + ip + " banned.");
      }
      else
      {
        GameClient gameClient = new GameClient(client);
        if (this._loggedClients.Contains(gameClient))
          CLogger.getInstance().info("Client is Have");
        else
          this._loggedClients.Add(gameClient);
      }
    }

    public void removeClient(GameClient loginClient)
    {
      try
      {
        if (!this._loggedClients.Contains(loginClient))
          return;
        this._loggedClients.Remove(loginClient);
      }
      catch (Exception ex)
      {
        CLogger.getInstance().warning(ex.ToString());
      }
    }
  }
}
