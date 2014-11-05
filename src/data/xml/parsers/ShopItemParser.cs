namespace PBServer.src.data.xml.parsers
{
    using PBServer;
    using PBServer.src.commons.utils;
    using PBServer.src.data.xml.holders;
    using PBServer.src.templates;
    using System;
    using System.IO;
    using System.Xml;

    public class ShopItemParser
    {
        private ShopItemHolder _holder;
        private static ShopItemParser _instance;

        public ShopItemParser()
        {
            if (this._holder == null)
            {
                this._holder = ShopItemHolder.getInstance();
            }
            string path = "data//shopItems";
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
                CLogger.getInstance().info("|[SIP]| No Have Dir: " + path);
            }
            if (this._holder != null)
            {
                this._holder.log();
            }
        }

        public static ShopItemParser getInstance()
        {
            if (_instance == null)
            {
                _instance = new ShopItemParser();
            }
            return _instance;
        }

        private void parse(string path)
        {
            XmlDocument document = new XmlDocument();
            FileStream inStream = new FileStream(path, FileMode.Open);
            if (inStream.Length == 0L)
            {
                CLogger.getInstance().info("|[SIP]| File is Empty: " + path);
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
                                int price;
                                int count;
                                int price2;
                                int buy_type2;
                                int buy_type;
                                int noname;
                                int _new;
                                ParamSet set;
                                if ("weapon".Equals(node2.Name))
                                {
                                    attributes = node2.Attributes;
                                    name = attributes.GetNamedItem("name").Value;
                                    num = int.Parse(attributes.GetNamedItem("id").Value);
                                    price = int.Parse(attributes.GetNamedItem("price").Value);
                                    count = int.Parse(attributes.GetNamedItem("count").Value);
                                    price2 = int.Parse(attributes.GetNamedItem("price2").Value);
                                    buy_type2 = int.Parse(attributes.GetNamedItem("buy_type2").Value);
                                    buy_type = int.Parse(attributes.GetNamedItem("buy_type").Value);
                                    noname = int.Parse(attributes.GetNamedItem("noname").Value);
                                    _new = int.Parse(attributes.GetNamedItem("new").Value);
                                    set = new ParamSet();
                                    set.set("id", num);
                                    set.set("name", name);
                                    set.set("price", price);
                                    set.set("count", count);
                                    set.set("price2", price2);
                                    set.set("buy_type2", buy_type2);
                                    set.set("buy_type", buy_type);
                                    set.set("noname", noname);
                                    set.set("new", _new);
                                    ShopTemplate item = new ShopTemplate(set);
                                    this._holder.addShopTemplate(item);
                                }
                            }
                        }
                    }
                }
                catch (XmlException exception)
                {
                    CLogger.getInstance().info("|[SIP]| Error in file: " + path);
                    CLogger.getInstance().info("|[SIP]| " + exception.Message);
                }
                inStream.Close();
            }
        }
    }
}
