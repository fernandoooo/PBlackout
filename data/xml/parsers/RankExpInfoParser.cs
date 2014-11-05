using PBServer;
using PBServer.data.model;
using PBServer.data.xml.holders;
using System.IO;
using System.Xml;

namespace PBServer.data.xml.parsers
{
  public class RankExpInfoParser
  {
    private static RankExpInfoParser _instance;
    private RankExpInfoHolder _holder;

    public RankExpInfoParser()
    {
      if (this._holder == null)
        this._holder = RankExpInfoHolder.getInstance();
      string path = "data//RankInfoTemplate.xml";
      if (File.Exists(path))
        this.parse(path);
      else
        CLogger.getInstance().warning("[RankExpInfoParser]: No Have File: " + path);
      if (this._holder == null)
        return;
      this._holder.log();
    }

    public static RankExpInfoParser getInstance()
    {
      if (RankExpInfoParser._instance == null)
        RankExpInfoParser._instance = new RankExpInfoParser();
      return RankExpInfoParser._instance;
    }

    private void parse(string path)
    {
      XmlDocument xmlDocument = new XmlDocument();
      FileStream fileStream = new FileStream(path, FileMode.Open);
      if (fileStream.Length == 0L)
      {
        CLogger.getInstance().warning("[RankExpInfoParser]: File is Empty: " + path);
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
                if ("rank".Equals(xmlNode2.Name))
                {
                  XmlNamedNodeMap xmlNamedNodeMap = (XmlNamedNodeMap) xmlNode2.Attributes;
                  this._holder.addRankExpInfo(new RankExpModel(xmlNamedNodeMap.GetNamedItem("name").Value, int.Parse(xmlNamedNodeMap.GetNamedItem("rank").Value), int.Parse(xmlNamedNodeMap.GetNamedItem("onNextLevel").Value), int.Parse(xmlNamedNodeMap.GetNamedItem("onGPUp").Value), int.Parse(xmlNamedNodeMap.GetNamedItem("onItemUp").Value), int.Parse(xmlNamedNodeMap.GetNamedItem("onAllExp").Value)));
                }
              }
            }
          }
        }
        catch (XmlException ex)
        {
          CLogger.getInstance().warning("[RankExpInfoParser]: Error in file: " + path);
          CLogger.getInstance().info(ex.Message);
        }
        fileStream.Close();
      }
    }
  }
}
