// Type: PBServer.src.data.xml.holders.ItemTemplateHolder
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.src.commons.data.holders;
using PBServer.src.templates;
using System.Collections.Generic;

namespace PBServer.src.data.xml.holders
{
    internal class ItemTemplateHolder : IHolder
    {
        private static ItemTemplateHolder _instance;
        private static List<WeaponTemplate> _weapons;
        private static List<ArmorTemplate> _armors;
        private static List<CuponsTemplate> _cupons;

        public ItemTemplateHolder()
        {
            ItemTemplateHolder._weapons = new List<WeaponTemplate>();
            ItemTemplateHolder._armors = new List<ArmorTemplate>();
            ItemTemplateHolder._cupons = new List<CuponsTemplate>();
        }

        public static ItemTemplateHolder getInstance()
        {
            if (ItemTemplateHolder._instance == null)
                ItemTemplateHolder._instance = new ItemTemplateHolder();
            return ItemTemplateHolder._instance;
        }

        public void addWeaponTemplate(WeaponTemplate item)
        {
            ItemTemplateHolder._weapons.Add(item);
        }

        public WeaponTemplate getWeaponTemplate(int id)
        {
            return ItemTemplateHolder._weapons[id];
        }

        public List<WeaponTemplate> getAllWeapons()
        {
            return ItemTemplateHolder._weapons;
        }
        //
        public void addArmorTemplate(ArmorTemplate item)
        {
            ItemTemplateHolder._armors.Add(item);
        }

        public ArmorTemplate getArmorTemplate(int id)
        {
            return ItemTemplateHolder._armors[id];
        }

        public List<ArmorTemplate> getAllArmors()
        {
            return ItemTemplateHolder._armors;
        }
        public void addCuponsTemplate(CuponsTemplate item)
        {
            ItemTemplateHolder._cupons.Add(item);
        }

        public CuponsTemplate getCuponsTemplate(int id)
        {
            return ItemTemplateHolder._cupons[id];
        }

        public List<CuponsTemplate> getAllCupons()
        {
            return ItemTemplateHolder._cupons;
        }

        public void log()
        {
            CLogger.getInstance().info("|[ITH]| Foram carregados " + (object)ItemTemplateHolder._weapons.Count + " Weapons.");
            CLogger.getInstance().info("|[ITH]| Foram carregados " + (object)ItemTemplateHolder._armors.Count + " Armors.");
            CLogger.getInstance().info("|[ITH]| Foram carregados " + (object)ItemTemplateHolder._cupons.Count + " Cupons.");
        }

        public void clear()
        {
            ItemTemplateHolder._weapons.Clear();
        }
    }
}
