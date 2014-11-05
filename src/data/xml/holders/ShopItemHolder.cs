using PBServer;
using PBServer.src.commons.data.holders;
using PBServer.src.templates;
using System.Collections.Generic;

namespace PBServer.src.data.xml.holders
{
    internal class ShopItemHolder : IHolder
    {
        private static ShopItemHolder _instance;
        private static List<ShopTemplate> _weapons;

        public ShopItemHolder()
        {
            ShopItemHolder._weapons = new List<ShopTemplate>();
        }

        public static ShopItemHolder getInstance()
        {
            if (ShopItemHolder._instance == null)
                ShopItemHolder._instance = new ShopItemHolder();
            return ShopItemHolder._instance;
        }

        public void addShopTemplate(ShopTemplate item)
        {
            ShopItemHolder._weapons.Add(item);
        }

        public ShopTemplate getShopTemplate(int id)
        {
            return ShopItemHolder._weapons[id];
        }

        public List<ShopTemplate> getAllShopItens()
        {
            return ShopItemHolder._weapons;
        }

        public void log()
        {
            CLogger.getInstance().info("|[SIH]| Foram carregados " + (object)ShopItemHolder._weapons.Count + " equipamentos da loja.");
        }

        public void clear()
        {
            ShopItemHolder._weapons.Clear();
        }
    }
}
