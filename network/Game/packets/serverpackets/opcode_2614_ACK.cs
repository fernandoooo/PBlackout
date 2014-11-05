using PBServer;

namespace PBServer.network.Game.packets.serverpackets
{
  public class opcode_2614_ACK : SendBaseGamePacket
  {
    private int _rank;
    private int _itemid;

    public opcode_2614_ACK(int rank, int itemid)
    {
      this.makeme();
      this._rank = rank;
      this._itemid = itemid;
    }

    protected internal override void write()
    {
      this.writeH((short) 2614);
      this.writeD(this._rank);
      this.writeD(1);
      this.writeD(this._itemid);
    }
  }
}
