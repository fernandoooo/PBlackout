using PBServer.src.commons.utils;

namespace PBServer.src.templates
{
    public class WeaponCTemplate : ItemTemplate
    {
        public static int _dalnost;
        public static int _razbros;
        public static int _uron;

        public WeaponCTemplate(ParamSet set)
        {
            this.setParameters(set);
            this._id = set.getInt32("id");
            this._name = set.getString("name");
            this._price = set.getInt32("price");
            this._count = set.getInt32("count");
            WeaponCTemplate._uron = set.getInt32("uron", 30);
            WeaponCTemplate._dalnost = set.getInt32("dalnost", 100);
            WeaponCTemplate._razbros = set.getInt32("uron", 10);
        }
    }
}
