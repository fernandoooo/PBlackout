// Type: PBServer.src.network.gsPacket.clientpackets.PROTOCOL_BATTLE_TIMERSYNC_REQ
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.network;
using PBServer.src.data.xml.parsers;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;
using PBServer.src.network.gsPacket.serverpackets;
using PBServer.src.data.xml.holders;

namespace PBServer.src.network.gsPacket.clientpackets
{
  public class PROTOCOL_BATTLE_TIMERSYNC_REQ : ReceiveBaseGamePacket
  {
    private int _timeLost;
    private int unkD;
    private int unkC1;
    private int unkC2;

    public PROTOCOL_BATTLE_TIMERSYNC_REQ(GameClient gc, byte[] buff)
    {
      this.makeme(gc, buff);
    }

    protected internal override void read()
    {
      int num = (int) this.readH();
      this._timeLost = this.readD();
      this.unkD = this.readD();
      this.unkC1 = (int) this.readC();
      this.unkC2 = (int) this.readC();
    }

    protected internal override void run()
    {
      Account player = this.getClient().getPlayer();
      if (player == null)
        return;
      Room room = player.getRoom();
      room.setTimeLost(this._timeLost);
      if (this._timeLost >= 1 || player.player_id != room.getLeader().player_id)
        return;
      CLogger.getInstance().info("[GAME]: O TEMPO ACABOU!!!");
      room.setState(ROOM_STATE.ROOM_STATE_BATTLE_END);
      room.CalculateBattleResult(player);
      for (int id = 0; id < 16; ++id)
      {
        Account playerFromPlayerId = ChannelInfoHolder.getChannel(this.getClient().getChannelId()).getPlayerFromPlayerId(room.getSlot(id)._playerId);
        if (playerFromPlayerId != null)
          playerFromPlayerId.sendPacket((SendBaseGamePacket) new S_BATTLE_ENDBATTLE(playerFromPlayerId));
      }
    }
  }
}
