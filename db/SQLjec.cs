using MySql.Data.MySqlClient;
using PBServer;
using System.Runtime.Remoting.Contexts;

namespace PBServer.db
{
  [Synchronization]
  public class SQLjec
  {
    private static SQLjec sql = new SQLjec();
    protected MySqlConnectionStringBuilder connBuilder;

    static SQLjec()
    {
    }

    public SQLjec()
    {
      this.connBuilder = new MySqlConnectionStringBuilder();
      this.connBuilder.Database = Config.DB_NAME;
      this.connBuilder.Server = Config.DB_HOST;
      this.connBuilder.UserID = Config.DB_USER;
      this.connBuilder.Password = Config.DB_PASS;
      this.connBuilder.Port = 3306U;
      CLogger.getInstance().info("|[SQLJ]| IP: " + Config.DB_HOST + ":3306");
      CLogger.getInstance().info("|[SQLJ]| MySQL: estabelecendo conexão...");
    }

    public static SQLjec getInstance()
    {
      return SQLjec.sql;
    }

    public MySqlConnection conn()
    {
      return new MySqlConnection(this.connBuilder.ConnectionString);
    }
  }
}
