// Type: PBServer.network.web.function.Utilits
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using System;

namespace PBServer.network.web.function
{
  public static class Utilits
  {
    public static string[] getAccInfo(string data)
    {
      string[] strArray1 = new string[2];
      try
      {
        string[] strArray2 = data.Split(new string[1]
        {
          "login="
        }, StringSplitOptions.None)[1].Split(new string[1]
        {
          "&"
        }, StringSplitOptions.None)[0].Split(new char[1]
        {
          ' '
        });
        strArray1[0] = strArray2[0];
        string[] strArray3 = data.Split(new string[1]
        {
          "password="
        }, StringSplitOptions.None)[1].Split(new string[1]
        {
          "&"
        }, StringSplitOptions.None)[0].Split(new string[1]
        {
          " "
        }, StringSplitOptions.None);
        strArray1[1] = strArray3[0];
      }
      catch
      {
      }
      return strArray1;
    }

    public static string getLoginWeb(string data)
    {
      string[] strArray = data.Split(new string[1]
      {
        "login="
      }, StringSplitOptions.None);
      if (strArray.Length <= 1)
        return (string) null;
      return strArray[1].Split(new char[2]
      {
        ' ',
        '&'
      })[0];
    }

    public static string getCookieAuth(string data)
    {
      string str = "";
      try
      {
        str = data.Split(new string[1]
        {
          "auth="
        }, StringSplitOptions.None)[1].Split(new string[1]
        {
          "\r\n\r\n"
        }, StringSplitOptions.None)[0].Split(new string[1]
        {
          ";"
        }, StringSplitOptions.RemoveEmptyEntries)[0];
      }
      catch
      {
      }
      return str;
    }

    public static string tokenGenerator()
    {
      string str = "";
      Random random = new Random();
      char[] chArray = new char[36]
      {
        'a',
        'b',
        'c',
        'd',
        'e',
        'f',
        'g',
        'h',
        'i',
        'j',
        'k',
        'l',
        'm',
        'n',
        'o',
        'p',
        'q',
        'r',
        's',
        't',
        'u',
        'v',
        'w',
        'x',
        'y',
        'z',
        '1',
        '2',
        '3',
        '4',
        '5',
        '6',
        '7',
        '8',
        '9',
        '0'
      };
      for (int index = 0; index < 45; ++index)
        str = str + Convert.ToString(chArray[random.Next(0, 36)]);
      return str;
    }
  }
}
