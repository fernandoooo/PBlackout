using PBServer;
using PBServer.network;
using PBServer.network.BattleConnect;
using PBServer.network.Game.packets.serverpackets;
using PBServer.src.data.xml.parsers;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;
using PBServer.src.data.xml.holders;

namespace PBServer.network.Game.packets.clientpackets
{
  public class opcode_3890_REQ : ReceiveBaseGamePacket
  {
    public opcode_3890_REQ(GameClient gc, byte[] buff)
    {
      this.makeme(gc, buff);
    }

    protected internal override void run()
    {
      Account player = this.getClient().getPlayer();
      Room room = player.getRoom();
      if (player == null || room == null)
        return;
      if (room._aiLevel < 10)
        ++room._aiLevel;
      for (int id = 0; id < 16; ++id)
      {
        Account playerFromPlayerId = ChannelInfoHolder.getChannel(this.getClient().getChannelId()).getPlayerFromPlayerId(room.getSlot(id)._playerId);
        if (playerFromPlayerId != null)
          playerFromPlayerId.sendPacket((SendBaseGamePacket) new opcode_3890_ACK(room));
      }
      UdpHandler.getInstance().PingUdpServer();
    }

    protected internal override void read()
    {
      int num = (int) this.readH();
    }
  }
}
