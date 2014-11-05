using PBServer;
using PBServer.network;
using PBServer.network.serverpackets;
using PBServer.src.data.xml.parsers;
using PBServer.src.model;
using PBServer.src.model.accounts;
using PBServer.src.data.xml.holders;

namespace PBServer.network.clientpacket
{
  internal class PROTOCOL_LOBBY_LEAVE_REQ : ReceiveBaseGamePacket
  {
    public PROTOCOL_LOBBY_LEAVE_REQ(GameClient Client, byte[] data)
    {
      this.makeme(Client, data);
    }

    protected internal override void read()
    {
    }

    protected internal override void run()
    {
      if (this.getClient() == null)
        return;
      Account player = this.getClient().getPlayer();
      Channel channel = ChannelInfoHolder.getChannel(this.getClient().getChannelId());
      if (channel != null && player != null && channel.getAllPlayers().Contains(player.player_id))
        channel.removePlayer(player);
      this.getClient().sendPacket((SendBaseGamePacket) new S_LOBBY_LEAVE());
      CLogger.getInstance().info("O jogador " + this.getClient().getPlayer().getPlayerName().ToString() + " saiu do lobby.");
    }
  }
}
