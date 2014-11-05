using PBServer;
using PBServer.network;
using PBServer.network.BattleConnect;
using PBServer.network.serverpackets;
using PBServer.src.data.xml.parsers;
using PBServer.src.model;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;
using PBServer.src.data.xml.holders;

namespace PBServer.network.clientpacket
{
  internal class PROTOCOL_BATTLE_READYBATTLE_REQ : ReceiveBaseGamePacket
  {
    public PROTOCOL_BATTLE_READYBATTLE_REQ(GameClient Client, byte[] data)
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
      Channel channel = ChannelInfoHolder.getChannel(this.getClient().getChannelId());
      Room room = this.getClient().getPlayer().getRoom();
      Account player = this.getClient().getPlayer();
      Account playerFromPlayerId1 = channel.getPlayerFromPlayerId(this.getClient().getPlayer().getRoom().getLeader().player_id);
      if (room.getLeader().player_id == this.getClient().getPlayer().player_id)
      {
        player.getRoom().changeSlotState(this.getClient().getPlayer().getSlot(), SLOT_STATE.SLOT_STATE_LOAD, true);
        for (int index = 0; index < 16; ++index)
        {
          int playerId = room.getSlot(index)._playerId;
          if (playerId > 0)
          {
            Account playerFromPlayerId2 = ChannelInfoHolder.getChannel(this.getClient().getChannelId()).getPlayerFromPlayerId(playerId);
            if (playerFromPlayerId2 != null)
            {
              if (channel.getRoomInId(room.getRoomId()).getSlotState(index) == SLOT_STATE.SLOT_STATE_READY)
                channel.getRoomInId(room.getRoomId()).changeSlotState(index, SLOT_STATE.SLOT_STATE_LOAD, true);
              room.setState(ROOM_STATE.ROOM_STATE_LOADING);
              playerFromPlayerId2.CheckCorrectInventory();
              playerFromPlayerId2.sendPacket((SendBaseGamePacket) new S_BATTLE_READYBATTLE(playerFromPlayerId2));
            }
          }
        }
        if (this.getClient().getPlayer().getRoom().getLeader().player_id != this.getClient().getPlayer().player_id || this.getClient().getPlayer().getRoom().server_type <= 1)
          return;
        if (this.getClient().getPlayer().customAddress == null)
          UdpHandler.getInstance().CreateBattleUdpRoom(this.getClient().getPlayer().getRoom(), this.getClient().getPlayer().getRoom().server_type);
        else
          UdpHandler.getInstance().CreateBattleUdpRoom(this.getClient().getPlayer().getRoom(), this.getClient().getPlayer().getRoom().server_type);
      }
      else if (room.getSlotState(playerFromPlayerId1.getSlot()) == SLOT_STATE.SLOT_STATE_PRESTART || room.getSlotState(playerFromPlayerId1.getSlot()) == SLOT_STATE.SLOT_STATE_RENDEZVOUS || (room.getSlotState(playerFromPlayerId1.getSlot()) == SLOT_STATE.SLOT_STATE_LOAD || room.getSlotState(playerFromPlayerId1.getSlot()) == SLOT_STATE.SLOT_STATE_BATTLE_READY) || room.getSlotState(playerFromPlayerId1.getSlot()) == SLOT_STATE.SLOT_STATE_BATTLE)
      {
        ChannelInfoHolder.getChannel(this.getClient().getChannelId()).getPlayerFromPlayerId(room.getSlot(player.getSlot())._playerId).getRoom().changeSlotState(this.getClient().getPlayer().getSlot(), SLOT_STATE.SLOT_STATE_LOAD, true);
        player.sendPacket((SendBaseGamePacket) new S_BATTLE_READYBATTLE(player));
        UdpHandler.getInstance().AddPlayerInUdpRoom(player, room.getLeader());
      }
      else
      {
        int playerId = room.getSlot(player.getSlot())._playerId;
        int slot = this.getClient().getPlayer().getSlot();
        if (ChannelInfoHolder.getChannel(this.getClient().getChannelId()).getPlayerFromPlayerId(playerId).getRoom().getSlotState(slot) == SLOT_STATE.SLOT_STATE_NORMAL)
          ChannelInfoHolder.getChannel(this.getClient().getChannelId()).getPlayerFromPlayerId(playerId).getRoom().changeSlotState(slot, SLOT_STATE.SLOT_STATE_READY, true);
        else if (ChannelInfoHolder.getChannel(this.getClient().getChannelId()).getPlayerFromPlayerId(playerId).getRoom().getSlotState(slot) == SLOT_STATE.SLOT_STATE_READY)
          ChannelInfoHolder.getChannel(this.getClient().getChannelId()).getPlayerFromPlayerId(playerId).getRoom().changeSlotState(slot, SLOT_STATE.SLOT_STATE_NORMAL, true);
        player.sendPacket((SendBaseGamePacket) new S_BATTLE_READYBATTLE(player));
      }
    }
  }
}
