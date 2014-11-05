using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PBServer.src.model.rooms;
using PBServer.src.model.accounts;
using PBServer.src.managers;
using PBServer.network.Game.packets.serverpackets;
using PBServer.src.data.xml.parsers;

namespace PBServer.network.Game.packets.clientpackets
{
    /**
     * @author Felixx
     *         Чтение настроек разрешенного оружия.
     */
    public class opcode_3591_REQ : ReceiveBaseGamePacket
    {
        private int _unk1, _unk2, _unk3, _mapId, _unk4, _unk5, _room_type, _unk7, _allweapons, _unk8, _unk9, _unk10, _random_map, _aiCount, _aiLevel, _unk12;
        private string _name;

        public opcode_3591_REQ(GameClient Client, byte[] data)
        {
            this.makeme(Client, data);
        }

        protected internal override void read()
        {
            this._unk1 = readH();
            this._unk2 = readD(); // Ошибка?
            this._name = readS(22); // Название
            this._unk3 = readC();// Не меняется = 0
            this._mapId = readC();// Карта
            this._unk4 = readC(); // Не меняется = 0
            this._unk5 = readC();// Не меняется = 0        
            this._room_type = readC(); // Всегда 1, на дино 7
            this._unk7 = readC(); // Не меняется = 0
            this._unk8 = readC(); // Не меняется = 0
            this._unk9 = readC(); // Не меняется = 0
            this._unk10 = readC(); // Не меняется = 0
            this._allweapons = readC(); // Разрешонное оружие
            this._random_map = readC(); // Рандом мапа
            this._unk12 = readC(); // Что то про ботов, 6 выставляется /3-на дробовиках /2 -дино
            this._aiCount = readC(); // Кол-во ботов /1-на дробовиках / 1 - дино
            this._aiLevel = readC(); // Сложноть ботов /1-на дробовиках / 1 -дино
        }

        protected internal override void run()
        {
            Account player = this.getClient().getPlayer();
            player.getRoom().name = this._name;
            player.getRoom().map_id = this._mapId;
            player.getRoom().allweapons = this._allweapons;
            player.getRoom()._aiCount = this._aiCount;
            player.getRoom()._aiLevel = this._aiLevel;
            player.getRoom().random_map = this._random_map;
            player.getRoom().room_type = this._room_type;
            foreach (Account account in player.getRoom().getAllPlayers())
                account.sendPacket((SendBaseGamePacket)new S_ROOM_CHANGE_INFO(player, this._name, this._mapId, this._allweapons, this._aiCount, this._aiLevel, this._random_map, this._room_type));
        }
    }
}
