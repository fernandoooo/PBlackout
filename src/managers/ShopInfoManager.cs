using MySql.Data.MySqlClient;
using PBServer;
using PBServer.db;
using PBServer.model.players;
using PBServer.src.model.accounts;
using System;
using System.Collections.Generic;
using System.Data;

namespace PBServer.src.managers
{
    public class ShopInfoManager
    {
        private static ShopInfoManager acm = new ShopInfoManager();
        private int id = 1;
        public int dbstatus = 0;
        protected List<ShopInfo> _accounts = new List<ShopInfo>();

        static ShopInfoManager()
        {
        }

        public ShopInfoManager()
        {
            try
            {
                using (MySqlConnection mySqlConnection = SQLjec.getInstance().conn())
                {
                    MySqlCommand command = mySqlConnection.CreateCommand();
                    mySqlConnection.Open();
                    command.CommandText = "SELECT * FROM shop";
                    command.CommandType = CommandType.Text;
                    MySqlDataReader mySqlDataReader = command.ExecuteReader();
                    while (mySqlDataReader.Read())
                    {
                        ShopInfo account = new ShopInfo();
                        account.good_id = mySqlDataReader.GetInt32("good_id");
                        account.item_id = mySqlDataReader.GetInt32("item_id");
                        account.item_name = mySqlDataReader.GetString("item_name");
                        account.price_gold = mySqlDataReader.GetInt32("price_gold");
                        account.price_cash = mySqlDataReader.GetInt32("price_cash");
                        account.count = mySqlDataReader.GetInt32("count");
                        account.buy_type = mySqlDataReader.GetInt32("buy_type");
                        account.buy_type2 = mySqlDataReader.GetInt32("buy_type2");
                        account.equip = mySqlDataReader.GetInt32("equip");
                        account.tag = mySqlDataReader.GetInt32("tag");
                        account.title = mySqlDataReader.GetInt32("title");
                        this._accounts.Add(account);
                        ++this.id;
                    }
                    CLogger.getInstance().info("|[SIM]| Foram carregadas " + (object)this._accounts.Count + " informações da loja.");
                }
            }
            catch (Exception ex)
            {
                this.dbstatus = -100;
                CLogger.getInstance().error("|[SIM]| Nenhuma conta foi encontrada");
                CLogger.getInstance().error("|[SIM]| " + ((object)ex).ToString());
            }
        }

        public static ShopInfoManager getInstance()
        {
            return ShopInfoManager.acm;
        }

        public List<ShopInfo> getShopItens()
        {
            return this._accounts;
        }

        public ShopInfo getInfoItem(int item_id)
        {
            ShopInfo result;
            foreach (ShopInfo current in this._accounts)
            {
                if (current.item_id == item_id)
                {
                    result = current;
                    return result;
                }
            }
            result = null;
            return result;
        }

        public ShopInfo getInfoItem2(int good_id)
        {
            ShopInfo result;
            foreach (ShopInfo current in this._accounts)
            {
                if (current.good_id == good_id)
                {
                    result = current;
                    return result;
                }
            }
            result = null;
            return result;
        }
    }
}
