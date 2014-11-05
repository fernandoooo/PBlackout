using MySql.Data.MySqlClient;
using PBServer;
using PBServer.db;
using PBServer.model.players;
using PBServer.src.model.accounts;
using PBServer.src.model.friends;
using System;
using System.Collections.Generic;
using System.Data;

namespace PBServer.src.managers
{
    public class FriendManager
    {
        public List<Friends> _clans = new List<Friends>();
        private int id = 1;
        public int dbstatus = 0;
        private static FriendManager clm = new FriendManager();

        static FriendManager()
        {
        }

    public FriendManager()
    {
      try
      {
        using (MySqlConnection mySqlConnection = SQLjec.getInstance().conn())
        {
          MySqlCommand command = mySqlConnection.CreateCommand();
          mySqlConnection.Open();
          command.CommandText = "SELECT * FROM friends";
          command.CommandType = CommandType.Text;
          MySqlDataReader mySqlDataReader = command.ExecuteReader();
          while (mySqlDataReader.Read())
          {
            this._clans.Add(new Friends()
            {
              friend_id = mySqlDataReader.GetInt32("friend_id"),
              owner_id = mySqlDataReader.GetInt32("owner_id")
            });
            ++this.id;
          }
          CLogger.getInstance().extra_info("|[FIM]| Foram carregados " + (object)this._clans.Count + " amigos.");
        }
      }
      catch (Exception ex)
      {
        this.dbstatus = -100;
        CLogger.getInstance().error("|[CM]| Nenhum clan foi encontrado.");
        CLogger.getInstance().error("|[CM]| " + ex.ToString());
      }
    }

    public List<Friends> getFriends(int owner_id)
    {
        List<Friends> friends = new List<Friends>();
        try
        {
            using (MySqlConnection mySqlConnection = SQLjec.getInstance().conn())
            {
                MySqlCommand command = mySqlConnection.CreateCommand();
                mySqlConnection.Open();
                command.CommandText = "SELECT * FROM friends WHERE owner_id='" + owner_id + "'";
                command.CommandType = CommandType.Text;
                MySqlDataReader mySqlDataReader = command.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    friends.Add(new Friends()
                    {
                        friend_id = mySqlDataReader.GetInt32("friend_id"),
                        owner_id = mySqlDataReader.GetInt32("owner_id")
                    });
                    ++this.id;
                }
                CLogger.getInstance().extra_info("|[FIM]| Foram encontrados " + (object)friends.Count + " amigos.");
            }
        }
        catch (Exception ex)
        {
            this.dbstatus = -100;
            CLogger.getInstance().error("|[FIM]| Nenhum amigo foi encontrado.");
            CLogger.getInstance().error("|[FIM]| " + ex.ToString());
        }
        return friends;
    }

    public void UpdateFriendsCount(int player_id, int count_friends)
    {
        try
        {
            using (MySqlConnection mySqlConnection = SQLjec.getInstance().conn())
            {
                MySqlCommand command = mySqlConnection.CreateCommand();
                mySqlConnection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE accounts SET count_friend='" + count_friends + "' WHERE player_id='" + player_id + "';";
                command.ExecuteNonQuery();
            }
        }
        catch (MySqlException ex)
        {
            throw ex;
        }
    }

        public static FriendManager getInstance()
        {
            return FriendManager.clm;
        }

        public Friends get(int object_id)
        {
            Friends result;
            foreach (Friends current in this._clans)
            {
                if (current.owner_id == object_id)
                {
                    result = current;
                    return result;
                }
            }
            result = null;
            return result;
        }

        public List<Friends> getAccounts()
        {
            return this._clans;
        }

        public void AddFriend(int friend_id, int owner_id)
        {
            using (MySqlConnection mySqlConnection = SQLjec.getInstance().conn())
            {
                mySqlConnection.Open();
                MySqlCommand mySqlCommand = new MySqlCommand(string.Concat(new object[]
				{
					"INSERT INTO friends(friend_id,owner_id)VALUES('" + friend_id + "','" + owner_id + "')"
				}), mySqlConnection);
                mySqlCommand.ExecuteNonQuery();
            }
        }

        public int getFriendForOwnerId(int owner_id)
        {
            using (MySqlConnection mySqlConnection = SQLjec.getInstance().conn())
            {
                MySqlCommand command = mySqlConnection.CreateCommand();
                mySqlConnection.Open();
                command.CommandText = "SELECT friend_id FROM friends WHERE owner_id='" + owner_id + "'';";
                command.CommandType = CommandType.Text;
                MySqlDataReader mySqlDataReader = command.ExecuteReader();
                if (mySqlDataReader.Read())
                    return mySqlDataReader.GetInt32("friend_id");
            }
            return 0;
        }
    }
}
