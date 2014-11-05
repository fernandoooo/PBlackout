// Type: PBServer.src.network.gsPacket.clientpackets.PROTOCOL_BATTLE_ENDBATTLE_REQ
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.network;
using PBServer.network.BattleConnect;
using PBServer.src.model.accounts;
using PBServer.src.network.gsPacket.serverpackets;

namespace PBServer.src.network.gsPacket.clientpackets
{
  public class PROTOCOL_BATTLE_ENDBATTLE_REQ : ReceiveBaseGamePacket
  {
    private int itemid;

    public PROTOCOL_BATTLE_ENDBATTLE_REQ(GameClient Client, byte[] data)
    {
      this.makeme(Client, data);
    }

    protected internal override void read()
    {
      int num = (int) this.readH();
      this.itemid = this.readD();
    }

    protected internal override void run()
    {
      Account player = this.getClient().getPlayer();
      UdpHandler.getInstance().RemovePlayerInRoom(player);
      player.sendPacket((SendBaseGamePacket) new S_BATTLE_ENDBATTLE(player));
    }
  }
}
