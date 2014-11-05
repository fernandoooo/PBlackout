using System;
using System.IO;

namespace PBServer
{
  public class CLogger
  {
    private string name = "logs//" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + ".log";
    private static bool cf = true;
    private static CLogger _instance;

    static CLogger()
    {
    }

    public CLogger()
    {
      this.form();
    }

    public static CLogger getInstance()
    {
      if (CLogger._instance == null)
        CLogger._instance = new CLogger();
      return CLogger._instance;
    }

    public void info_red(string text)
    {
        try
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            if (CLogger.cf)
            {
                FileStream fileStream = new FileStream(this.name, FileMode.Append);
                StreamWriter streamWriter = new StreamWriter((Stream)fileStream);
                streamWriter.WriteLine(DateTime.Now.ToString() + " - " + text);
                streamWriter.Close();
                fileStream.Close();
            }
            Console.ResetColor();
        }
        catch
        {
        }
    }

    public void info_blue(string text)
    {
        try
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(text);
            if (CLogger.cf)
            {
                FileStream fileStream = new FileStream(this.name, FileMode.Append);
                StreamWriter streamWriter = new StreamWriter((Stream)fileStream);
                streamWriter.WriteLine(DateTime.Now.ToString() + " - " + text);
                streamWriter.Close();
                fileStream.Close();
            }
            Console.ResetColor();
        }
        catch
        {
        }
    }

    public void warning(string text)
    {
      try
      {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(text);
        if (CLogger.cf)
        {
          FileStream fileStream = new FileStream(this.name, FileMode.Append);
          StreamWriter streamWriter = new StreamWriter((Stream) fileStream);
          streamWriter.WriteLine(DateTime.Now.ToString() + " - " + text);
          streamWriter.Close();
          fileStream.Close();
        }
        Console.ResetColor();
      }
      catch
      {
      }
    }

    public void error(string text)
    {
      try
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(text);
        if (CLogger.cf)
        {
          FileStream fileStream = new FileStream(this.name, FileMode.Append);
          StreamWriter streamWriter = new StreamWriter((Stream) fileStream);
          streamWriter.WriteLine(DateTime.Now.ToString() + " - " + text);
          streamWriter.Close();
          fileStream.Close();
        }
        Console.ResetColor();
      }
      catch
      {
      }
    }

    public void info(string text)
    {
      try
      {
        Console.WriteLine(text);
        if (!CLogger.cf)
          return;
        FileStream fileStream = new FileStream(this.name, FileMode.Append);
        StreamWriter streamWriter = new StreamWriter((Stream) fileStream);
        streamWriter.WriteLine(DateTime.Now.ToString() + " - " + text);
        streamWriter.Close();
        fileStream.Close();
      }
      catch
      {
      }
    }

    public void extra_info(string text)
    {
      try
      {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(text);
        if (CLogger.cf)
        {
          FileStream fileStream = new FileStream(this.name, FileMode.Append);
          StreamWriter streamWriter = new StreamWriter((Stream) fileStream);
          streamWriter.WriteLine(DateTime.Now.ToString() + " - " + text);
          streamWriter.Close();
          fileStream.Close();
        }
        Console.ResetColor();
      }
      catch
      {
      }
    }

    public void form()
    {
      if (!CLogger.cf || Directory.Exists("logs"))
        return;
      Directory.CreateDirectory("logs");
    }
  }
}
