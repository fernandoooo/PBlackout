using PBServer;
using PBServer.network;
using PBServer.network.BattleConnect;
using PBServer.network.Game.packets.serverpackets;
using PBServer.network.serverpackets;
using PBServer.src.data.xml.parsers;
using PBServer.src.managers;
using PBServer.src.model;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;
using System;
using System.Net;
using PBServer.src.data.xml.holders;

namespace PBServer.network.clientpacket
{
  internal class PROTOCOL_LOBBY_CHATTING_REQ : ReceiveBaseGamePacket
  {
    private Chat chat;
    private int _len;

    public PROTOCOL_LOBBY_CHATTING_REQ(GameClient Client, byte[] data)
    {
      this.chat = new Chat();
      this.makeme(Client, data);
    }

    protected internal override void read()
    {
      int num = (int) this.readH();
      this.chat.chat_type = this.readH();
      this._len = (int) this.readH();
      this.chat.chat = this.readS(this._len - 1);
      this.chat.playername = this.getClient().getPlayer().getPlayerName();
    }

    protected internal override void run()
    {
      CLogger.getInstance().info("O jogador " + this.getClient().getPlayer().getPlayerName() + " escreveu " + this.chat.chat);
      Account player = this.getClient().getPlayer();
      Room room = player.getRoom();
      if (room != null)
      {
          for (int slot = 0; slot < 15; ++slot)
          {
            Account playerBySlot = room.getPlayerBySlot(slot);
            if (playerBySlot != null)
                playerBySlot.sendPacket((SendBaseGamePacket)new S_LOBBY_CHATTING(this.chat, this.getClient().getPlayer()));
          }
      }
      else
      {
        Channel channel = ChannelInfoHolder.getChannel(this.getClient().getChannelId());
        if (channel != null)
        {
          for (int index = 0; index < channel.getWaitPlayers().Count; ++index)
          {
            if (channel.getWaitPlayers()[index] != null)
                channel.getWaitPlayers()[index].sendPacket((SendBaseGamePacket)new S_LOBBY_CHATTING(this.chat, this.getClient().getPlayer()));
          }
        }
      }
    }
  }
}
