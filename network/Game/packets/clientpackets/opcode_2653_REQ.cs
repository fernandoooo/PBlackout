using PBServer;
using PBServer.network;

namespace PBServer.network.Game.packets.clientpackets
{
  public class opcode_2653_REQ : ReceiveBaseGamePacket
  {
    public opcode_2653_REQ(GameClient gc, byte[] buff)
    {
      this.makeme(gc, buff);
    }

    protected internal override void run()
    {
      this.getClient().close();
    }

    protected internal override void read()
    {
    }
  }
}
