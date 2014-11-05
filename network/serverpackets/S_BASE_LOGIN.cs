// Type: PBServer.network.serverpackets.PROTOCOL_BASE_LOGIN_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;

namespace PBServer.network.serverpackets
{
  internal class S_BASE_LOGIN : SendBaseLoginPacket
  {
    private static byte[] p = new byte[15]
    {
      (byte) 4,
      (byte) 160,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 224,
      (byte) 59,
      (byte) 6,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0
    };
    protected long _result;

    static S_BASE_LOGIN()
    {
    }

    public S_BASE_LOGIN(long result)
    {
      this.makeme();
      this._result = result;
    }

    protected internal override void write()
    {
      this.writeH((short) 2564);
      this.writeQ(this._result);
      this.writeB(new byte[4]
      {
        (byte) 189,
        (byte) 197,
        (byte) 19,
        (byte) 0
      });
      this.writeQ(0L);
    }
  }
}
