using PBServer;
using MySql.Data.MySqlClient;
using PBServer.db;
using PBServer.src.managers;
using System;

namespace PBServer.network.Game.packets.serverpackets
{
    public class S_CREATE_EQUIPMENT : SendBaseGamePacket
    {
        public int _type;
        public int _item;
        public int p_id;
        public int _item_type;
        public string _item_name;
        public int _count;
        public int _equip;
        public int _pag_weapon;
        public int _unk1;
        public int _unk2;

        public S_CREATE_EQUIPMENT(int item, int type, int p_id, string item_name, int count, int equip)
        {
            this._item = item;
            this._type = type;
            this.p_id = p_id;
            this._item_name = item_name;
            this._count = count;
            this._equip = equip;
            base.makeme();
        }
        protected internal override void write()
        {
            if (this._item < 1105004000 &&
                this._item > 1001001000)
            {
                this._pag_weapon = 1;
                this._unk1 = 1;
                this._unk2 = 0;
            }
            if (this._item < 1001001000)
            {
                this._pag_weapon = 0;
                this._unk1 = 1;
                this._unk2 = 0;
            }
            if (this._item > 1105004000)
            {
                this._pag_weapon = 0;
                this._unk1 = 0;
                this._unk2 = 2;
            }
            base.writeH(3588);
            base.writeC(0);
            base.writeD(this._pag_weapon); //0 - Armas, 1 - Personagens, 27 - Cupons
            base.writeD(this._unk1); //1
            base.writeD(this._unk2); //0
            base.writeD(this._item); //0
            base.writeD(this._item); //0
            base.writeD(this._item);
            if (this._item < 600000000)
                this._type = 1;

            if (this._item < 700000000 &&
            this._item > 600000000)
                this._type = 2;

            if (this._item < 800000000 &&
            this._item > 700000000)
                this._type = 3;

            if (this._item < 900000000 &&
            this._item > 800000000)
                this._type = 4;

            if (this._item < 1000000000 &&
            this._item > 900000000)
                this._type = 5;

            if (this._item < 1001002000 &&
            this._item > 1001001000)
                this._type = 6;

            if (this._item < 1001003000 &&
            this._item > 1001002000)
                this._type = 7;

            if (this._item < 1104004000 &&
            this._item > 1104003000)
                this._type = 8;

            if (this._item < 1105004000 &&
            this._item > 1105003000)
                this._type = 8;

            if (this._item < 1102004000 &&
            this._item > 1102003000)
                this._type = 8;

            if (this._item < 1103004000 &&
            this._item > 1103003000)
                this._type = 10; //BOINA

            if (this._item < 1006004000 &&
            this._item > 1006001000)
                this._type = 9; //DINO

            if (this._item < 1301510000 &&
            this._item > 1300002000)
                this._type = 11;

            if (this._item < 700000000 &&
            this._item > 600000000)
                this._item_type = 1;

            if (this._item < 800000000 &&
            this._item > 700000000)
                this._item_type = 1;

            if (this._item < 900000000 &&
            this._item > 800000000)
                this._item_type = 1;

            if (this._item < 1000000000 &&
            this._item > 900000000)
                this._item_type = 1;

            if (this._item < 1001002000 &&
            this._item > 1001001000)
                this._item_type = 2;

            if (this._item < 1001003000 &&
            this._item > 1001002000)
                this._item_type = 2;

            if (this._item < 1104004000 &&
            this._item > 1104003000)
                this._item_type = 2;

            if (this._item < 1105004000 &&
            this._item > 1105003000)
                this._item_type = 2;

            if (this._item < 1102004000 &&
            this._item > 1102003000)
                this._item_type = 2;

            if (this._item < 1103004000 &&
            this._item > 1103003000)
                this._item_type = 2;

            if (this._item < 1006004000 &&
            this._item > 1006001000)
                this._item_type = 2;

            if (this._item < 1301510000 &&
            this._item > 1300002000)
                this._item_type = 3;

            using (MySqlConnection mySqlConnection = SQLjec.getInstance().conn())
            {
                mySqlConnection.Open();
                MySqlCommand mySqlCommand = new MySqlCommand(string.Concat(new object[]
				{
					"INSERT INTO items(owner_id,item_id,item_name,item_type,count,equip,loc_slot)VALUES(" + "'" + this.p_id + "'" + "," + "'" + this._item + "'" + "," + "'" + this._item_name + "'" + "," + "'" + "," + this._item_type + "'" + "," + "'" + this._count + "'" + "," + "'" + this._equip + "'" + "," + "'" + this._type + "'" +
					")"
				}), mySqlConnection);
                mySqlCommand.ExecuteNonQuery();
            }
            this.writeC((byte)this._equip);
            this.writeD(this._count);
            this.writeC(0);
            this.writeC(0);
            base.writeC(1);
        }
    }
}