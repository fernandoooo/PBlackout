using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace PBServer
{
  internal class NetworkGameFactory
  {
    private static NetworkGameFactory _instance;
    private static TcpListener _clientGameListener;

    public NetworkGameFactory()
    {
      new Thread(new ThreadStart(this.NetworkStart)).Start();
    }

    public static NetworkGameFactory getInstance()
    {
      if (NetworkGameFactory._instance != null)
        return NetworkGameFactory._instance;
      NetworkGameFactory._instance = new NetworkGameFactory();
      return NetworkGameFactory._instance;
    }

    public void NetworkStart()
    {
      try
      {
        NetworkGameFactory._clientGameListener = new TcpListener(IPAddress.Parse(Config.GAME_HOST), Config.GAME_PORT);
        NetworkGameFactory._clientGameListener.Start();
        CLogger.getInstance().info("|[NGF]| Game Server IP: " + NetworkGameFactory._clientGameListener.LocalEndpoint.ToString());
        NetworkGameFactory._clientGameListener.BeginAcceptTcpClient(new AsyncCallback(this.BeginAcceptTcpClient), (object) null);
      }
      catch (Exception ex)
      {
        CLogger.getInstance().error(ex.ToString());
      }
    }

    private void BeginAcceptTcpClient(IAsyncResult result)
    {
      NetworkGameFactory.accept(NetworkGameFactory._clientGameListener.EndAcceptTcpClient(result));
      NetworkGameFactory._clientGameListener.BeginAcceptTcpClient(new AsyncCallback(this.BeginAcceptTcpClient), (object) null);
    }

    private static void accept(TcpClient client)
    {
      GameClientManager.getInstance().addClient(client);
    }
  }
}
