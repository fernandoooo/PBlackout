using System.Collections.Generic;
using System.IO;

namespace PBServer
{
  internal class ConfigFile
  {
    private FileInfo File;
    public SortedList<string, string> _topics;

    public ConfigFile(string Path)
    {
      this.File = new FileInfo(Path);
      this._topics = new SortedList<string, string>();
      this.reload();
    }

    public void reload()
    {
      StreamReader streamReader = new StreamReader(this.File.FullName);
      while (!streamReader.EndOfStream)
      {
        string str = streamReader.ReadLine();
        if (str.Length != 0 && !str.StartsWith(";"))
          this._topics.Add(str.Split(new char[1]
          {
            '='
          })[0], str.Split(new char[1]
          {
            '='
          })[1]);
      }
    }

    public string getProperty(string value, string defaultprop)
    {
      string str;
      try
      {
        str = this._topics[value];
      }
      catch
      {
        CLogger.getInstance().warning("|[CF]| Não foi encontrado o parametro: " + value);
        return defaultprop;
      }
      return str == null ? defaultprop : str;
    }
  }
}
