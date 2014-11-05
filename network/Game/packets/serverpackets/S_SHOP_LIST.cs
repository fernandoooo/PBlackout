using PBServer;
using System;

namespace PBServer.network.Game.packets.serverpackets
{
  public class S_SHOP_LIST : SendBaseGamePacket
  {
    public S_SHOP_LIST()
    {
        base.makeme();
    }

    protected internal override void write()
    {
        base.writeH(0xB06);
        base.writeD(Convert.ToInt32(DateTime.Now.ToString("yyMMddHHmm")));
    }
  }
}
