// Type: PBServer.network.serverpackets.PROTOCOL_BASE_GET_CHANNELLIST_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using System.Net;
using PBServer.src.data.xml.parsers;
using PBServer.src.model;
using System;
using System.Globalization;
using PBServer.src.templates;
using PBServer.src.data.xml.holders;
using System.Collections.Generic;

namespace PBServer.network.serverpackets
{
    public class S_BASE_GET_CHANNELLIST : SendBaseGamePacket
    {
        private GameClient _gc;

        public S_BASE_GET_CHANNELLIST(GameClient gc)
        {
            this.makeme();
            this._gc = gc;
        }

        protected internal override void write()
        {
            this.writeH((short)2049);
            this.writeD(5404);
            this.writeB(IPAddress.Parse(Config.GAME_HOST).GetAddressBytes());
            this.writeH((short)this._gc.getCryptKey());
            this.writeH((short)32759);
            for (int index = 0; index < 10; ++index)
            this.writeC((byte)ChannelInfoHolder.getChannel(index).getTypeChannel());
            this.writeC((byte)1);
            int count1 = 1;
            this.writeD(count1 + 1);
            for (int index = 0; index < 2; ++index)
            {
                this.writeD(1); //2
                this.writeB(IPAddress.Parse(Config.GAME_HOST).GetAddressBytes());
                this.writeH((short)Config.GAME_PORT);
                this.writeC((byte)1); //TIPO DE CANAL
                this.writeH((short)ChannelInfoHolder.getChannel(index).getMaxPlayers()); //MAXIMO DE JOGADORES NO CANAL
                this.writeD(ChannelInfoHolder.getChannel(index).getAllPlayers().Count); //QUANTIDADE DE JOGADORES NO CANAL
            }
            for (int i = 0; i < count1; i++)
            {
                writeB(new byte[17]); // Попробуем не посылать. ибо нах оно тут?
            }

            int count2 = 0; // ХЗ небыло пока > 0
            this.writeH((short)count2);
            for (int i = 0; i < count2; i++)
            {
                this.writeH((short)0);
                this.writeD(0);
                this.writeD(0);
            }

            int count3 = 2;
            this.writeC((byte)count3); // ХЗ на руоффе тут 2
            for (int i = 0; i < count3; i++)
            {
                this.writeB(new byte[12]); // Всегда в конце нули. Руофф.
            }
        }
    }
}
