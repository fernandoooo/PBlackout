using PBServer;
using PBServer.src.commons.utils;
using PBServer.src.data.xml.holders;
using PBServer.src.templates;
using System;
using System.IO;
using System.Xml;

namespace PBServer.src.data.xml.parsers
{

    public class ItemTemplateParser
    {
        private ItemTemplateHolder _holder;
        private static ItemTemplateParser _instance;

        public ItemTemplateParser()
        {
            if (this._holder == null)
            {
                this._holder = ItemTemplateHolder.getInstance();
            }
            string path = "data//items";
            if (Directory.Exists(path))
            {
                string[] strArray = Directory.GetFiles(path, "*.xml", SearchOption.AllDirectories);
                for (int i = 0; i < strArray.Length; i++)
                {
                    this.parse(strArray[i]);
                }
            }
            else
            {
                CLogger.getInstance().info("[ItemTemplateParser]: No Have Dir: " + path);
            }
            if (this._holder != null)
            {
                this._holder.log();
            }
        }

        public static ItemTemplateParser getInstance()
        {
            if (_instance == null)
            {
                _instance = new ItemTemplateParser();
            }
            return _instance;
        }

        private void parse(string path)
        {
            XmlDocument document = new XmlDocument();
            FileStream inStream = new FileStream(path, FileMode.Open);
            if (inStream.Length == 0L)
            {
                CLogger.getInstance().info("[ItemTemplateParser]: File is Empty: " + path);
            }
            else
            {
                try
                {
                    document.Load(inStream);
                    for (XmlNode node = document.FirstChild; node != null; node = node.NextSibling)
                    {
                        if ("list".Equals(node.Name))
                        {
                            for (XmlNode node2 = node.FirstChild; node2 != null; node2 = node2.NextSibling)
                            {
                                XmlNamedNodeMap attributes;
                                string name;
                                int num;
                                ParamSet set;
                                if ("weapon".Equals(node2.Name))
                                {
                                    attributes = node2.Attributes;
                                    name = attributes.GetNamedItem("name").Value;
                                    num = int.Parse(attributes.GetNamedItem("id").Value);
                                    set = new ParamSet();
                                    set.set("id", num);
                                    set.set("name", name);
                                    WeaponTemplate item = new WeaponTemplate(set);
                                    this._holder.addWeaponTemplate(item);
                                }
                                else if ("armor".Equals(node2.Name))
                                {
                                    attributes = node2.Attributes;
                                    name = attributes.GetNamedItem("name").Value;
                                    num = int.Parse(attributes.GetNamedItem("id").Value);
                                    set = new ParamSet();
                                    set.set("id", num);
                                    set.set("name", name);
                                    ArmorTemplate template2 = new ArmorTemplate(set);
                                    this._holder.addArmorTemplate(template2);
                                }
                                else if ("cupon".Equals(node2.Name))
                                {
                                    attributes = node2.Attributes;
                                    name = attributes.GetNamedItem("name").Value;
                                    num = int.Parse(attributes.GetNamedItem("id").Value);
                                    set = new ParamSet();
                                    set.set("id", num);
                                    set.set("name", name);
                                    CuponsTemplate template3 = new CuponsTemplate(set);
                                    this._holder.addCuponsTemplate(template3);
                                }
                            }
                        }
                    }
                }
                catch (XmlException exception)
                {
                    CLogger.getInstance().info("[ITP]: Error in file: " + path);
                    CLogger.getInstance().info("[ITP]: " + exception.Message);
                }
                inStream.Close();
            }
        }
    }
}

