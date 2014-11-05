using PBServer;
using PBServer.src.model.rooms;
using PBServer.src.model.accounts;

namespace PBServer.network.Game.packets.serverpackets
{
    public class S_ROOM_CHANGE_INFO : SendBaseGamePacket
    {
        private Room _room;
        private Account _player;
        private string _name;
        private int _map_id;
        private int _all_weapons;
        private int _aiCount;
        private int _aiLevel;
        private int _random_map;
        private int _room_type;

        public S_ROOM_CHANGE_INFO(Account p, string name, int map_id, int all_weapons, int aicount, int ailevel, int random_map, int map_type)
        {
            this.makeme();
            this._player = p;
            this._room = _player.getRoom();
            this._name = name;
            this._map_id = map_id;
            this._all_weapons = all_weapons;
            this._aiCount = aicount;
            this._aiLevel = ailevel;
            this._random_map = random_map;
            this._room_type = map_type;
        }

        protected internal override void write()
        {
            CLogger.getInstance().info("Recebendo: S_ROOM_CHANGE_INFO");
            this.writeH((short)3592);
            this.writeD(this._player.getSlot());//_player.getSlot()
            this.writeS(this._name, 23);
            this.writeC((byte)this._map_id); //map_id
            this.writeH((short)0);
            this.writeC((byte)this._room_type);
            this.writeC((byte)5);
            this.writeC((byte)1);
            this.writeC((byte)this._room.getSlotCount()); // кол-во слотов.
            this.writeC((byte)5);
            this.writeC((byte)this._all_weapons); //Ограничение по оружию
            this.writeC((byte)this._random_map); //Случайная карта - 2
            this.writeC((byte)this._room.special); //Специальный тип боя)
            this.writeS(this._room.getLeader().getPlayerName(), 33); // Ник нейм
            this.writeD(this._room.killtime); //Условия - количество убийств(Маска, складываются порядковые номера)
            this.writeC((byte)this._room.limit); //Лимит на вход
            this.writeC((byte)this._room.seeConf); //Настройки наблюдения
            this.writeH((short)this._room.autobalans); //Автобаланс
            if (this._aiCount > 0 && this._aiLevel > 0)
            {
                this.writeC((byte)this._aiCount);
                this.writeC((byte)this._aiLevel);
            }
        }
    }
}
