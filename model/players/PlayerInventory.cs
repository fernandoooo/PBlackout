using PBServer;
using PBServer.data.model;
using PBServer.data.xml.holders;
using PBServer.src.managers;
using System.Collections.Generic;
using PBServer.network.clientpacket;

namespace PBServer.model.players
{
    public class PlayerInventory
    {
        public int player_id;
        private PlayerEquep _equep = new PlayerEquep();
        private List<ItemsModel> _inventory = new List<ItemsModel>();

        public List<ItemsModel> getItemFromSlot(int slot)
        {
            List<ItemsModel> _temp = new List<ItemsModel>();
            for (int i = 0; i < _inventory.Count; i++)
            {
                if (_inventory[i].slot == slot)
                    _temp.Add(_inventory[i]);
            }
            return _temp;
        }

        public bool CheckEQInventory()
        {
            bool flag = false;
            if (this._equep.CHAR_BLUE == 0 || this._equep.CHAR_DINO == 0 || this._equep.CHAR_HEAD == 0 || this._equep.CHAR_RED == 0)
            {
                this._equep.CHAR_BLUE = AccountManager.getInstance().getAccountInObjectId(this.player_id).getCharBlue();
                this._equep.CHAR_DINO = AccountManager.getInstance().getAccountInObjectId(this.player_id).getCharDino();
                this._equep.CHAR_HEAD = AccountManager.getInstance().getAccountInObjectId(this.player_id).getCharHelmet();
                this._equep.CHAR_ITEM = AccountManager.getInstance().getAccountInObjectId(this.player_id).getCharBeret();
                this._equep.CHAR_RED = AccountManager.getInstance().getAccountInObjectId(this.player_id).getCharRed();
                flag = true;
            }
            if (this._equep.PRIM == 0 || this._equep.SUB == 0 || (this._equep.MELEE == 0 || this._equep.ITEM == 0) || this._equep.THROWING == 0)
            {
                this._equep.PRIM = AccountManager.getInstance().getAccountInObjectId(this.player_id).getPrimaryWeapon();
                this._equep.SUB = AccountManager.getInstance().getAccountInObjectId(this.player_id).getSecondaryWeapon();
                this._equep.MELEE = AccountManager.getInstance().getAccountInObjectId(this.player_id).getMeleeWeapon();
                this._equep.ITEM = AccountManager.getInstance().getAccountInObjectId(this.player_id).getThrownNormalWeapon();
                this._equep.THROWING = AccountManager.getInstance().getAccountInObjectId(this.player_id).getThrownSpecialWeapon();
                flag = true;
            }
            return flag;
        }

        public void CheckCorrectInventory(int player_id)
        {
            if (!this.CheckEQInventory())
                return;
            List<PlayerTemplateInventory> playerInventoryStatic = StartedInventoryItemsHolder.getInstance().getPlayerInventoryStatic();
            for (int index = 0; index < playerInventoryStatic.Count; ++index)
            {
                if (!this.isItemInventoryExist(player_id, playerInventoryStatic[index].id))
                {
                    ItemsModel itemsModel = new ItemsModel(playerInventoryStatic[index].id, playerInventoryStatic[index].slot, playerInventoryStatic[index].name, playerInventoryStatic[index].onEquip, playerInventoryStatic[index].count, playerInventoryStatic[index].equip_type);
                    this._inventory.Add(itemsModel);
                    if (playerInventoryStatic[index].onEquip == 1)
                        this.setEquipItemFromSlot(playerInventoryStatic[index].id, playerInventoryStatic[index].slot);
                    AccountManager.getInstance().AddInitialItems(player_id, itemsModel, playerInventoryStatic[index].name, playerInventoryStatic[index].onEquip);
                }
            }
        }

        public PlayerInventory(int id_do_jogador)
        {
            this.player_id = id_do_jogador;
        }

        public PlayerEquep getEquipAll()
        {
            return this._equep;
        }

        public List<ItemsModel> getItemsAll()
        {
            return this._inventory;
        }

        public void AddItem(ItemsModel i)
        {
            this._inventory.Add(i);
        }

        public List<ItemsModel> getAllItemsOnType(int type)
        {
            List<ItemsModel> list = new List<ItemsModel>();
            foreach (ItemsModel itemsModel in this._inventory.ToArray())
            {
                if (itemsModel.slot >= 1 && itemsModel.slot < 6 && type == 1)
                    list.Add(itemsModel);
                if (itemsModel.slot >= 6 && itemsModel.slot < 11 && type == 2)
                    list.Add(itemsModel);
                if (itemsModel.slot >= 11 && itemsModel.slot < 12 && type == 3)
                    list.Add(itemsModel);
            }
            return list;
        }

        public List<ItemsModel> getAllNoEquipItems()
        {
            List<ItemsModel> list = new List<ItemsModel>();
            foreach (ItemsModel itemsModel in this._inventory.ToArray())
            {
                if (itemsModel.equip == 0)
                    list.Add(itemsModel);
            }
            return list;
        }

        public List<ItemsModel> getAllItemsOnTypeEquip(int type)
        {
            List<ItemsModel> list = new List<ItemsModel>();
            foreach (ItemsModel itemsModel in this._inventory.ToArray())
            {
                if (itemsModel.slot >= 1 && itemsModel.slot < 6 && type == 1 && itemsModel.equip == 1)
                    list.Add(itemsModel);
                if (itemsModel.slot >= 6 && itemsModel.slot < 11 && type == 2 && itemsModel.equip == 2)
                    list.Add(itemsModel);
                if (itemsModel.slot >= 11 && itemsModel.slot < 12 && type == 3 && itemsModel.equip == 3)
                    list.Add(itemsModel);
            }
            return list;
        }

        public void setEquipItemFromSlot(int id, int slot)
        {
            switch (slot)
            {
                case 1:
                    this._equep.PRIM = id;
                    break;
                case 2:
                    this._equep.SUB = id;
                    break;
                case 3:
                    this._equep.MELEE = id;
                    break;
                case 4:
                    this._equep.ITEM = id;
                    break;
                case 5:
                    this._equep.THROWING = id;
                    break;
                case 6:
                    this._equep.CHAR_RED = id;
                    break;
                case 7:
                    this._equep.CHAR_BLUE = id;
                    break;
                case 8:
                    this._equep.CHAR_HEAD = id;
                    break;
                case 9: //8
                    this._equep.CHAR_DINO = id;
                    break;
                case 10: //10
                    this._equep.CHAR_ITEM = id;
                    break;
            }
        }

        public int getEquipItemFromSlot(int slot)
        {
            switch (slot)
            {
                case 1:
                    return this._equep.PRIM;
                case 2:
                    return this._equep.SUB;
                case 3:
                    return this._equep.MELEE;
                case 4:
                    return this._equep.ITEM;
                case 5:
                    return this._equep.THROWING;
                case 6:
                    return this._equep.CHAR_RED;
                case 7:
                    return this._equep.CHAR_BLUE;
                case 8:
                    return this._equep.CHAR_HEAD;
                case 9: //9
                    return this._equep.CHAR_DINO;
                case 10: //10
                    return this._equep.CHAR_ITEM;
                default:
                    return 0;
            }
        }
        public bool isItemInventoryExist(int player_id, int itemid)
        {
            foreach (ItemsModel itemsModel in this._inventory.ToArray())
            {
                if (itemsModel.id == itemid)
                    return true;
            }
            return false;
        }
    }
}