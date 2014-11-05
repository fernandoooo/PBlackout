// Type: PBServer.src.network.gsPacket.clientpackets.PROTOCOL_BATTLE_PRESTARTBATTLE_REQ
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.network;
using PBServer.network.Game.packets.serverpackets;
using PBServer.src.data.xml.parsers;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;
using PBServer.src.network.gsPacket.serverpackets;
using System.Threading;
using PBServer.src.data.xml.holders;

namespace PBServer.src.network.gsPacket.clientpackets
{
  internal class PROTOCOL_BATTLE_PRESTARTBATTLE_REQ : ReceiveBaseGamePacket
  {
    public PROTOCOL_BATTLE_PRESTARTBATTLE_REQ(GameClient Client, byte[] data)
    {
      this.makeme(Client, data);
    }

    protected internal override void read()
    {
    }

    protected internal override void run()
    {
      Account player = this.getClient().getPlayer();
      Thread.Sleep(2000);
      if (this.getClient() == null)
        return;
      player.getRoom().setState(ROOM_STATE.ROOM_STATE_PRE_BATTLE);
      player.getRoom().changeSlotState(this.getClient().getPlayer().getSlot(), SLOT_STATE.SLOT_STATE_PRESTART, true);
      player.sendPacket((SendBaseGamePacket) new S_BATTLE_ROOMINFO(player.getRoom()));
      if (player.getRoom().server_type == 1)
      {
        foreach (Account p in ChannelInfoHolder.getChannel(this.getClient().getChannelId()).getRoomInId(player.getRoom().getRoomId()).getAllPlayers().ToArray())
        {
          if (p.getRoom().getSlot(p.getSlot()).state == SLOT_STATE.SLOT_STATE_PRESTART)
          {
            CLogger.getInstance().info("Send: PROTOCOL_BATTLE_PRESTARTBATTLE_ACK info_player=" + p.getPlayerName() + " send player " + player.player_name);
            player.sendPacket((SendBaseGamePacket) new S_BATTLE_PRESTARTBATTLE(p));
          }
        }
        if (player.player_id == player.getRoom().getLeader().player_id)
          return;
        CLogger.getInstance().info("Send: PROTOCOL_BATTLE_PRESTARTBATTLE_ACK info_player=" + player.getPlayerName() + " send player " + player.getRoom().getLeader().player_name);
        player.getRoom().getLeader().sendPacket((SendBaseGamePacket) new S_BATTLE_PRESTARTBATTLE(player));
      }
      else if (player.player_id == player.getRoom().getLeader().player_id)
      {
        CLogger.getInstance().info("Send: PROTOCOL_BATTLE_PRESTARTBATTLE_ACK info_player=" + player.getPlayerName() + " send player " + player.player_name);
        player.sendPacket((SendBaseGamePacket) new S_BATTLE_PRESTARTBATTLE(player));
        foreach (Account p in ChannelInfoHolder.getChannel(this.getClient().getChannelId()).getRoomInId(player.getRoom().getRoomId()).getAllPlayers().ToArray())
        {
          if (p.player_id != player.player_id && p.getRoom().getSlot(p.getSlot()).state == SLOT_STATE.SLOT_STATE_PRESTART)
          {
            CLogger.getInstance().info("Send: PROTOCOL_BATTLE_PRESTARTBATTLE_ACK info_player=" + p.getPlayerName() + " send player " + player.player_name);
            player.sendPacket((SendBaseGamePacket) new S_BATTLE_PRESTARTBATTLE(p));
          }
        }
      }
      else
      {
        CLogger.getInstance().info("Send: PROTOCOL_BATTLE_PRESTARTBATTLE_ACK info_player=" + player.getRoom().getLeader().player_name + " send player " + player.player_name);
        player.sendPacket((SendBaseGamePacket) new S_BATTLE_PRESTARTBATTLE(player));
      }
    }
  }
}
