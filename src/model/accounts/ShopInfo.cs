using PBServer;

namespace PBServer.src.model.accounts
{
    public class ShopInfo
    {
        public int item_id;
        public string item_name;
        public int price_gold;
        public int price_cash;
        public int count;
        public int buy_type, buy_type2;
        public int equip;
        public int good_id;
        public int tag;
        public int title;

        public int getTitleId()
        {
            return this.title;
        }

        public int getGoodId()
        {
            return this.good_id;
        }

        public int getBuyType()
        {
            return this.buy_type;
        }

        public int getBuyType2()
        {
            return this.buy_type2;
        }

        public int getTag()
        {
            return this.tag;
        }

        public int getItemId()
        {
            return this.item_id;
        }

        public string getItemName()
        {
            return this.item_name;
        }

        public int getItemGold()
        {
            return this.price_gold;
        }

        public int getItemCash()
        {
            return this.price_cash;
        }

        public int getItemCount()
        {
            return this.count;
        }

        public int getItemEquip()
        {
            return this.equip;
        }
    }
}
