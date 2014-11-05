// Type: PBServer.network.serverpackets.PROTOCOL_BASE_GET_SCHANNELLIST_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using System;
using System.IO;
using System.Xml;
using System.Net;
using PBServer.src.data.xml.holders;
using PBServer.src.data.xml.parsers;
using System.Collections.Generic;
using System.Collections;
using PBServer.src.data.model;

namespace PBServer.network.serverpackets
{
  public class S_BASE_GET_SCHANNELLIST : SendBaseLoginPacket
  {
    private LoginClient _lc;

    public S_BASE_GET_SCHANNELLIST(LoginClient lc)
    {
      this.makeme();
      this._lc = lc;
    }

    protected internal override void write()
    {
      CLogger.getInstance().info("Send: PROTOCOL_BASE_GET_SCHANNELLIST_ACK");
      this.writeH((short) 2049);
      this.writeD(5404);
      this.writeB(new byte[4]
      {
        (byte) 1,
        (byte) 0,
        (byte) 0,
        (byte) 127
      });
      this.writeH((short) 29890);
      this.writeH((short) 32759);
      for (int index = 0; index < 10; ++index) //10
      {
          this.writeC((byte)1);
      }
      this.writeC((byte) 1);
      int count1 = GameServerInfoHolder.getInstance().getAllGameserverInfos().Count;
      this.writeD(count1);
          for (int index = 0; index < 2; ++index)
          {
              this.writeD(GameServerInfoHolder.getInstance().getAllGameserverInfos().Count);
              this.writeB(IPAddress.Parse(Config.GAME_HOST).GetAddressBytes());
              this.writeH((short)Config.GAME_PORT);
              this.writeC((byte)GameServerInfoHolder.getInstance().getGameServerInfo(index).getTypeGameServer());
              this.writeH((short)GameServerInfoHolder.getInstance().getGameServerInfo(index).getMaxPlayers());
              this.writeD(0);
          }
      int count2 = 0; // ХЗ небыло пока > 0
      this.writeH((short)count2);
      for (int i = 0; i < count2; i++)
      {
          this.writeH(0);
          this.writeD(0);
          this.writeD(0);
      }

      int count3 = 1; // ХЗ на руоффе тут 1
      this.writeC((byte)count3);
      for (int i = 0; i < count3; i++)
      {
          writeB(new byte[12]); // Всегда в конце нули. Руофф.
      }
    }
  }
}
