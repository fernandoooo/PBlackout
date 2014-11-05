// Type: PBServer.network.web.webserver
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer.network.web.function;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System;
using System.Net;

namespace PBServer.network.web
{
  public class webserver
  {
    private static webserver _instance;
    private TcpListener _tcpListener;
    private TcpClient _tcpClient;
    private Client _client;

    public webserver()
    {
      if (!Directory.Exists("web"))
        Directory.CreateDirectory("web");
      if (!File.Exists("web/index.html"))
        File.AppendAllText("web/index.html", "<legend><center>Welcome to Hell!<center></legend><br>=)");
      if (!File.Exists("web/ReadMe.txt"))
        File.AppendAllText("web/ReadMe.txt", "Здесь могут быть размещены дополнительные файлы, сервер не поддерживает php, может служить только для закачки файлов, плохо работает многопоточность. Вебадминку можете не искать она зашита в exe файл. Если вы удалите этот файл, то он будет создан после перезапуска сервера. Так же если хотите зайти в вебадминку то хост http://localhost:3321/, Логин: admin, Пароль: admin. С уважением PBDev Team & MMo-Network.");
      new Thread(new ThreadStart(this.InitWebServer)).Start();
    }

    public void InitWebServer()
    {
        string host = Config.WebServerHost.ToString();
        IPAddress addr = IPAddress.Parse(host);

        CLogger.getInstance().info("|[WEB]| Carregando...");
        _tcpListener = new TcpListener(IPAddress.Any, 3321);
        _tcpListener.Start();

        while (true)
        {
            _tcpClient = _tcpListener.AcceptTcpClient();
            _client = new Client(_tcpClient);
        }
    }

    public static webserver getInstance()
    {
      if (webserver._instance != null)
        return webserver._instance;
      webserver._instance = new webserver();
      return webserver._instance;
    }
  }
}
