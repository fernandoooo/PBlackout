namespace PBServer.model.players
{
    public class SItemsModel
    {
        public int id;
        public int slot;
        public string name;
        public int equip;

        public SItemsModel()
        {
        }

        public SItemsModel(int _id, int _slot, string name, int _equip)
        {
            this.id = _id;
            this.name = name;
            this.slot = _slot;
            this.equip = _equip;
        }
    }
}
