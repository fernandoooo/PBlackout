using PBServer.src.commons.utils;

namespace PBServer.src.templates
{
    public class WeaponNHTemplate : ItemTemplate
    {
        public static int _dalnost;
        public static int _razbros;
        public static int _uron;

        public WeaponNHTemplate(ParamSet set)
        {
            this.setParameters(set);
            this._id = set.getInt32("id");
            this._name = set.getString("name");
            this._price = set.getInt32("price");
            this._count = set.getInt32("count");
            WeaponNHTemplate._uron = set.getInt32("uron", 30);
            WeaponNHTemplate._dalnost = set.getInt32("dalnost", 100);
            WeaponNHTemplate._razbros = set.getInt32("uron", 10);
        }
    }
}
