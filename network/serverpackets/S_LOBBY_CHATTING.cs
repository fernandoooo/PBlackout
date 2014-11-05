// Type: PBServer.network.serverpackets.PROTOCOL_LOBBY_CHATTING_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.src.model;
using PBServer.src.model.accounts;

namespace PBServer.network.serverpackets
{
  public class S_LOBBY_CHATTING : SendBaseGamePacket
  {
    protected Chat _chat;
    private Account player;
    public S_LOBBY_CHATTING(Chat chat, Account player)
    {
      this._chat = chat;
      this.player = player;
      this.makeme();
    }

    protected internal override void write()
    {
        this.writeH((short)3851);
        this.writeD(this.player.getPlayerId());
        this.writeC((byte)33);
        this.writeS(this._chat.playername, 33);
        this.writeC((byte)this.player.getNameColor());
        this.writeH((short)this._chat.chat.Length);
        this.writeS(this._chat.chat, this._chat.chat.Length);
    }
  }
}
