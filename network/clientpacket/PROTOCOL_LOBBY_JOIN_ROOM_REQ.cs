using PBServer;
using PBServer.network;
using PBServer.network.Game.packets.serverpackets;
using PBServer.network.serverpackets;
using PBServer.src.data.xml.parsers;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;
using PBServer.network.BattleConnect;
using PBServer.src.data.xml.holders;

namespace PBServer.network.clientpacket
{
  internal class PROTOCOL_LOBBY_JOIN_ROOM_REQ : ReceiveBaseGamePacket
  {
    private int roomID;

    public PROTOCOL_LOBBY_JOIN_ROOM_REQ(GameClient Client, byte[] data)
    {
      this.makeme(Client, data);
    }

    protected internal override void read()
    {
      int num = (int) this.readH();
      this.roomID = this.readD();
    }

    protected internal override void run()
    {
      Account player = this.getClient().getPlayer();
      if (player != null)
      {
        Room roomInId = ChannelInfoHolder.getChannel(this.getClient().getChannelId()).getRoomInId(this.roomID);
        if (roomInId != null)
        {
          int slotId = roomInId.addPlayer(player);
          CLogger.getInstance().info("O jogador " + player.player_name + " entrou em uma sala.");
          player.setSlot(slotId);
          player.setRoom(roomInId);
          for (int id = 0; id < 15; ++id)
          {
            int playerId = roomInId.getSlot(id)._playerId;
            if (playerId > 0)
            {
              Account playerFromPlayerId = ChannelInfoHolder.getChannel(this.getClient().getChannelId()).getPlayerFromPlayerId(playerId);
              if (playerFromPlayerId != null && playerFromPlayerId.player_id != this.getClient().getPlayer().player_id)
                playerFromPlayerId.sendPacket((SendBaseGamePacket) new S_ROOM_PLAYER_ENTER(player));
            }
          }
          player.sendPacket((SendBaseGamePacket) new S_LOBBY_JOIN_ROOM((long) this.roomID, player));
        }
        else
          player.sendPacket((SendBaseGamePacket) new S_LOBBY_JOIN_ROOM(2147487748L, (Account) null));
      }
      else
        player.sendPacket((SendBaseGamePacket) new S_LOBBY_JOIN_ROOM(2147487748L, (Account) null));
    }
  }
}
