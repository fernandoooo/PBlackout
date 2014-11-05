using PBServer;
using PBServer.data.model;
using PBServer.data.xml.holders;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System;

namespace PBServer.data.xml.parsers
{
    public class PlayerTemplateParser
    {
        private static PlayerTemplateParser _instance;
        private PlayerTemplateHolder _holder;

        public PlayerTemplateParser()
        {
            if (this._holder == null)
                this._holder = PlayerTemplateHolder.getInstance();
            string path = "data//PlayerTemplate.xml";
            if (File.Exists(path))
                this.parse(path);
            else
                CLogger.getInstance().info("[PlayerTemplateParser]: No Have File: " + path);
            if (this._holder == null)
                return;
            this._holder.log();
        }

        public static PlayerTemplateParser getInstance()
        {
            if (PlayerTemplateParser._instance == null)
                PlayerTemplateParser._instance = new PlayerTemplateParser();
            return PlayerTemplateParser._instance;
        }

        private void parse(string path)
        {
            XmlDocument xmlDocument = new XmlDocument();
            FileStream fileStream = new FileStream(path, FileMode.Open);
            if (fileStream.Length == 0L)
            {
                CLogger.getInstance().info("[PlayerTemplateParser]: File is Empty: " + path);
            }
            else
            {
                try
                {
                    xmlDocument.Load((Stream)fileStream);
                    for (XmlNode xmlNode1 = xmlDocument.FirstChild; xmlNode1 != null; xmlNode1 = xmlNode1.NextSibling)
                    {
                        if ("list".Equals(xmlNode1.Name))
                        {
                            for (XmlNode xmlNode2 = xmlNode1.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
                            {
                                if ("template".Equals(xmlNode2.Name))
                                {
                                    XmlNamedNodeMap xmlNamedNodeMap = (XmlNamedNodeMap)xmlNode2.Attributes;
                                    List<PlayerTemplateInventory> _startInventory = new List<PlayerTemplateInventory>();
                                    int id = int.Parse(xmlNamedNodeMap.GetNamedItem("id").Value);
                                    int rank = int.Parse(xmlNamedNodeMap.GetNamedItem("rank").Value);
                                    int gp = int.Parse(xmlNamedNodeMap.GetNamedItem("gp").Value);
                                    int exp = int.Parse(xmlNamedNodeMap.GetNamedItem("exp").Value);

                                    string itemid = xmlNamedNodeMap.GetNamedItem("itemid").Value;
                                    string[] _temp = itemid.Split(new char[] { ';' });
                                    for (int i = 0; i < _temp.Length - 1; i++)
                                    {
                                        string[] _temp2 = _temp[i].Split(new char[] { ',' });
                                        PlayerTemplateInventory _invTemp = new PlayerTemplateInventory();
                                        _invTemp.id = Int32.Parse(_temp2[0]);
                                        _invTemp.slot = Int32.Parse(_temp2[1]);
                                        _invTemp.onEquip = Int32.Parse(_temp2[2]);
                                        _startInventory.Add(_invTemp);
                                    }
                                    PlayerTemplate playerTemplate = new PlayerTemplate(id, rank, exp, gp, _startInventory);
                                    _holder.addPlayerTemplateInfo(playerTemplate);
                                }
                            }
                        }
                    }
                }
                catch (XmlException ex)
                {
                    CLogger.getInstance().info("[PlayerTemplateParser]: Error in file: " + path);
                    throw ex;
                }
                fileStream.Close();
            }
        }
    }
}
