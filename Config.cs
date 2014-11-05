namespace PBServer
{
  internal class Config
  {
    public static string LOGIN_HOST;
    public static int LOGIN_PORT;
    public static string GAME_HOST;
    public static int GAME_PORT;
    public static string UDPHost;
    public static string DB_HOST;
    public static string DB_NAME;
    public static string DB_USER;
    public static string DB_PASS;
    public static bool AUTO_ACCOUNTS;
    public static bool debug;
    public static bool TrainigExpEnable;
    public static int PlayerTemplateId;
    public static bool onlyGM;
    public static bool ExternalOrInternalSendIpUdp;
    public static int MaxPlayerInChannel;
    public static string WebServerHost;
    public static int OutpostEnable;
    public static int NecessaryRank;
    public static int NecessaryGold;

    public static void load()
    {
      ConfigFile configFile = new ConfigFile("config/server.ini");
      Config.LOGIN_HOST = configFile.getProperty("loginhost", "127.0.0.1");
      Config.LOGIN_PORT = int.Parse(configFile.getProperty("loginport", "39190"));
      Config.GAME_HOST = configFile.getProperty("gamehost", "127.0.0.1");
      Config.GAME_PORT = int.Parse(configFile.getProperty("gameport", "39191"));
      Config.UDPHost = configFile.getProperty("udphost", "127.0.0.1");
      Config.DB_HOST = configFile.getProperty("dbhost", "localhost");
      Config.DB_NAME = configFile.getProperty("dbname", "pb_auth");
      Config.DB_USER = configFile.getProperty("dbuser", "root");
      Config.DB_PASS = configFile.getProperty("dbpass", "");
      Config.AUTO_ACCOUNTS = bool.Parse(configFile.getProperty("autoaccounts", "false"));
      Config.debug = bool.Parse(configFile.getProperty("debug", "true"));
      Config.TrainigExpEnable = bool.Parse(configFile.getProperty("TrainigExpEnable", "false"));
      Config.PlayerTemplateId = int.Parse(configFile.getProperty("PlayerTemplateId", "1"));
      Config.onlyGM = bool.Parse(configFile.getProperty("OnlyGM", "false"));
      Config.MaxPlayerInChannel = int.Parse(configFile.getProperty("MaxPlayerInChannel", "300"));
      Config.ExternalOrInternalSendIpUdp = bool.Parse(configFile.getProperty("ExternalOrInternalSendIpUdp", "false"));
      Config.WebServerHost = configFile.getProperty("WebServerHost", "127.0.0.1");
      Config.OutpostEnable = int.Parse(configFile.getProperty("OutpostEnable", "1"));
      Config.NecessaryRank = int.Parse(configFile.getProperty("NecessaryRank", "1"));
      Config.NecessaryGold = int.Parse(configFile.getProperty("NecessaryGold", "1"));
      if (!Config.onlyGM)
        return;
      CLogger.getInstance().warning("Servidor aberto apenas para GM's!");
    }
  }
}
