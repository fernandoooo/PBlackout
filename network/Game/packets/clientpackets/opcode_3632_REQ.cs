using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PBServer.src.model.rooms;
using PBServer.src.model.accounts;
using PBServer.network.Game.packets.serverpackets;
using System.IO;

namespace PBServer.network.Game.packets.clientpackets
{
    public class opcode_3632_REQ : ReceiveBaseGamePacket
    {
        private Account p;
        private Room r;
        private int autobalans;
        private int limit;
        private int seeConf;
        private int killtime;
        private string name;
        private int map_id;
        private int random_map;
        private int allweapons;
        private int aiCount;
        private int aiLevel;
        private int room_type;
        private int special;
        private int unk1;

        public opcode_3632_REQ(GameClient Client, byte[] data)
        {
            this.makeme(Client, data);
        }

        protected internal override void read()
        {
            p = this.getClient().getPlayer();
            r = p.getRoom();
            readH();
            readD();
            name = readS(23);
            readB(3);
            map_id = readC();
            readH();
            room_type = readC();
            readB(4);
            allweapons = readC();
            random_map = readC();
            special = readC();
            readS(33);
            killtime = readD();
            limit = readC();
            seeConf = readC();
            autobalans = readC();
            unk1 = readC();
            aiCount = readC();
            aiLevel = readC();
        }

        protected internal override void run()
        {
            p.getRoom().name = this.name;
            p.getRoom().map_id = this.map_id;
            p.getRoom().allweapons = this.allweapons;
            p.getRoom().random_map = this.random_map;
            p.getRoom().autobalans = this.autobalans;
            p.getRoom().seeConf = this.seeConf;
            p.getRoom().killtime = this.killtime;
            p.getRoom().limit = this.limit;
            p.getRoom().special = this.special;
            p.getRoom().room_type = this.room_type;
            p.getRoom()._aiCount = this.aiCount;
            p.getRoom()._aiLevel = this.aiLevel;
            foreach (Account pl in r.getAllPlayers())
            {
                pl.sendPacket(new opcode_3608_ACK(this.autobalans, this.seeConf, this.killtime, this.limit));
                pl.sendPacket(new S_ROOM_CHANGE_INFO(pl, this.name, this.map_id, this.allweapons, this.aiCount, this.aiLevel, this.random_map, this.room_type));
            }
        }
    }
}
