using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace PBServer.db
{
  public class SQL_Block
  {
    private List<string[]> values = new List<string[]>();
    private List<string[]> values2 = new List<string[]>();
    private string _table;
    private MySqlConnection connection;
    private MySqlCommand cmd;

    public SQL_Block(string table)
    {
      this._table = table;
    }

    public void param(string p, object v)
    {
      this.values.Add(new string[2]
      {
        p,
        v.ToString()
      });
    }

    public void where(string p, object v)
    {
      this.values2.Add(new string[2]
      {
        p,
        v.ToString()
      });
    }

    public void on()
    {
      this.connection = SQLjec.getInstance().conn();
      this.cmd = this.connection.CreateCommand();
      this.connection.Open();
    }

    public void off()
    {
      this.connection.Close();
    }

    public void sql_insert(bool ex)
    {
      if (!ex)
      {
        this.connection = SQLjec.getInstance().conn();
        this.cmd = this.connection.CreateCommand();
        this.connection.Open();
      }
      string str1 = "";
      string str2 = "";
      short num = (short) 0;
      foreach (string[] strArray in this.values)
      {
        str1 = str1 + strArray[0];
        str2 = str2 + "'" + strArray[1] + "'";
        ++num;
        if ((int) num < this.values.Count)
        {
          str1 = str1 + ",";
          str2 = str2 + ",";
        }
      }
      this.cmd.CommandText = "insert into " + this._table + " (" + str1 + ") values (" + str2 + ")";
      this.cmd.CommandType = CommandType.Text;
      this.cmd.ExecuteNonQuery();
      if (!ex)
      {
        this.connection.Close();
      }
      else
      {
        this.values.Clear();
        this.values2.Clear();
      }
    }

    public void sql_delete(bool ex)
    {
      if (!ex)
      {
        this.connection = SQLjec.getInstance().conn();
        this.cmd = this.connection.CreateCommand();
        this.connection.Open();
      }
      string str = "";
      short num = (short) 0;
      foreach (string[] strArray in this.values2)
      {
        str = str + strArray[0] + "='" + strArray[1] + "'";
        ++num;
        if ((int) num < this.values2.Count)
          str = str + " and ";
      }
      this.cmd.CommandText = "delete from " + this._table + " where " + str;
      this.cmd.CommandType = CommandType.Text;
      this.cmd.ExecuteNonQuery();
      if (!ex)
      {
        this.connection.Close();
      }
      else
      {
        this.values.Clear();
        this.values2.Clear();
      }
    }

    public void sql_update(bool ex)
    {
      if (!ex)
      {
        this.connection = SQLjec.getInstance().conn();
        this.cmd = this.connection.CreateCommand();
        this.connection.Open();
      }
      string str1 = "";
      string str2 = "";
      byte num1 = (byte) 0;
      foreach (string[] strArray in this.values)
      {
        str1 = str1 + strArray[0] + "='" + strArray[1] + "'";
        ++num1;
        if ((int) num1 < this.values.Count)
          str1 = str1 + ",";
      }
      byte num2 = (byte) 0;
      foreach (string[] strArray in this.values2)
      {
        str2 = str2 + strArray[0] + "='" + strArray[1] + "'";
        ++num2;
        if ((int) num2 < this.values2.Count)
          str2 = str2 + " and ";
      }
      this.cmd.CommandText = "update " + this._table + " set " + str1 + " where " + str2;
      this.cmd.CommandType = CommandType.Text;
      this.cmd.ExecuteNonQuery();
      if (!ex)
      {
        this.connection.Close();
      }
      else
      {
        this.values.Clear();
        this.values2.Clear();
      }
    }
  }
}
