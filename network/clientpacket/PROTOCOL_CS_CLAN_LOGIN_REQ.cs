using PBServer;
using PBServer.network;
using PBServer.network.serverpackets;
using PBServer.src.model.rooms;
using PBServer.network.Game.packets.serverpackets;
using PBServer.src.model.accounts;
using PBServer.model.clans;
using PBServer.model.players;
using PBServer.managers;
using System.Collections.Generic;

namespace PBServer.network.clientpacket
{
  internal class PROTOCOL_CS_CLAN_LOGIN_REQ : ReceiveBaseGamePacket
  {
    public PROTOCOL_CS_CLAN_LOGIN_REQ(GameClient Client, byte[] data)
    {
      this.makeme(Client, data);
    }

    protected internal override void read()
    {
    }

    protected internal override void run()
    {
      Account p = this.getClient().getPlayer();
      GameClient client = this.getClient();
      List<Clan> _clans = ClanManager.getInstance().getClans();
      if (client.getPlayer() != null && client.getPlayer().getRoom() != null)
        client.getPlayer().getRoom().changeSlotState(client.getPlayer().getSlot(), SLOT_STATE.SLOT_STATE_CLAN, true);
      if (p.getClanId() == 0)
      {
          client.sendPacket((SendBaseGamePacket)new S_CLAN_ENTER(0, p, _clans));
          //client.sendPacket((SendBaseGamePacket)new opcode_519_ACK());
      }

      if (p.getClanId() > 0)
      {
          client.sendPacket((SendBaseGamePacket)new S_CLAN_ENTER(1, p, _clans));
          //client.sendPacket((SendBaseGamePacket)new opcode_519_ACK());
      }
      CLogger.getInstance().info("O jogador " + this.getClient().getPlayer().getPlayerName().ToString() + " entrou da tela de clan.");
    }
  }
}
