using PBServer;
using PBServer.network;
using PBServer.network.serverpackets;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;

namespace PBServer.network.clientpacket
{
  internal class PROTOCOL_BATTLE_RESPAWN_REQ : ReceiveBaseGamePacket
  {
    private int _first;
    private int _second;
    private int _third;
    private int _fourth;
    private int _fifth;
    private int _id;
    private int _red;
    private int _blue;
    private int _head;
    private int _beret;
    private int _dino;
    private int _UNK;

    public PROTOCOL_BATTLE_RESPAWN_REQ(GameClient Client, byte[] data)
    {
      this.makeme(Client, data);
    }

    protected internal override void read()
    {
      int num = (int) this.readH();
      this._first = this.readD();
      this._second = this.readD();
      this._third = this.readD();
      this._fourth = this.readD();
      this._fifth = this.readD();
      this._id = this.readD();
      this._red = this.readD();
      this._blue = this.readD();
      this._head = this.readD();
      this._beret = this.readD();
      this._dino = this.readD();
      this._UNK = (int) this.readC();
    }

    protected internal override void run()
    {
      Account player = this.getClient().getPlayer();
      if (player == null)
        return;
      Room room = player.getRoom();
      if (room == null)
        return;
      for (int index = 0; index < room.getAllPlayers().Count; ++index)
      {
        Account account = this.getClient().getPlayer().getRoom().getAllPlayers()[index];
        if (account != null && room.getSlot(account.getSlot()).state == SLOT_STATE.SLOT_STATE_BATTLE)
        {
          CLogger.getInstance().info("Send: PROTOCOL_BATTLE_RESPAWN_ACK | o jogador " + player.getPlayerName() + " pede o " + account.player_name);
          account.sendPacket((SendBaseGamePacket) new S_BATTLE_RESPAWN(new ResInfo(this._first, this._second, this._third, this._fourth, this._fifth, this._id, this._red, this._blue, this._head, this._beret, this._dino), player));
        }
      }
    }
  }
}
