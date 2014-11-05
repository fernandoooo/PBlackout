// Type: PBServer.src.managers.DaoManager
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using MySql.Data.MySqlClient;
using PBServer;
using PBServer.db;
using PBServer.model.accounts;
using PBServer.model.players;
using PBServer.src.model.accounts;
using System.Data;
using System.Data.Common;

namespace PBServer.src.managers
{
  public class DaoManager
  {
    private MySqlConnection connection;
    private MySqlCommand cmd;
    private GameClient _gc;

    public DaoManager(GameClient gc)
    {
      this._gc = gc;
      this.connection = SQLjec.getInstance().conn();
      this.cmd = this.connection.CreateCommand();
      this.connection.Open();
    }

    public void SetCookies(string cookies, string login)
    {
      this.cmd.CommandText = "INSERT INTO auth_cookies (login, cookie) VALUES ('" + login + "','" + cookies + "');";
      this.cmd.ExecuteNonQuery();
    }

    public void removeCookie(string cookie)
    {
      this.cmd.CommandText = "delete from auth_cookies where cookie='" + cookie + "';";
      this.cmd.ExecuteNonQuery();
    }

    public int clearCookies()
    {
      this.cmd.CommandText = "delete from auth_cookies;";
      int num = this.cmd.ExecuteNonQuery();
      CLogger.getInstance().info("Cleaning old cookies: " + num.ToString());
      return num;
    }

    public bool onCookie(string cookie)
    {
      this.cmd.CommandText = "SELECT cookie FROM auth_cookies WHERE cookie= '" + cookie + "';";
      this.cmd.CommandType = CommandType.Text;
      MySqlDataReader mySqlDataReader = this.cmd.ExecuteReader();
      while (mySqlDataReader.Read())
      {
        string @string = ((DbDataReader) mySqlDataReader).GetString(0);
        if (cookie == @string)
        {
          mySqlDataReader.Close();
          return true;
        }
      }
      if (!mySqlDataReader.IsClosed)
        mySqlDataReader.Close();
      return false;
    }

    public bool WebAuth(string login, string password, AccessLevel accl)
    {
        if (login != null && password != null)
        {
            MySqlDataReader MyDataReader;
            cmd.CommandText = "SELECT login, password, access_level FROM `accounts`;";
            MyDataReader = cmd.ExecuteReader();
            while (MyDataReader.Read())
            {
                int _access_level = MyDataReader.GetInt32(2);
                string _login = MyDataReader.GetString(0);
                string _password = MyDataReader.GetString(1);
                if (_login == login && _password == password)
                {
                    if (accl == AccessLevel.Admin)
                    {
                        MyDataReader.Close();
                        if (_access_level == ((int)AccessLevel.Admin))
                            return true;
                        else
                            return false;
                    }
                    else
                    {
                        MyDataReader.Close();
                        return true;
                    }
                }

            }
            if (!MyDataReader.IsClosed)
                MyDataReader.Close();
        }


        return false;

    }

    public Account getPlayerInfo(int player_id)
    {
      this.cmd.CommandText = "SELECT * FROM accounts WHERE player_id='" + player_id + "' LIMIT 1;";
      this.cmd.CommandType = CommandType.Text;
      Account account1 = new Account();
      MySqlDataReader mySqlDataReader = this.cmd.ExecuteReader();
      while (mySqlDataReader.Read())
      {
        account1.setPlayerId(((DbDataReader) mySqlDataReader).GetInt32(1));
        account1.setPlayerName(((DbDataReader) mySqlDataReader).GetString(2));
        account1.setRank(((DbDataReader) mySqlDataReader).GetInt32(5));
        account1.setGP(((DbDataReader) mySqlDataReader).GetInt32(6));
        account1.setMoney(((DbDataReader) mySqlDataReader).GetInt32(32));
        account1.setExp(((DbDataReader) mySqlDataReader).GetInt32(7));
      }
      mySqlDataReader.Close();
      this.connection.Close();
      return account1;
    }

    public PlayerInventory getInventory(int player_id)
    {
      this.cmd.CommandText = "SELECT * FROM items WHERE owner_id='" + (object) player_id + "';";
      this.cmd.CommandType = CommandType.Text;
      PlayerInventory playerInventory = new PlayerInventory(player_id);
      MySqlDataReader mySqlDataReader = this.cmd.ExecuteReader();
      while (mySqlDataReader.Read())
      {
        ItemsModel itemsModel = new ItemsModel();
        itemsModel.id = ((DbDataReader) mySqlDataReader).GetInt32(2);
        itemsModel.count = ((DbDataReader)mySqlDataReader).GetInt32(5);
        itemsModel.slot = ((DbDataReader) mySqlDataReader).GetInt32(6);
        itemsModel.equip_type = ((DbDataReader)mySqlDataReader).GetInt32(7);
        int int32 = ((DbDataReader) mySqlDataReader).GetInt32(4);
        playerInventory.getItemsAll().Add(itemsModel);
          if (int32 == 1)
        {
            if (itemsModel.slot == 1)
                playerInventory.getEquipAll().PRIM = itemsModel.id;
            else if (itemsModel.slot == 2)
                playerInventory.getEquipAll().SUB = itemsModel.id;
            else if (itemsModel.slot == 3)
                playerInventory.getEquipAll().MELEE = itemsModel.id;
            else if (itemsModel.slot == 4)
                playerInventory.getEquipAll().ITEM = itemsModel.id;
            else if (itemsModel.slot == 5)
                playerInventory.getEquipAll().THROWING = itemsModel.id;
            else if (itemsModel.slot == 6)
                playerInventory.getEquipAll().CHAR_RED = itemsModel.id;
            else if (itemsModel.slot == 7)
                playerInventory.getEquipAll().CHAR_BLUE = itemsModel.id;
            else if (itemsModel.slot == 8)
                playerInventory.getEquipAll().CHAR_HEAD = itemsModel.id;
            else if (itemsModel.slot == 9)
                playerInventory.getEquipAll().CHAR_DINO = itemsModel.id;
            else if (itemsModel.slot == 10)
                playerInventory.getEquipAll().CHAR_ITEM = itemsModel.id;
            else if (itemsModel.slot == 11)
                playerInventory.getEquipAll().CUPON = itemsModel.id;
        }
      }
      return playerInventory;
    }
  }
}
