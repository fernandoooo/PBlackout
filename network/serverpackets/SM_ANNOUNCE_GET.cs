// Type: PBServer.network.serverpackets.PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.src.managers;
using PBServer.src.data.xml.parsers;
using PBServer.src.model;
using PBServer.src.data.xml.holders;

namespace PBServer.network.serverpackets
{
  internal class SM_ANNOUNCE_GET : SendBaseGamePacket
  {
    private Channel _channel;
    public SM_ANNOUNCE_GET(int channelId)
    {
      this.makeme();
      _channel = ChannelInfoHolder.getChannel(channelId);
    }

    protected internal override void write()
    {
      this.writeH((short) 2574);
      this.writeD(1);
      this.writeH((short) this._channel.getAnnounceSize());
      this.writeS(this._channel.getAnnounce(), this._channel.getAnnounceSize());
      this.writeD(0);
    }
  }
}
