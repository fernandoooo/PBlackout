using PBServer;
using System;
using System.Collections.Generic;
using System.IO;

namespace PBServer.data
{
  internal class NetworkBlock
  {
    protected List<NB_interface> blocks = new List<NB_interface>();
    private static NetworkBlock nb = new NetworkBlock();

    static NetworkBlock()
    {
    }

    public NetworkBlock()
    {
      StreamReader streamReader = new StreamReader(new FileInfo("data//sq//blocks.txt").FullName);
      while (!streamReader.EndOfStream)
      {
        string str = streamReader.ReadLine();
        if (str.Length != 0 && !str.StartsWith("//"))
        {
          if (str.StartsWith("d"))
            this.blocks.Add(new NB_interface()
            {
              directIp = str.Split(new char[1]
              {
                ' '
              })[1],
              forever = ((str.Split(new char[1]
              {
                ' '
              })[2].Equals("0") ? 1 : 0) != 0 ? 1 : 0) != 0
            });
          else if (str.StartsWith("m"))
            this.blocks.Add(new NB_interface()
            {
              mask = str.Split(new char[1]
              {
                ' '
              })[1],
              forever = ((str.Split(new char[1]
              {
                ' '
              })[2].Equals("0") ? 1 : 0) != 0 ? 1 : 0) != 0
            });
        }
      }
      CLogger.getInstance().info("NetworkBlock: " + (object) this.blocks.Count + " blocks.");
    }

    public static NetworkBlock getInstance()
    {
      return NetworkBlock.nb;
    }

    public bool allowed(string ip)
    {
      if (this.blocks.Count == 0)
        return true;
      foreach (NB_interface nbInterface in this.blocks)
      {
        if (nbInterface.directIp != null && nbInterface.directIp.Equals(ip) && (nbInterface.forever || nbInterface.timeEnd.CompareTo(DateTime.Now) == 1))
          return false;
        if (nbInterface.mask != null)
        {
          string[] strArray1 = ip.Split(new char[1]
          {
            '.'
          });
          string[] strArray2 = nbInterface.mask.Split(new char[1]
          {
            '.'
          });
          bool[] flagArray = new bool[4];
          for (byte index = (byte) 0; (int) index < 4; ++index)
          {
            flagArray[(int) index] = false;
            if (strArray2[(int) index] == "*")
              flagArray[(int) index] = true;
            else if (strArray2[(int) index] == strArray1[(int) index])
              flagArray[(int) index] = true;
            else if (strArray2[(int) index].Contains("/"))
            {
              byte num1 = byte.Parse(strArray2[(int) index].Split(new char[1]
              {
                '/'
              })[0]);
              byte num2 = byte.Parse(strArray2[(int) index].Split(new char[1]
              {
                '/'
              })[1]);
              byte num3 = byte.Parse(strArray1[(int) index]);
              flagArray[(int) index] = (int) num3 >= (int) num1 && (int) num3 <= (int) num2;
            }
          }
          byte num = (byte) 0;
          foreach (bool flag in flagArray)
          {
            if (flag)
              ++num;
          }
          if ((int) num >= 4)
            return false;
        }
      }
      return true;
    }
  }
}
