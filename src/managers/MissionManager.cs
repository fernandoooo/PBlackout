using MySql.Data.MySqlClient;
using PBServer;
using PBServer.db;
using PBServer.model.players;
using PBServer.src.model.accounts;
using System;
using System.Collections.Generic;
using System.Data;
using PBServer.src.model;

namespace PBServer.src.managers
{
    public class MissionManager
    {
        private static MissionManager acm = new MissionManager();
        private int id = 1;
        public int dbstatus = 0;
        protected List<Mission> _accounts = new List<Mission>();

        static MissionManager()
        {
        }

        public MissionManager()
        {
            try
            {
                using (MySqlConnection mySqlConnection = SQLjec.getInstance().conn())
                {
                    MySqlCommand command = mySqlConnection.CreateCommand();
                    mySqlConnection.Open();
                    command.CommandText = "SELECT * FROM missions";
                    command.CommandType = CommandType.Text;
                    MySqlDataReader mySqlDataReader = command.ExecuteReader();
                    while (mySqlDataReader.Read())
                    {
                        Mission account = new Mission();
                        account.owner_id = mySqlDataReader.GetInt32("owner_id");
                        account.cards_tutorial[1] = mySqlDataReader.GetInt32("mission_1");
                        account.cards_tutorial[2] = mySqlDataReader.GetInt32("mission_2");
                        account.cards_tutorial[3] = mySqlDataReader.GetInt32("mission_3");
                        account.cards_tutorial[4] = mySqlDataReader.GetInt32("mission_4");
                        account.cards_tutorial[5] = mySqlDataReader.GetInt32("mission_5");
                        account.cards_tutorial[6] = mySqlDataReader.GetInt32("mission_6");
                        account.cards_tutorial[7] = mySqlDataReader.GetInt32("mission_7");
                        account.cards_tutorial[8] = mySqlDataReader.GetInt32("mission_8");
                        account.cards_tutorial[9] = mySqlDataReader.GetInt32("mission_9");
                        account.cards_tutorial[10] = mySqlDataReader.GetInt32("mission_10");
                        this._accounts.Add(account);
                        ++this.id;
                    }
                    CLogger.getInstance().extra_info("|[MM]| Foram carregadas " + (object)this._accounts.Count + " missão(sões).");
                }
            }
            catch (Exception ex)
            {
                this.dbstatus = -100;
                CLogger.getInstance().error("|[MM]| Nenhuma conta foi encontrada");
                CLogger.getInstance().error("|[MM]| " + ((object)ex).ToString());
            }
        }

        public static MissionManager getInstance()
        {
            return MissionManager.acm;
        }

        public void AddAccount(Mission acc)
        {
            this._accounts.Add(acc);
        }

        public Mission getAccountInName(int player_id)
        {
            Mission result;
            foreach (Mission current in MissionManager.getInstance().getAccounts())
            {
                if (current.owner_id == player_id)
                {
                    result = current;
                    return result;
                }
            }
            result = null;
            return result;
        }

        public List<Mission> getAccounts()
        {
            return this._accounts;
        }
    }
}
