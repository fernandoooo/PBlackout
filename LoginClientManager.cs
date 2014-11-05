using PBServer.data;
using System.Collections.Generic;
using System.Net.Sockets;

namespace PBServer
{
  internal class LoginClientManager
  {
    private List<LoginClient> _loggedClients = new List<LoginClient>();
    private SortedList<string, LoginClient> _waitingAcc = new SortedList<string, LoginClient>();
    private static LoginClientManager _instance = new LoginClientManager();
    protected NetworkBlock _banned;

    static LoginClientManager()
    {
    }

    public LoginClientManager()
    {
      
    }

    public static LoginClientManager getInstance()
    {
      return LoginClientManager._instance;
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
        LoginClient loginClient = new LoginClient(client);
        if (this._loggedClients.Contains(loginClient))
          CLogger.getInstance().info("Client is Have");
        else
          this._loggedClients.Add(loginClient);
      }
    }

    public void removeClient(LoginClient loginClient)
    {
      if (!this._loggedClients.Contains(loginClient))
        return;
      this._loggedClients.Remove(loginClient);
    }
  }
}
