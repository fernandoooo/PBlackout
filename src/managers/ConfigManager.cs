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
    public class ConfigManager
    {
        private static ConfigManager acm = new ConfigManager();
        private int id = 1;
        public int dbstatus = 0;
        protected List<ConfigP> _accounts = new List<ConfigP>();

        static ConfigManager()
        {
        }

        public ConfigManager()
        {
            try
            {
                using (MySqlConnection mySqlConnection = SQLjec.getInstance().conn())
                {
                    MySqlCommand command = mySqlConnection.CreateCommand();
                    mySqlConnection.Open();
                    command.CommandText = "SELECT * FROM configs";
                    command.CommandType = CommandType.Text;
                    MySqlDataReader mySqlDataReader = command.ExecuteReader();
                    while (mySqlDataReader.Read())
                    {
                        ConfigP account = new ConfigP();
                        account.ownerid = mySqlDataReader.GetInt32("owner_id");
                        account.ownername = mySqlDataReader.GetString("owner_name");
                        account.mira = mySqlDataReader.GetInt32("mira");
                        account.mao = mySqlDataReader.GetInt32("mao");
                        account.sangue = mySqlDataReader.GetInt32("sangue");
                        account.audio1 = mySqlDataReader.GetInt32("audio1");
                        account.audio2 = mySqlDataReader.GetInt32("audio2");
                        account.sensibilidade = mySqlDataReader.GetInt32("sensibilidade");
                        account.visao = mySqlDataReader.GetInt32("visao");
                        account.audio_enable = mySqlDataReader.GetInt32("audio_enable");
                        this._accounts.Add(account);
                        ++this.id;
                    }
                    CLogger.getInstance().extra_info("|[CFM]| Foram encontradas " + this._accounts.Count + " configurações.");
                }
            }
            catch (Exception ex)
            {
                this.dbstatus = -100;
                CLogger.getInstance().error("|[CFM]| Nenhuma configuração foi encontrada.");
                CLogger.getInstance().error("|[CFM]| " + ((object)ex).ToString());
            }
        }

        public void AddConfigDb(int player_id, int audio1, int audio2, int sensibilidade, int visao, int sangue, int mira, string name, int mao, int audio_enable)
        {
            try
            {
                using (MySqlConnection connection = SQLjec.getInstance().conn())
                {
                    MySqlCommand cmd = connection.CreateCommand();
                    connection.Open();
                    cmd.CommandText = "INSERT INTO configs (owner_id, sangue, mira, audio1, audio2, sensibilidade, visao, owner_name, mao, audio_enable) VALUES ('" + player_id + "', '" + sangue + "', '" + mira + "', '" + audio1 + "', '" + audio2 + "', '" + sensibilidade + "', '" + visao + "', '" + name + "', '" + mao + "', '" + audio_enable + "');";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {

            }
        }

        public static ConfigManager getInstance()
        {
            return ConfigManager.acm;
        }

        public void UpdateConfig(int player_id, int audio1, int audio2, int mira, int sensibilidade, int visao, int sangue, int mao, int audio_enable)
        {
            try
            {
                using (MySqlConnection mySqlConnection = SQLjec.getInstance().conn())
                {
                    MySqlCommand command = mySqlConnection.CreateCommand();
                    mySqlConnection.Open();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "UPDATE configs SET mira='" + mira + "' WHERE owner_id='" + player_id + "';";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE configs SET audio1='" + audio1 + "' WHERE owner_id='" + player_id + "';";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE configs SET audio2='" + audio2 + "' WHERE owner_id='" + player_id + "';";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE configs SET sensibilidade='" + sensibilidade + "' WHERE owner_id='" + player_id + "';";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE configs SET visao='" + visao + "' WHERE owner_id='" + player_id + "';";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE configs SET sangue='" + sangue + "' WHERE owner_id='" + player_id + "';";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE configs SET mao='" + mao + "' WHERE owner_id='" + player_id + "';";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE configs SET audio_enable='" + audio_enable + "' WHERE owner_id='" + player_id + "';";
                    command.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public ConfigP getInfoItem(int item_id)
        {
            ConfigP result;
            foreach (ConfigP current in this._accounts)
            {
                if (current.ownerid == item_id)
                {
                    result = current;
                    return result;
                }
            }
            result = null;
            return result;
        }

        public ConfigP getInfoItem2(string name)
        {
            ConfigP result;
            foreach (ConfigP current in this._accounts)
            {
                if (current.ownername == name)
                {
                    result = current;
                    return result;
                }
            }
            result = null;
            return result;
        }

        public void AddConfig(ConfigP acc)
        {
            this._accounts.Add(acc);
        }
    }
}
