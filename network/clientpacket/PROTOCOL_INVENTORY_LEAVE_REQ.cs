using PBServer;
using PBServer.network;
using PBServer.network.serverpackets;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;
using PBServer.model.players;
using PBServer.src.managers;

namespace PBServer.network.clientpacket
{
    internal class PROTOCOL_INVENTORY_LEAVE_REQ : ReceiveBaseGamePacket
    {
        private int[] _ids = new int[10];
        private int first;
        private int second;
        private int third;
        private int fourth;
        private int fifth;
        private int red;
        private int blue;
        private int head;
        private int dino;
        private int item;
        private int type;

        public PROTOCOL_INVENTORY_LEAVE_REQ(GameClient Client, byte[] data)
        {
            this.makeme(Client, data);
        }

        protected internal override void read()
        {
            CLogger.getInstance().info("O jogador " + this.getClient().getPlayer().getPlayerName() + " saiu do inventário.");
            int num = (int)this.readH();
            this.type = this.readD();
            if (type == 3)
            { // если 3 - оружие + скины, маски, береты - 40 байт
                CLogger.getInstance().info("Salvando armamentos e personagens...");
                readEquipArmors();
                readEquipWeapons();
            }
            else if (type == 2)
            { // если 2 - оружие - 20 байт
                CLogger.getInstance().info("Salvando armamentos...");
                readEquipWeapons();
            }
            else if (type == 1)
            { // если 1 - скины, маски, береты - 20 байт
                CLogger.getInstance().info("Salvando personagens...");
                readEquipArmors();
            }
        }

        protected internal override void run()
        {
            if (this.getClient() == null)
                return;
            GameClient client = this.getClient();
            Account player = client.getPlayer();
            if (this.type == 1)
            {
                for (int index = 0; index < 5; ++index)
                    player.getInventory().setEquipItemFromSlot(this._ids[index], 6 + index);
            }
            else if (this.type == 2)
            {
                for (int slot = 0; slot < 5; ++slot)
                    player.getInventory().setEquipItemFromSlot(this._ids[slot], slot);
            }
            else
            {
                for (int slot = 0; slot < 10; ++slot)
                    player.getInventory().setEquipItemFromSlot(this._ids[slot], slot);
            }
            if (client.getPlayer() != null && client.getPlayer().getRoom() != null)
                player.getRoom().changeSlotState(player.getSlot(), SLOT_STATE.SLOT_STATE_NORMAL, true);
            player.sendPacket((SendBaseGamePacket)new S_INVENTORY_LEAVE(this.type));
        }

        private void readEquipWeapons()
        {
            first = readD(); // Какая должна быть одета прим пуха
            second = readD(); // Какая должна быть одета саб пуха
            third = readD(); // Какая должна быть одета мили пуха
            fourth = readD(); // Какая должна быть одета тров пуха
            fifth = readD(); // Какая должна быть одета итем пуха
            AccountManager.getInstance().UpdateWeaponItens(this.getClient().getPlayer().getPlayerId(), first, second, third, fourth, fifth);
            this.getClient().getPlayer().setPrimaryWeapon(first);
            this.getClient().getPlayer().setSecondaryWeapon(second);
            this.getClient().getPlayer().setMeleeWeapon(third);
            this.getClient().getPlayer().setThrownNormalWeapon(fourth);
            this.getClient().getPlayer().setThrownSpecialWeapon(fifth);
            this._ids[0] = first;
            this._ids[1] = second;
            this._ids[2] = third;
            this._ids[3] = fourth;
            this._ids[4] = fifth;
        }

        private void readEquipArmors()
        {
            red = readD(); // Скин Мужчина стандартный красные.
            blue = readD(); // Скин Мужчина стандартный синие.
            head = readD(); // Шлем.
            dino = readD(); // Берет.
            item = readD(); // Скин дино.
            AccountManager.getInstance().UpdateCharItens(this.getClient().getPlayer().getPlayerId(), red, blue, head, dino, item);
            this.getClient().getPlayer().setCharRed(red);
            this.getClient().getPlayer().setCharBlue(blue);
            this.getClient().getPlayer().setCharHelmet(head);
            this.getClient().getPlayer().setCharDino(dino);
            this.getClient().getPlayer().setCharBeret(item);
            this._ids[5] = red;
            this._ids[6] = blue;
            this._ids[7] = head;
            this._ids[8] = dino;
            this._ids[9] = item;
        }
    }
}
