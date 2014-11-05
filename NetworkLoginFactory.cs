using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace PBServer
{
  internal class NetworkLoginFactory
  {
    private static NetworkLoginFactory _instance;
    private static TcpListener _clientLoginListener;

    public NetworkLoginFactory()
    {
      new Thread(new ThreadStart(this.NetworkStart)).Start();
    }

    public static NetworkLoginFactory getInstance()
    {
      if (NetworkLoginFactory._instance != null)
        return NetworkLoginFactory._instance;
      NetworkLoginFactory._instance = new NetworkLoginFactory();
      return NetworkLoginFactory._instance;
    }

    public void NetworkStart()
    {
      try
      {
        NetworkLoginFactory._clientLoginListener = new TcpListener(IPAddress.Parse(Config.LOGIN_HOST), Config.LOGIN_PORT);
        NetworkLoginFactory._clientLoginListener.Start();
        CLogger.getInstance().info(string.Concat(new object[4]
        {
          (object) "|[NLF]| Login Server IP: ",
          (object) ((IPEndPoint) NetworkLoginFactory._clientLoginListener.LocalEndpoint).Address,
          (object) ":",
          (object) Config.LOGIN_PORT
        }));
        NetworkLoginFactory._clientLoginListener.BeginAcceptTcpClient(new AsyncCallback(this.BeginAcceptTcpClient), (object) null);
      }
      catch (Exception ex)
      {
        CLogger.getInstance().error(ex.ToString());
      }
    }

    private void BeginAcceptTcpClient(IAsyncResult result)
    {
      NetworkLoginFactory.accept(NetworkLoginFactory._clientLoginListener.EndAcceptTcpClient(result));
      NetworkLoginFactory._clientLoginListener.BeginAcceptTcpClient(new AsyncCallback(this.BeginAcceptTcpClient), (object) null);
    }

    private static void accept(TcpClient client)
    {
      LoginClientManager.getInstance().addClient(client);
    }
  }
}
