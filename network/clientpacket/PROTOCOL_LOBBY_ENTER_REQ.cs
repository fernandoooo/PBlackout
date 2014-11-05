using PBServer;
using PBServer.network;
using PBServer.network.serverpackets;
using PBServer.src.data.xml.parsers;
using PBServer.src.model.accounts;
using PBServer.src.data.xml.holders;

namespace PBServer.network.clientpacket
{
  internal class PROTOCOL_LOBBY_ENTER_REQ : ReceiveBaseGamePacket
  {
    public PROTOCOL_LOBBY_ENTER_REQ(GameClient Client, byte[] data)
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
      if (this.getClient().getPlayer() != null)
      {
        Account playerFromPlayerId = ChannelInfoHolder.getChannel(this.getClient().getChannelId()).getPlayerFromPlayerId(this.getClient().getPlayer().player_id);
        if (playerFromPlayerId != null && playerFromPlayerId.getRoom() != null)
          ChannelInfoHolder.getChannel(this.getClient().getChannelId()).getRoomInId(playerFromPlayerId.getRoom().getRoomId()).removePlayer(playerFromPlayerId);
        if (this.getClient().getPlayer() != null)
        {
          Account player = this.getClient().getPlayer();
          player.setClient(this.getClient());
          player.setOnlineStatus(true);
          int channelId = this.getClient().getChannelId();
          if (channelId > -1)
            ChannelInfoHolder.getChannel(channelId).addPlayer(player);
        }
      }
      this.getClient().sendPacket((SendBaseGamePacket) new S_LOBBY_ENTER());
    }
  }
}
