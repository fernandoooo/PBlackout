using PBServer;
using PBServer.data.model;
using PBServer.data.xml.holders;
using PBServer.Properties;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace PBServer.data.xml.parsers
{
  public class StartedInventoryItemsParser
  {
    private static StartedInventoryItemsParser _instance;
    private StartedInventoryItemsHolder _holder;

    public StartedInventoryItemsParser()
    {
        if (this._holder == null)
            this._holder = StartedInventoryItemsHolder.getInstance();
        string path = "data//StartedInventoryItems.xml";
        if (File.Exists(path))
            this.parse(path);
        else
            CLogger.getInstance().warning("|[SIP]| No Have File: " + path);
        if (this._holder == null)
            return;
        this._holder.log();
    }

    public static StartedInventoryItemsParser getInstance()
    {
      if (StartedInventoryItemsParser._instance == null)
        StartedInventoryItemsParser._instance = new StartedInventoryItemsParser();
      return StartedInventoryItemsParser._instance;
    }

    private void parse(string path)
    {
      XmlDocument xmlDocument = new XmlDocument();
      FileStream fileStream = new FileStream(path, FileMode.Open);
      if (fileStream.Length == 0L)
      {
        CLogger.getInstance().warning("|[SIIP]| File is Empty: " + path);
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
              if ("initial".Equals(xmlNode2.Name))
              {
                XmlNamedNodeMap xmlNamedNodeMap = (XmlNamedNodeMap) xmlNode2.Attributes;
                List<PlayerTemplateInventory> list = new List<PlayerTemplateInventory>();
                int num1 = int.Parse(xmlNamedNodeMap.GetNamedItem("id").Value);
                string name = xmlNamedNodeMap.GetNamedItem("name").Value;
                int num2 = int.Parse(xmlNamedNodeMap.GetNamedItem("slot").Value);
                int num3 = int.Parse(xmlNamedNodeMap.GetNamedItem("equip").Value);
                int num4 = int.Parse(xmlNamedNodeMap.GetNamedItem("count").Value);
                int num5 = int.Parse(xmlNamedNodeMap.GetNamedItem("equip2").Value);
                this._holder.addInventoryStatic(new PlayerTemplateInventory()
                {
                  id = num1,
                  name = name,
                  slot = num2,
                  onEquip = num3,
                  count = num4,
                  equip = num5
                });
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
