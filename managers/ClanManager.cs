using MySql.Data.MySqlClient;
using PBServer;
using PBServer.db;
using PBServer.model.clans;
using System;
using System.Collections.Generic;
using System.Data;
using PBServer.model.players;
using PBServer.src.model.accounts;

namespace PBServer.managers
{
    public class ClanManager
    {
        public List<Clan> _clans = new List<Clan>();
        private int id = 1;
        public int dbstatus = 0;
        private static ClanManager clm = new ClanManager();

        static ClanManager()
        {
        }

        public ClanManager()
        {
            try
            {
                using (MySqlConnection mySqlConnection = SQLjec.getInstance().conn())
                {
                    MySqlCommand command = mySqlConnection.CreateCommand();
                    mySqlConnection.Open();
                    command.CommandText = "SELECT * FROM clan_data";
                    command.CommandType = CommandType.Text;
                    MySqlDataReader mySqlDataReader = command.ExecuteReader();
                    while (mySqlDataReader.Read())
                    {
                        this._clans.Add(new Clan()
                        {
                            clan_id = mySqlDataReader.GetInt32("clan_id"),
                            clan_name = mySqlDataReader.GetString("clan_name"),
                            clan_rank = mySqlDataReader.GetInt32("clan_rank"),
                            owner_id = mySqlDataReader.GetInt32("owner_id"),
                            clan_info = mySqlDataReader.GetString("clan_info"),
                            dateCreated = mySqlDataReader.GetInt32("create_date"),
                            _logo1 = mySqlDataReader.GetInt32("logo1"),
                            _logo2 = mySqlDataReader.GetInt32("logo2"),
                            _logo3 = mySqlDataReader.GetInt32("logo3"),
                            _logo4 = mySqlDataReader.GetInt32("logo4"),
                            _color = mySqlDataReader.GetInt32("color")
                        });
                        ++this.id;
                    }
                    CLogger.getInstance().extra_info("|[CM]| Foram carregados " + (object)this._clans.Count + " clans.");
                }
            }
            catch (Exception ex)
            {
                this.dbstatus = -100;
                CLogger.getInstance().error("|[CM]| Nenhum clan foi encontrado.");
                CLogger.getInstance().error("|[CM]| " + ex.ToString());
            }
        }

        public static ClanManager getInstance()
        {
            return ClanManager.clm;
        }

        public List<Clan> getClans()
        {
            return this._clans;
        }

        /*public Clan get(int clan_id)
        {
          foreach (Clan clan in this._clans)
          {
            if (clan.clan_id == clan_id)
              return clan;
          }
          return (Clan) null;
        }*/

        /*public Clan get(int username)
        {
            foreach (Clan account in ClanManager.getInstance().getClans())
            {
                if (account.clan_id.ToString() == username.ToString())
                    return account;
            }
            return (Clan)null;
        }*/

        public Clan get(int object_id)
        {
            Clan result;
            foreach (Clan current in this._clans)
            {
                if (current.clan_id == object_id)
                {
                    result = current;
                    return result;
                }
            }
            result = null;
            return result;
        }

        public Clan getClanForName(string name)
        {
            Clan result;
            foreach (Clan current in this._clans)
            {
                if (current.clan_name == name)
                {
                    result = current;
                    return result;
                }
            }
            result = null;
            return result;
        }

        public void createClanInDb(string name, int ownerId)
        {
            using (MySqlConnection mySqlConnection = SQLjec.getInstance().conn())
            {
                mySqlConnection.Open();
                MySqlCommand mySqlCommand = new MySqlCommand(string.Concat(new object[]
				{
					"INSERT INTO clan_data(clan_id,clan_name,owner_id,create_date)VALUES('" + ownerId + "','" + name + "','" + ownerId + "','" + DateTime.Now.ToString("yyyyMMdd") + "')"
				}), mySqlConnection);
                mySqlCommand.ExecuteNonQuery();
            }
        }

        public void excludeClanInDb(int id)
        {
            using (MySqlConnection mySqlConnection = SQLjec.getInstance().conn())
            {
                mySqlConnection.Open();
                MySqlCommand mySqlCommand = new MySqlCommand(string.Concat(new object[]
				{
					"DELETE FROM clan_data WHERE clan_id='" + id + "'"
				}), mySqlConnection);
                mySqlCommand.ExecuteNonQuery();
            }
        }

        public void UpdateClanInfo(int clan_id, string clan_info)
        {
            try
            {
                using (MySqlConnection mySqlConnection = SQLjec.getInstance().conn())
                {
                    MySqlCommand command = mySqlConnection.CreateCommand();
                    mySqlConnection.Open();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "UPDATE clan_data SET clan_info='" + clan_info + "' WHERE clan_id='" + clan_id + "';";
                    command.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public List<Account> getClanPlayers(int clan_id)
        {
            List<Account> friends = new List<Account>();
            try
            {
                using (MySqlConnection mySqlConnection = SQLjec.getInstance().conn())
                {
                    MySqlCommand command = mySqlConnection.CreateCommand();
                    mySqlConnection.Open();
                    command.CommandText = "SELECT * FROM accounts WHERE clan_id='" + clan_id + "'";
                    command.CommandType = CommandType.Text;
                    MySqlDataReader mySqlDataReader = command.ExecuteReader();
                    while (mySqlDataReader.Read())
                    {
                        friends.Add(new Account()
                        {
                            player_name = mySqlDataReader.GetString("player_name"),
                            _rank = mySqlDataReader.GetInt32("rank")
                        });
                        ++this.id;
                    }
                    CLogger.getInstance().extra_info("|[CLA]| Foram encontrados " + (object)friends.Count + " jogadores do cla.");
                }
            }
            catch (Exception ex)
            {
                this.dbstatus = -100;
                CLogger.getInstance().error("|[CLA]| Nenhum amigo foi encontrado.");
                CLogger.getInstance().error("|[CLA]| " + ex.ToString());
            }
            return friends;
        }

        public void AddClan(Clan clan)
        {
            this._clans.Add(clan);
        }
    }
}
