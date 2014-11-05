using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PBServer.src.data.xml.parsers;
using PBServer.src.model.accounts;
using PBServer.model.players;
using PBServer.src.model;
using PBServer.src.model.rooms;
using PBServer.src.managers;
using PBServer.managers;

namespace PBServer.network.serverpackets
{
    public class S_LOBBY_GET_PLAYERINFO : SendBaseGamePacket
    {
        private int _id;

        public S_LOBBY_GET_PLAYERINFO(int id)
        {
            this.makeme();
            _id = id;
        }

        protected internal override void write()
        {
            Account pl_ = AccountManager.getInstance().getAccountInObjectId(this._id);
            this.writeH(2639);
            this.writeD(pl_._statistic.getFights(true)); //getFights
            this.writeD(pl_._statistic.getWinFights(true)); //getWinFights
            this.writeD(pl_._statistic.getLostFights(true)); //getLostFights
            this.writeD(0);
            this.writeD(pl_._statistic.getKills(true)); //getKills
            this.writeD(0);
            this.writeD(pl_._statistic.getDeaths(true)); //getDeaths
            this.writeD(0);
            this.writeD(0);
            this.writeD(pl_._statistic.getEscapes(true)); //getEscapes
            this.writeD(pl_._statistic.getFights(true)); //getFights
            this.writeD(pl_._statistic.getWinFights(true)); //getWinFights
            this.writeD(pl_._statistic.getLostFights(true)); //getLostFights
            this.writeD(0);
            this.writeD(pl_._statistic.getKills(true)); //getKills
            this.writeD(0);
            this.writeD(pl_._statistic.getDeaths(true)); //getDeaths
            this.writeD(0);
            this.writeD(0);
            this.writeD(pl_._statistic.getEscapes(true)); //getEscapes
        }
    }
}
