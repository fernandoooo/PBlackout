// Type: PBServer.src.data.xml.parsers.ChannelInfoParser
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.src.model;
using System.IO;
using System.Xml;
using PBServer.src.data.xml.holders;

namespace PBServer.src.data.xml.parsers
{
  public class ChannelInfoParser
  {
    private static ChannelInfoParser _instance;
    private ChannelInfoHolder _holder;

    public ChannelInfoParser()
    {
      if (this._holder == null)
        this._holder = ChannelInfoHolder.getInstance();
      string path = "data//channelTemplate.xml";
      if (File.Exists(path))
        this.parse(path);
      else
        CLogger.getInstance().info("[ChannelInfoParser]: No Have File: " + path);
      if (this._holder == null)
        return;
      this._holder.log();
    }

    public static ChannelInfoParser getInstance()
    {
      if (ChannelInfoParser._instance == null)
        ChannelInfoParser._instance = new ChannelInfoParser();
      return ChannelInfoParser._instance;
    }

    private void parse(string path)
    {
      XmlDocument xmlDocument = new XmlDocument();
      FileStream fileStream = new FileStream(path, FileMode.Open);
      if (fileStream.Length == 0L)
      {
        CLogger.getInstance().info("[ChannelInfoParser]: File is Empty: " + path);
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
                if ("channel".Equals(xmlNode2.Name))
                {
                  XmlNamedNodeMap xmlNamedNodeMap = (XmlNamedNodeMap) xmlNode2.Attributes;
                  int id1 = int.Parse(xmlNamedNodeMap.GetNamedItem("id").Value);
                  int type = int.Parse(xmlNamedNodeMap.GetNamedItem("type").Value);
                  string str = xmlNamedNodeMap.GetNamedItem("name").Value;
                  string announce = xmlNamedNodeMap.GetNamedItem("announce").Value;
                  int max_players = int.Parse(xmlNamedNodeMap.GetNamedItem("max_players").Value);
                  this._holder.addChannelInfo(new Channel(id1, type, announce, max_players));
                }
              }
            }
          }
        }
        catch (XmlException ex)
        {
          CLogger.getInstance().info("[ChannelInfoParser]: Error in file: " + path);
          CLogger.getInstance().info(ex.Message);
        }
        fileStream.Close();
      }
    }
  }
}
