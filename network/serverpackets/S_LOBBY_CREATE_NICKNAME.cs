// Type: PBServer.network.serverpackets.PROTOCOL_LOBBY_CREATE_NICK_NAME_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;

namespace PBServer.network.serverpackets
{
  public class S_LOBBY_CREATE_NICKNAME : SendBaseGamePacket
  {
    private long _status;

    public S_LOBBY_CREATE_NICKNAME(long status)
    {
      this.makeme();
      this._status = status;
    }

    protected internal override void write()
    {
      this.writeB(new byte[2]
      {
        (byte) 16,
        (byte) 11
      });
      this.writeQ(this._status);
    }
  }
}
