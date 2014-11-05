// Type: PBServer.src.data.xml.parsers.GameserverInfoParser
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.src.data.model;
using PBServer.src.data.xml.holders;
using System.IO;
using System.Xml;

namespace PBServer.src.data.xml.parsers
{
  public class GameServerInfoParser
  {
    private static GameServerInfoParser _instance;
    private GameServerInfoHolder _holder;

    public GameServerInfoParser()
    {
      if (this._holder == null)
        this._holder = GameServerInfoHolder.getInstance();
      string path = "data//gameservers.xml";
      if (File.Exists(path))
        this.parse(path);
      else
        CLogger.getInstance().info("[GameServerInfoParser]: No Have File: " + path);
      if (this._holder == null)
        return;
      this._holder.log();
    }

    public static GameServerInfoParser getInstance()
    {
      if (GameServerInfoParser._instance == null)
        GameServerInfoParser._instance = new GameServerInfoParser();
      return GameServerInfoParser._instance;
    }

    private void parse(string path)
    {
      XmlDocument xmlDocument = new XmlDocument();
      FileStream fileStream = new FileStream(path, FileMode.Open);
      if (fileStream.Length == 0L)
      {
        CLogger.getInstance().info("[GameServerInfoParser]: File is Empty: " + path);
      }
      else
      {
        try
        {
          xmlDocument.Load((Stream) fileStream);
          for (XmlNode xmlNode1 = xmlDocument.FirstChild; xmlNode1 != null; xmlNode1 = xmlNode1.NextSibling)
          {
            if ("list".Equals(xmlNode1.Name))
            {
              for (XmlNode xmlNode2 = xmlNode1.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
              {
                if ("gameserver".Equals(xmlNode2.Name))
                {
                  XmlNamedNodeMap xmlNamedNodeMap = (XmlNamedNodeMap) xmlNode2.Attributes;
                  this._holder.addGameServerInfo(new GameServerInfo(xmlNamedNodeMap.GetNamedItem("name").Value, int.Parse(xmlNamedNodeMap.GetNamedItem("id").Value), xmlNamedNodeMap.GetNamedItem("password").Value, int.Parse(xmlNamedNodeMap.GetNamedItem("type").Value), int.Parse(xmlNamedNodeMap.GetNamedItem("max_players").Value), xmlNamedNodeMap.GetNamedItem("ip").Value));
                }
              }
            }
          }
        }
        catch (XmlException ex)
        {
          CLogger.getInstance().info("[GameServerInfoParser]: Error in file: " + path);
          CLogger.getInstance().info("[GameServerInfoParser]: " + ex.Message);
        }
        fileStream.Close();
      }
    }
  }
}
