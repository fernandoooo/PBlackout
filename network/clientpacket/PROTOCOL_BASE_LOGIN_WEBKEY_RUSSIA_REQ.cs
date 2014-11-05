using PBServer;
using PBServer.network;
using PBServer.network.serverpackets;
using PBServer.src.managers;
using PBServer.src.model.accounts;
using System;

namespace PBServer.network.clientpacket
{
  internal class PROTOCOL_BASE_LOGIN_WEBKEY_RUSSIA_REQ : ReceiveBaseLoginPacket
  {
    private string _login;
    private string _passwd;
    private int _GAME_VER;
    private int _UNK_C2;
    private int _UNK_C3;
    private int _UNK_C4;

    public byte[] _MAC { get; set; }

    public short _UNKH { get; set; }

    public PROTOCOL_BASE_LOGIN_WEBKEY_RUSSIA_REQ(LoginClient Client, byte[] data)
    {
      this.makeme(Client, data);
    }

    protected internal override void read()
    {
      int num = (int) this.readH();
      this._GAME_VER = (int)this.readC();
      this.readB(5);
      this._UNK_C2 = (int) this.readC();
      this._UNK_C3 = (int) this.readC();
      this._login = this.readS(this._UNK_C2);
      this._passwd = this.readS(this._UNK_C3);
      this._MAC = this.readB(6);
      this._UNKH = this.readH();
      this._UNK_C4 = (int) this.readC();
    }

    protected internal override void run()
    {
      CLogger.getInstance().info("Login: " + this._login);
      CLogger.getInstance().info("Password: " + this._passwd);
      AccountManager instance = AccountManager.getInstance();
      if (!instance.accountExists(this._login) && Config.AUTO_ACCOUNTS && (this._login.Length > 4 && this._passwd.Length > 4) && !instance.CreateAccount(this._login, this._passwd))
      {
          this.getClient().sendPacket((SendBaseLoginPacket)new S_BASE_LOGIN(2147483906L));
          this.getClient().close();
      }
      else
      {
          Account account = instance.get(this._login) ?? instance.SearchAccountInDB(this._login, this._passwd);
          if (account == null || !account.validatePassword(this._passwd))
          {
              this.getClient().sendPacket((SendBaseLoginPacket)new S_BASE_LOGIN(2147483906L));
              this.getClient().close();
          }
          else if (Config.onlyGM)
          {
              if (account.access_level > 0)
              {
                  if (!instance.get(this._login).getOnline())
                  {
                      this.getClient().setLogin(this._login);
                      this.getClient().sendPacket((SendBaseLoginPacket)new S_BASE_LOGIN(1L));
                      instance.get(this._login).setOnlineStatus(true);
                  }
                  else
                  {
                      instance.get(this._login).setConnected(false);
                      instance.get(this._login).setOnlineStatus(false);
                      this.getClient().sendPacket((SendBaseLoginPacket)new S_BASE_LOGIN(2147483905L));
                  }
              }
              else
              {
                  this.getClient().sendPacket((SendBaseLoginPacket)new S_BASE_LOGIN(2147483918L));
                  this.getClient().close();
              }
          }
          else if (account.access_level >= 0)
          {
              if (!instance.get(this._login).getOnline())
              {
                  this.getClient().setLogin(this._login);
                  this.getClient().sendPacket((SendBaseLoginPacket)new S_BASE_LOGIN(1L));
                  instance.get(this._login).setOnlineStatus(true);
              }
              else
              {
                  instance.get(this._login).setConnected(false);
                  instance.get(this._login).setOnlineStatus(false);
                  this.getClient().sendPacket((SendBaseLoginPacket)new S_BASE_LOGIN(2147483905L));
              }
          }
          else if (account.access_level < 0)
          {
              this.getClient().sendPacket((SendBaseLoginPacket)new S_BASE_LOGIN(2147483911L));
              this.getClient().close();
          }
      }
    }
  }
}
