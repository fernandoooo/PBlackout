using PBServer;

namespace PBServer.network.Game.packets.serverpackets
{
  public class opcode_2596_ACK : SendBaseGamePacket
  {
    public opcode_2596_ACK()
    {
      this.makeme();
    }

    protected internal override void write()
    {
      this.writeH((short) 2596);
      this.writeC((byte) 0);
      this.writeB(new byte[85]);
      this.writeH((short) 1);
      this.writeC((byte) 0);
      this.writeC((byte) 13);
    }
  }
}
