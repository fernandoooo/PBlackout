// Type: PBServer.network.Game.packets.serverpackets.opcode_3879_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;
using System;
using System.Globalization;

namespace PBServer.network.Game.packets.serverpackets
{
  public class opcode_3879_ACK : SendBaseGamePacket
  {
    private Room _room;

    public opcode_3879_ACK(Room room)
    {
      this.makeme();
      this._room = room;
    }

    protected internal override void write()
    {
      this.writeH((short) 3879);
      for (int slot = 0; slot < 16; ++slot)
      {
        Account playerBySlot = this._room.getPlayerBySlot(slot);
        if (playerBySlot != null)
        {
          this.writeD(playerBySlot.getSlot());
          this.writeD(playerBySlot.getCharRed());
          this.writeD(playerBySlot.getCharBlue());
          this.writeD(playerBySlot.getCharHelmet());
          this.writeD(playerBySlot.getCharDino());
          this.writeD(playerBySlot.getCharBeret());
          this.writeD(playerBySlot.getPrimaryWeapon());
          this.writeD(playerBySlot.getSecondaryWeapon());
          this.writeD(playerBySlot.getMeleeWeapon());
          this.writeD(playerBySlot.getThrownNormalWeapon());
          this.writeD(playerBySlot.getThrownSpecialWeapon());
          this.writeD(0); //CUPON ATIVO?
          this.writeB(new byte[5]
          {
            (byte) 100,
            (byte) 100,
            (byte) 100,
            (byte) 100,
            (byte) 100
          });
          this.writeC((byte) 1);
          this.writeD(0);
        }
        else
        {
          this.writeD(slot);
          this.writeB(new byte[20]);
          this.writeB(new byte[24]);
          this.writeB(new byte[5]
          {
            (byte) 100, //100
            (byte) 100, //100
            (byte) 100, //100
            (byte) 100, //100
            (byte) 100 //100
          });
          this.writeD(0);
        }
      }
    }
  }
}
