using PBServer;
using PBServer.network;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;
using PBServer.network.serverpackets;

namespace PBServer.network.Game.packets.clientpackets
{
  public class PROTOCOL_ROOM_CLOSE_SLOT_REQ : ReceiveBaseGamePacket
  {
    private int _slot;

    public PROTOCOL_ROOM_CLOSE_SLOT_REQ(GameClient gc, byte[] buff)
    {
      this.makeme(gc, buff);
    }

    protected internal override void read()
    {
      int num1 = (int) this.readH();
      this._slot = (int) this.readC();
      int num2 = (int) this.readC();
      int num3 = (int) this.readC();
      int num4 = (int) this.readC();
    }

    protected internal override void run()
    {
      Account player = this.getClient().getPlayer();
      Account p2 = player.getRoom().getPlayerBySlot(this._slot);
      if (player == null)
        return;
      Room room = player.getRoom();
      if (room == null)
        return;
      switch (room.getSlotState(this._slot))
      {
        case SLOT_STATE.SLOT_STATE_EMPTY:
          room.changeSlotState(this._slot, SLOT_STATE.SLOT_STATE_CLOSE, true);
          break;
        case SLOT_STATE.SLOT_STATE_CLOSE:
          room.changeSlotState(this._slot, SLOT_STATE.SLOT_STATE_EMPTY, true);
          break;
      }
      if (p2 == null)
      {
          player.getRoom().updateInfo();
      }
      else
      {
          p2.sendPacket((SendBaseGamePacket)new S_LOBBY_ENTER());
          p2.getRoom().removePlayer(p2);
          CLogger.getInstance().info("O jogador " + p2.getPlayerName() + " foi kikado da sala " + this.getClient().getPlayer().getRoom().getRoomId());
          player.getRoom().updateInfo();
          p2.sendPacket((SendBaseGamePacket)new S_KICK_PLAYER(this._slot));
      }
    }
  }
}
