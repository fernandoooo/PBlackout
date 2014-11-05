using PBServer;
using PBServer.network;
using PBServer.network.Game.packets.serverpackets;
using PBServer.src.network.gsPacket.serverpackets;

namespace PBServer.network.Game.packets.clientpackets
{
  public class opcode_3906_REQ : ReceiveBaseGamePacket
  {
    public opcode_3906_REQ(GameClient gc, byte[] buff)
    {
      this.makeme(gc, buff);
    }

    protected internal override void run()
    {
      this.getClient().sendPacket((SendBaseGamePacket) new opcode_3906_ACK());
      this.getClient().sendPacket((SendBaseGamePacket) new S_BATTLE_ENDBATTLE(this.getClient().getPlayer()));
    }

    protected internal override void read()
    {
      int num = (int) this.readH();
    }
  }
}
