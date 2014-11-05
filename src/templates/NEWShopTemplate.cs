using PBServer.src.commons.utils;

namespace PBServer.src.templates
{
    public class NEWShopTemplate : ItemTemplate
    {
        public static int _dalnost;
        public static int _razbros;
        public static int _uron;

        public NEWShopTemplate(ParamSet set)
        {
            this.setParameters(set);
            this._id = set.getInt32("id");
            this._name = set.getString("name");
            this._price = set.getInt32("price");
            this._count = set.getInt32("count");
            this._type2 = set.getInt32("buy_type2");
            this._buy_type = set.getInt32("buy_type");
            this._price2 = set.getInt32("price2");
            NEWShopTemplate._uron = set.getInt32("uron", 30);
            NEWShopTemplate._dalnost = set.getInt32("dalnost", 100);
            NEWShopTemplate._razbros = set.getInt32("uron", 10);
        }
    }
}
