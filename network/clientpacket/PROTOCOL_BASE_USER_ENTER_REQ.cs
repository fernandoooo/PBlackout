using PBServer;
using PBServer.network;
using PBServer.network.serverpackets;
using PBServer.src.managers;
using System;
using System.Net;

namespace PBServer.network.clientpacket
{
  internal class PROTOCOL_BASE_USER_ENTER_REQ : ReceiveBaseGamePacket
  {
    private int Account_len;
    private string account;
    private byte[] _IP;

    public PROTOCOL_BASE_USER_ENTER_REQ(GameClient Client, byte[] data)
    {
      this.makeme(Client, data);
    }

    protected internal override void read()
    {
      int num1 = (int) this.readH();
      this.Account_len = (int) this.readC();
      this.account = this.readS(this.Account_len - 1);
      this.readQ();
      int num2 = (int) this.readC();
      int num3 = (int) this.readC();
      this._IP = this.readB(4);
    }

    protected internal override void run()
    {
      if (this.getClient() == null)
        return;
      try
      {
        this.getClient().setAccount(AccountManager.getInstance().get(this.account).player_id);
        this.getClient().restoreAccount(this.account);
        AccountManager.getInstance().get(this.account).setClient(this.getClient());
        AccountManager.getInstance().get(this.account).setLocalAddress(this._IP);
        AccountManager.getInstance().get(this.account).setPublicAddress(((IPEndPoint) this.getClient()._address).Address.ToString());
        this.getClient().sendPacket((SendBaseGamePacket) new S_BASE_USER_ENTER());
      }
      catch (Exception ex)
      {
        CLogger.getInstance().warning(ex.ToString());
        this.getClient().close();
      }
      CLogger.getInstance().info("O jogador " + this.getClient().getPlayer().getPlayerName().ToString() + " entrou no jogo.");
    }
  }
}
