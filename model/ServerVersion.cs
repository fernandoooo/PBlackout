using System.Reflection;

namespace PBServer.model
{
  public static class ServerVersion
  {
    public static string version = ((object) Assembly.GetExecutingAssembly().GetName().Version).ToString();

    static ServerVersion()
    {
       
    }
  }
}
