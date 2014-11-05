// Type: PBServer.src.managers.AccountManager
// Assembly: PBServerС, Version=0.7.3.28, Culture=neutral, PublicKeyToken=null
// MVID: 4EA1E2FB-F867-4553-9FBB-41B5E9555B25
// Assembly location: C:\Users\Лана\Desktop\Server files\PBEU1\Debug\PBServerС.exe

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
    public class AccountManager
    {
        private static AccountManager acm = new AccountManager();
        private int id = 1;
        public int dbstatus = 0;
        protected List<Account> _accounts = new List<Account>();

        static AccountManager()
        {
        }

        public AccountManager()
        {
            try
            {
                using (MySqlConnection mySqlConnection = SQLjec.getInstance().conn())
                {
                    MySqlCommand command = mySqlConnection.CreateCommand();
                    mySqlConnection.Open();
                    command.CommandText = "SELECT * FROM accounts";
                    command.CommandType = CommandType.Text;
                    MySqlDataReader mySqlDataReader = command.ExecuteReader();
                    while (mySqlDataReader.Read())
                    {
                        Account account = new Account();
                        account.id = this.id;
                        account.name = mySqlDataReader.GetString("login");
                        account.player_name = mySqlDataReader.GetString("player_name");
                        account.setPlayerId(mySqlDataReader.GetInt32("player_id"));
                        account.password = mySqlDataReader.GetString("password");
                        account.access_level = mySqlDataReader.GetInt32("access_level");
                        account.gp = mySqlDataReader.GetInt32("gp");
                        account.setRank(mySqlDataReader.GetInt32("rank"));
                        account.money = mySqlDataReader.GetInt32("money");
                        account.exp = mySqlDataReader.GetInt32("exp");
                        account.clan_id = mySqlDataReader.GetInt32("clan_id");
                        account.pc_cafe = mySqlDataReader.GetInt32("pc_cafe");
                        account._statistic = new PlayerStats();
                        account._statistic.setFights(mySqlDataReader.GetInt32("fights_s"), true);
                        account._statistic.setFights(mySqlDataReader.GetInt32("fights_ns"), true);
                        account._statistic.setWinFights(mySqlDataReader.GetInt32("fights_win_s"), true);
                        account._statistic.setWinFights(mySqlDataReader.GetInt32("fights_win_ns"), true);
                        account._statistic.setLostFights(mySqlDataReader.GetInt32("fights_lost_s"), true);
                        account._statistic.setLostFights(mySqlDataReader.GetInt32("fights_lost_ns"), true);
                        account._statistic.setKills(mySqlDataReader.GetInt32("kills_count_s"), true);
                        account._statistic.setKills(mySqlDataReader.GetInt32("kills_count_ns"), true);
                        account._statistic.setDeaths(mySqlDataReader.GetInt32("deaths_count_s"), true);
                        account._statistic.setDeaths(mySqlDataReader.GetInt32("deaths_count_ns"), true);
                        account._statistic.setEscapes(mySqlDataReader.GetInt32("escapes_s"), true);
                        account._statistic.setEscapes(mySqlDataReader.GetInt32("escapes_ns"), true);
                        account._status = mySqlDataReader.GetInt32("online");
                        account.count_friend = mySqlDataReader.GetInt32("count_friend");
                        account.weapon_primary = mySqlDataReader.GetInt32("weapon_primary");
                        account.weapon_secondary = mySqlDataReader.GetInt32("weapon_secondary");
                        account.weapon_melee = mySqlDataReader.GetInt32("weapon_melee");
                        account.weapon_thrown_normal = mySqlDataReader.GetInt32("weapon_thrown_normal");
                        account.weapon_thrown_special = mySqlDataReader.GetInt32("weapon_thrown_special");
                        account.char_red = mySqlDataReader.GetInt32("char_red");
                        account.char_blue = mySqlDataReader.GetInt32("char_blue");
                        account.char_helmet = mySqlDataReader.GetInt32("char_helmet");
                        account.char_dino = mySqlDataReader.GetInt32("char_dino");
                        account.char_beret = mySqlDataReader.GetInt32("char_beret");
                        account.brooch = mySqlDataReader.GetInt32("brooch");
                        account.insignia = mySqlDataReader.GetInt32("insignia");
                        account.medal = mySqlDataReader.GetInt32("medal");
                        account.blue_order = mySqlDataReader.GetInt32("blue_order");
                        account.title_slot_count = mySqlDataReader.GetInt32("title_slot_count");
                        this._accounts.Add(account);
                        ++this.id;
                    }
                    CLogger.getInstance().extra_info("|[ACM]| Foram carregadas " + (object)this._accounts.Count + " contas.");
                }
            }
            catch (Exception ex)
            {
                this.dbstatus = -100;
                CLogger.getInstance().error("|[ACM]| Nenhuma conta foi encontrada");
                CLogger.getInstance().error("|[ACM]| " + ((object)ex).ToString());
            }
        }

        public static AccountManager getInstance()
        {
            return AccountManager.acm;
        }

        public bool accountExists(string user)
        {
            return AccountManager.getInstance().get(user) != null;
        }

        public Account SearchAccountInDB(string login, string password)
        {
            using (MySqlConnection mySqlConnection = SQLjec.getInstance().conn())
            {
                MySqlCommand command = mySqlConnection.CreateCommand();
                Account account = new Account();
                mySqlConnection.Open();
                command.CommandText = "SELECT * FROM accounts WHERE login='" + login + "' AND password='" + password + "' LIMIT 1;";
                command.CommandType = CommandType.Text;
                MySqlDataReader mySqlDataReader = command.ExecuteReader();
                if (!mySqlDataReader.Read())
                    return (Account)null;
                account.id = this._accounts.Count + 1;
                account.name = mySqlDataReader.GetString("login");
                account.player_name = mySqlDataReader.GetString("player_name");
                account.name_color = mySqlDataReader.GetInt32("name_color");
                account.setPlayerId(mySqlDataReader.GetInt32("player_id"));
                account.password = mySqlDataReader.GetString("password");
                account.access_level = mySqlDataReader.GetInt32("access_level");
                account.gp = mySqlDataReader.GetInt32("gp");
                account.setRank(mySqlDataReader.GetInt32("rank"));
                account.money = mySqlDataReader.GetInt32("money");
                account.exp = mySqlDataReader.GetInt32("exp");
                account.clan_id = mySqlDataReader.GetInt32("clan_id");
                account.pc_cafe = mySqlDataReader.GetInt32("pc_cafe");
                account._statistic = new PlayerStats();
                account._statistic.setFights(mySqlDataReader.GetInt32("fights_s"), true);
                account._statistic.setFights(mySqlDataReader.GetInt32("fights_ns"), true);
                account._statistic.setWinFights(mySqlDataReader.GetInt32("fights_win_s"), true);
                account._statistic.setWinFights(mySqlDataReader.GetInt32("fights_win_ns"), true);
                account._statistic.setLostFights(mySqlDataReader.GetInt32("fights_lost_s"), true);
                account._statistic.setLostFights(mySqlDataReader.GetInt32("fights_lost_ns"), true);
                account._statistic.setKills(mySqlDataReader.GetInt32("kills_count_s"), true);
                account._statistic.setKills(mySqlDataReader.GetInt32("kills_count_ns"), true);
                account._statistic.setDeaths(mySqlDataReader.GetInt32("deaths_count_s"), true);
                account._statistic.setDeaths(mySqlDataReader.GetInt32("deaths_count_ns"), true);
                account._statistic.setEscapes(mySqlDataReader.GetInt32("escapes_s"), true);
                account._statistic.setEscapes(mySqlDataReader.GetInt32("escapes_ns"), true);
                account._status = mySqlDataReader.GetInt32("online");
                account.count_friend = mySqlDataReader.GetInt32("count_friend");
                account.weapon_primary = mySqlDataReader.GetInt32("weapon_primary");
                account.weapon_secondary = mySqlDataReader.GetInt32("weapon_secondary");
                account.weapon_melee = mySqlDataReader.GetInt32("weapon_melee");
                account.weapon_thrown_normal = mySqlDataReader.GetInt32("weapon_thrown_normal");
                account.weapon_thrown_special = mySqlDataReader.GetInt32("weapon_thrown_special");
                account.char_red = mySqlDataReader.GetInt32("char_red");
                account.char_blue = mySqlDataReader.GetInt32("char_blue");
                account.char_helmet = mySqlDataReader.GetInt32("char_helmet");
                account.char_dino = mySqlDataReader.GetInt32("char_dino");
                account.char_beret = mySqlDataReader.GetInt32("char_beret");
                account.brooch = mySqlDataReader.GetInt32("brooch");
                account.insignia = mySqlDataReader.GetInt32("insignia");
                account.medal = mySqlDataReader.GetInt32("medal");
                account.blue_order = mySqlDataReader.GetInt32("blue_order");
                account.title_slot_count = mySqlDataReader.GetInt32("title_slot_count");
                this._accounts.Add(account);
                CLogger.getInstance().extra_info("|[ACM]| Está carregando a conta " + login);
                return account;
            }
        }

        public bool isCookie(string cookie)
        {
            foreach (Account account in AccountManager.getInstance().getAccounts())
            {
                if (account.cookie == cookie)
                    return true;
            }
            return false;
        }

        public void setCookie(string cookie, string login)
        {
            foreach (Account account in AccountManager.getInstance().getAccounts())
            {
                if (account.name == login)
                    account.cookie = cookie;
            }
        }

        public void deleteCookie(string cookie)
        {
            foreach (Account account in AccountManager.getInstance().getAccounts())
            {
                if (account.cookie == cookie)
                    account.cookie = "";
            }
        }

        public void updatePlayer(Account p)
        {
            try
            {
                using (MySqlConnection mySqlConnection = SQLjec.getInstance().conn())
                {
                    MySqlCommand command = mySqlConnection.CreateCommand();
                    mySqlConnection.Open();
                    CLogger.getInstance().warning("Atualizando informações do jogador " + p.getPlayerName());
                    command.CommandText = "UPDATE accounts SET rank='" + (object)p.getRank() + "' WHERE player_id='" + p.player_id.ToString() + "';";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE accounts SET exp='" + (object)p.getExp() + "' WHERE player_id='" + p.player_id.ToString() + "';";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE accounts SET gp='" + (object)p.getGP() + "' WHERE player_id='" + p.player_id.ToString() + "';";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE accounts SET kills_count_s='" + p._statistic.getKills(true).ToString() + "' WHERE player_id='" + p.player_id.ToString() + "';";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE accounts SET deaths_count_s='" + p._statistic.getDeaths(true).ToString() + "' WHERE player_id='" + p.player_id.ToString() + "';";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE accounts SET kills_count_ns='" + p._statistic.getKills(true).ToString() + "' WHERE player_id='" + p.player_id.ToString() + "';";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE accounts SET deaths_count_ns='" + p._statistic.getDeaths(true).ToString() + "' WHERE player_id='" + p.player_id.ToString() + "';";
                    command.ExecuteNonQuery();
                }
                this.get(p.name).setRank(p.getRank());
                this.get(p.name).setExp(p.getExp());
                this.get(p.name).setGP(p.getGP());
            }
            catch (Exception ex)
            {
                CLogger.getInstance().warning("|[AM]| ERROR UPDATE player: " + ((object)ex).ToString());
            }
        }

        public Account get(string username)
        {
            foreach (Account account in AccountManager.getInstance().getAccounts())
            {
                if (account.name.ToLower() == username.ToLower())
                    return account;
            }
            return (Account)null;
        }

        public Account getAccountInObjectId(int player_id)
        {
            Account result;
            foreach (Account current in AccountManager.getInstance().getAccounts())
            {
                if (current.player_id == player_id)
                {
                    result = current;
                    return result;
                }
            }
            result = null;
            return result;
        }

        public Account getAccountInPlayerId(int object_id)
        {
            Account result;
            foreach (Account current in this._accounts)
            {
                if (current.player_id == object_id)
                {
                    result = current;
                    return result;
                }
            }
            result = null;
            return result;
        }

        public Account getAccountInName(string player_name)
        {
            Account result;
            foreach (Account current in AccountManager.getInstance().getAccounts())
            {
                if (current.player_name == player_name)
                {
                    result = current;
                    return result;
                }
            }
            result = null;
            return result;
        }
        /*{
            foreach (Account account in AccountManager.getInstance().getAccounts())
            {
                if (account.player_id == player_id)
                    return account;
            }
            return (Account)null;
        }*/

        public int getPlayerId(string name)
        {
            using (MySqlConnection mySqlConnection = SQLjec.getInstance().conn())
            {
                MySqlCommand command = mySqlConnection.CreateCommand();
                mySqlConnection.Open();
                command.CommandText = "SELECT player_id FROM accounts WHERE player_name='" + name + "' LIMIT 1;";
                command.CommandType = CommandType.Text;
                MySqlDataReader mySqlDataReader = command.ExecuteReader();
                if (mySqlDataReader.Read())
                    return mySqlDataReader.GetInt32("player_id");
            }
            return -1;
        }

        public int getCountForItemId(int item_id, int owner_id)
        {
            using (MySqlConnection mySqlConnection = SQLjec.getInstance().conn())
            {
                MySqlCommand command = mySqlConnection.CreateCommand();
                mySqlConnection.Open();
                command.CommandText = "SELECT count FROM items WHERE item_id='" + item_id + "' AND owner_id='" + owner_id + "';";
                command.CommandType = CommandType.Text;
                MySqlDataReader mySqlDataReader = command.ExecuteReader();
                if (mySqlDataReader.Read())
                    return mySqlDataReader.GetInt32("count");
            }
            return 0;
        }

        public bool isPlayerNameExist(string name)
        {
            try
            {
                using (MySqlConnection mySqlConnection = SQLjec.getInstance().conn())
                {
                    MySqlCommand command = mySqlConnection.CreateCommand();
                    mySqlConnection.Open();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT COUNT(*) FROM accounts WHERE player_name='" + name + "'";
                    return Convert.ToInt32(command.ExecuteScalar()) != 0;
                }
            }
            catch
            {
                return true;
            }
        }

        public void AddItem(Account p, int itemid, int slot)
        {
            try
            {
                using (MySqlConnection mySqlConnection = SQLjec.getInstance().conn())
                {
                    MySqlCommand command = mySqlConnection.CreateCommand();
                    mySqlConnection.Open();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "UPDATE accounts SET player_name='" + p.getPlayerName() + "' WHERE player_id='" + p.player_id.ToString() + "';";
                    command.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public void UpdateMGP(int player_id, int gold, int cash)
        {
            try
            {
                using (MySqlConnection mySqlConnection = SQLjec.getInstance().conn())
                {
                    MySqlCommand command = mySqlConnection.CreateCommand();
                    mySqlConnection.Open();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "UPDATE accounts SET gp='" + gold + "' WHERE player_id='" + player_id + "';";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE accounts SET money='" + cash + "' WHERE player_id='" + player_id + "';";
                    command.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public void UpdateWeaponItens(int player_id, int primary, int secondary, int melee, int thrown_normal, int thrown_special)
        {
            try
            {
                using (MySqlConnection mySqlConnection = SQLjec.getInstance().conn())
                {
                    MySqlCommand command = mySqlConnection.CreateCommand();
                    mySqlConnection.Open();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "UPDATE accounts SET weapon_primary='" + primary + "' WHERE player_id='" + player_id + "';";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE accounts SET weapon_secondary='" + secondary + "' WHERE player_id='" + player_id + "';";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE accounts SET weapon_melee='" + melee + "' WHERE player_id='" + player_id + "';";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE accounts SET weapon_thrown_normal='" + thrown_normal + "' WHERE player_id='" + player_id + "';";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE accounts SET weapon_thrown_special='" + thrown_special + "' WHERE player_id='" + player_id + "';";
                    command.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public void UpdateStatus(int player_id, int status)
        {
            try
            {
                using (MySqlConnection mySqlConnection = SQLjec.getInstance().conn())
                {
                    MySqlCommand command = mySqlConnection.CreateCommand();
                    mySqlConnection.Open();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "UPDATE accounts SET online='" + status + "' WHERE player_id='" + player_id + "';";
                    command.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public void UpdateClan(int player_id, int clan_id)
        {
            try
            {
                using (MySqlConnection mySqlConnection = SQLjec.getInstance().conn())
                {
                    MySqlCommand command = mySqlConnection.CreateCommand();
                    mySqlConnection.Open();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "UPDATE accounts SET clan_id='" + clan_id + "' WHERE player_id='" + player_id + "';";
                    command.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public void UpdateCharItens(int player_id, int char_red, int char_blue, int char_helmet, int char_beret, int char_dino)
        {
            try
            {
                using (MySqlConnection mySqlConnection = SQLjec.getInstance().conn())
                {
                    MySqlCommand command = mySqlConnection.CreateCommand();
                    mySqlConnection.Open();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "UPDATE accounts SET char_red='" + char_red + "' WHERE player_id='" + player_id + "';";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE accounts SET char_blue='" + char_blue + "' WHERE player_id='" + player_id + "';";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE accounts SET char_helmet='" + char_helmet + "' WHERE player_id='" + player_id + "';";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE accounts SET char_beret='" + char_beret + "' WHERE player_id='" + player_id + "';";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE accounts SET char_dino='" + char_dino + "' WHERE player_id='" + player_id + "';";
                    command.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public void UpdateTitleSlotCount(int player_id, int title_slot_count)
        {
            try
            {
                using (MySqlConnection mySqlConnection = SQLjec.getInstance().conn())
                {
                    MySqlCommand command = mySqlConnection.CreateCommand();
                    mySqlConnection.Open();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "UPDATE accounts SET title_slot_count='" + title_slot_count + "' WHERE player_id='" + player_id + "';";
                    command.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public int CreatePlayer(string account, Account p)
        {
            try
            {
                using (MySqlConnection mySqlConnection = SQLjec.getInstance().conn())
                {
                    MySqlCommand command = mySqlConnection.CreateCommand();
                    mySqlConnection.Open();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT COUNT(*) FROM accounts WHERE player_name='" + p.getPlayerName() + "'";
                    if (Convert.ToInt32(command.ExecuteScalar()) != 0)
                        return -1;
                    command.CommandText = "UPDATE accounts SET player_name='" + p.getPlayerName() + "' WHERE player_id='" + p.player_id.ToString() + "';";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE accounts SET rank='" + p.getRank().ToString() + "' WHERE player_id='" + p.player_id.ToString() + "';";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE accounts SET exp='" + p.getExp().ToString() + "' WHERE player_id='" + p.player_id.ToString() + "';";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE accounts SET gp='" + p.getGP().ToString() + "' WHERE player_id='" + p.player_id.ToString() + "';";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE accounts SET kills_count_s='" + p._statistic.getKills(true).ToString() + "' WHERE player_id='" + p.player_id.ToString() + "';";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE accounts SET deaths_count_s='" + p._statistic.getDeaths(true).ToString() + "' WHERE player_id='" + p.player_id.ToString() + "';";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE accounts SET kills_count_ns='" + p._statistic.getKills(true).ToString() + "' WHERE player_id='" + p.player_id.ToString() + "';";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE accounts SET deaths_count_ns='" + p._statistic.getDeaths(true).ToString() + "' WHERE player_id='" + p.player_id.ToString() + "';";
                    command.ExecuteNonQuery();
                    return 0;
                }
            }
            catch (MySqlException ex)
            {
                return -1;
                throw ex;
            }
        }

        public void DescontItem(int weaponId, int ownerId, int weaponCount)
        {
            try
            {
                using (MySqlConnection mySqlConnection = SQLjec.getInstance().conn())
                {
                    MySqlCommand command = mySqlConnection.CreateCommand();
                    mySqlConnection.Open();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "UPDATE items SET count='" + weaponCount + "' WHERE owner_id='" + ownerId + "' AND item_id='" + weaponId + "';";
                    command.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
    }

        public void AddInitialItems(int player_id, ItemsModel item, string name, int equip)
        {
            try
            {
                using (MySqlConnection connection = SQLjec.getInstance().conn())
                {
                    MySqlCommand cmd = connection.CreateCommand();
                    connection.Open();
                    cmd.CommandText = "INSERT INTO items (owner_id, item_id, item_name, item_type, count, loc_slot, equip) VALUES ('" + player_id + "', '" + item.id.ToString() + "', '" + name + "', '" + equip + "', '" + item.count + "', '" + item.slot + "', '" + item.equip + "');";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {

            }
        }

        public List<Account> getAccounts()
        {
            return this._accounts;
        }

        public List<Account> getOnlineAccounts()
        {
            List<Account> list = new List<Account>();
            foreach (Account account in AccountManager.getInstance().getAccounts())
            {
                if (account.getClient() != null)
                    list.Add(account);
            }
            return list;
        }

        public void AddAccount(Account acc)
        {
            this._accounts.Add(acc);
        }

        public bool CreateAccount(string login, string password)
        {
            try
            {
                using (MySqlConnection mySqlConnection = SQLjec.getInstance().conn())
                {
                    MySqlCommand command = mySqlConnection.CreateCommand();
                    mySqlConnection.Open();
                    command.CommandText = "SELECT COUNT(*) FROM accounts WHERE login='" + login + "';";
                    command.ExecuteScalar();
                    command.CommandText = "INSERT INTO accounts (login, password) VALUES ('" + login + "', '" + password + "');";
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                    command.CommandText = "SELECT * FROM accounts WHERE login='" + login + "';";
                    command.CommandType = CommandType.Text;
                    MySqlDataReader mySqlDataReader = command.ExecuteReader();
                    Account acc = new Account();
                    while (mySqlDataReader.Read())
                        acc.player_id = mySqlDataReader.GetInt32("player_id");
                    acc.name = login;
                    acc.password = password;
                    acc.setInventory(new PlayerInventory(acc.player_id));
                    this.AddAccount(acc);
                    return true;
                }
            }
            catch (Exception ex)
            {
                CLogger.getInstance().error("|[ACM]| Não foi possível ver a conta '" + login + "'");
                CLogger.getInstance().error("|[ACM]| " + ((object)ex).ToString());
                return false;
            }
        }
    }
}
