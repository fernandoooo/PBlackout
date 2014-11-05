using PBServer;
using PBServer.network;
using PBServer.network.Game.packets.serverpackets;
using PBServer.src.model.accounts;

namespace PBServer.network.Game.packets.clientpackets
{
  public class PROTOCOL_BATTLE_GIVEUPBATTLE_REQ : ReceiveBaseGamePacket
  {
    private int slot = 0;

    public PROTOCOL_BATTLE_GIVEUPBATTLE_REQ(GameClient gc, byte[] buff)
    {
      this.makeme(gc, buff);
    }

    protected internal override void run()
    {
      foreach (Account account in this.getClient().getPlayer().getRoom().getAllPlayers().ToArray())
        account.sendPacket((SendBaseGamePacket) new S_BATTLE_GIVEUPBATTLE(this.slot));
    }

    protected internal override void read()
    {
      int num = (int) this.readH();
      this.slot = this.readD();
    }
  }
}
