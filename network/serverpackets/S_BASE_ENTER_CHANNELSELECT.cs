// Type: PBServer.network.serverpackets.PROTOCOL_BASE_ENTER_CHANNELSELECT_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.src.data.xml.parsers;
using PBServer.src.data.xml.holders;

namespace PBServer.network.serverpackets
{
  public class S_BASE_ENTER_CHANNELSELECT : SendBaseGamePacket
  {
    public S_BASE_ENTER_CHANNELSELECT()
    {
      this.makeme();
    }

    protected internal override void write()
    {
      this.writeH((short) 2572);
      this.writeD(10);
      this.writeD(300);
      for (int channelId = 0; channelId < 10; ++channelId)
      {
        this.writeD(ChannelInfoHolder.getChannel(channelId).getAllPlayers().Count);
      }
    }
  }
}
