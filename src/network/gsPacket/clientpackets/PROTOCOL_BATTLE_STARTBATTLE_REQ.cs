// Type: PBServer.src.network.gsPacket.clientpackets.PROTOCOL_BATTLE_STARTBATTLE_REQ
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.network;
using PBServer.network.Game.packets.serverpackets;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;

namespace PBServer.src.network.gsPacket.clientpackets
{
  internal class PROTOCOL_BATTLE_STARTBATTLE_REQ : ReceiveBaseGamePacket
  {
    public PROTOCOL_BATTLE_STARTBATTLE_REQ(GameClient Client, byte[] data)
    {
      this.makeme(Client, data);
    }

    protected internal override void read()
    {
    }

    protected internal override void run()
    {
      Account player = this.getClient().getPlayer();
      Room room = player.getRoom();
      player.sendPacket((SendBaseGamePacket) new opcode_3890_ACK(room));
      player.sendPacket((SendBaseGamePacket) new S_BATTLE_ROOMINFO(room));
      if (room.getSlotState(player.getSlot()) != SLOT_STATE.SLOT_STATE_PRESTART)
        return;
      room.changeSlotState(player.getSlot(), SLOT_STATE.SLOT_STATE_BATTLE_READY, true);
    }
  }
}
