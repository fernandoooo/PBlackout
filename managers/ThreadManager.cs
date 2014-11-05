using System.Threading;

namespace PBServer.managers
{
  internal class ThreadManager
  {
    public static void runNewThread(Thread t)
    {
      t.Start();
    }
  }
}
