// Type: PBServer.network.Game.packets.serverpackets.opcode_3653_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.src.model.accounts;
using System;
using PBServer.model.clans;
using PBServer.managers;

namespace PBServer.network.Game.packets.serverpackets
{
  public class S_ROOM_PLAYER_ENTER : SendBaseGamePacket
  {
    private Account pl;

    public S_ROOM_PLAYER_ENTER(Account p)
    {
      this.pl = p;
      this.makeme();
    }

    protected internal override void write()
    {
        Clan c = ClanManager.getInstance().get(pl.getClanId());
        this.writeH(3909);
        this.writeD(pl.getSlot());
        this.writeC((byte)pl.getRoom().getSlotState(this.pl.getSlot())); // слот-статус
        this.writeC((byte)pl.getRank()); // ранг игрока
        this.writeC(0x2d);
        this.writeC(0x40);
        this.writeB(new byte[10]); // неизвестные байты
        if (pl.getClanId() == 0)
        {
            this.writeC((byte)255); // иконка клана
            this.writeC((byte)255); // иконка клана
            this.writeC((byte)255); // иконка клана
            this.writeC((byte)255); // иконка клана
            this.writeC((byte)0); // иконка клана
            this.writeB(new byte[6]);
            this.writeS("", 17);
        }
        if (pl.getClanId() > 0)
        {
            this.writeC((byte)c.getLogo1()); // иконка клана
            this.writeC((byte)c.getLogo2()); // иконка клана
            this.writeC((byte)c.getLogo3()); // иконка клана
            this.writeC((byte)c.getLogo4()); // иконка клана
            this.writeC((byte)c.getLogoColor()); // иконка клана
            this.writeB(new byte[6]);
            this.writeS(c.getClanName(), 17);
        }
        this.writeS(pl.getPlayerName(), 33); //Apelido
        this.writeC((byte)pl.getNameColor()); //Cor do nickname
    }
  }
}
