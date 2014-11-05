using PBServer;
using PBServer.network;
using PBServer.network.Game.packets.serverpackets;
using System.Threading;
using PBServer.network.serverpackets;
using PBServer.src.model.accounts;
using PBServer.src.managers;

namespace PBServer.network.Game.packets.clientpackets
{
  public class opcode_3872_REQ : ReceiveBaseGamePacket
  {
    private int slot;

    public opcode_3872_REQ(GameClient gc, byte[] buff)
    {
      this.makeme(gc, buff);
    }

    protected internal override void read()
    {
        int num = (int)this.readH();
        this.slot = this.readD();
    }

    protected internal override void run()
    {
        Account player = this.getClient().getPlayer();
        try
        {
            foreach (Account account in player.getRoom().getAllPlayers())
            {
                    account.sendPacket((SendBaseGamePacket)new S_BOMB_UNISTALL(this.slot, account.getRoom())); //BOMBA DEFUSADA
                    if (account.getRoom().bomb == 1)
                    {
                        return;
                    }
                    else
                    {
                        Thread.Sleep(5);
                        account.sendPacket((SendBaseGamePacket)new S_BATTLE_END_ROUND());
                    }
            }
        }
        catch (ThreadInterruptedException e)
        {
            CLogger.getInstance().error(e.ToString());
        }
    }
  }
}
