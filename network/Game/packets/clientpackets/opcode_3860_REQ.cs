using PBServer;
using PBServer.network;
using PBServer.network.serverpackets;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;
using PBServer.src.network.gsPacket.serverpackets;
using PBServer.network.BattleConnect;

namespace PBServer.network.clientpacket
{
  internal class opcode_3860_REQ : ReceiveBaseGamePacket
  {
    private byte[] slots;

    public opcode_3860_REQ(GameClient Client, byte[] data)
    {
      this.makeme(Client, data);
    }

    protected internal override void read()
    {
      int num = (int) this.readH();
      this.slots = this.readB(16);
    }

    protected internal override void run()
    {
        if (this.getClient() == null)
            return;
        Account player = this.getClient().getPlayer();
        Room room = player.getRoom();
        int num = 0;
        if (room == null)
            return;
        for (int slot = 0; slot < 15; ++slot)
        {
            Account playerBySlot = room.getPlayerBySlot(slot);
            if (playerBySlot != null)
            {
                playerBySlot.sendPacket((SendBaseGamePacket)new opcode_3860_ACK(this.slots));
                if (playerBySlot.getRoom().getSlotState(playerBySlot.getSlot()) == SLOT_STATE.SLOT_STATE_LOAD || playerBySlot.getRoom().getSlotState(playerBySlot.getSlot()) == SLOT_STATE.SLOT_STATE_RENDEZVOUS || playerBySlot.getRoom().getSlotState(playerBySlot.getSlot()) == SLOT_STATE.SLOT_STATE_PRESTART)
                    ++num;
            }
        }
        if (num == 0)
            player.getRoom().ChangeRoomState(ROOM_STATE.ROOM_STATE_BATTLE, player);
    }
  }
}
