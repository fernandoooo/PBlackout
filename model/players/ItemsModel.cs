namespace PBServer.model.players
{
  public class ItemsModel
  {
    public int id;
    public int slot;
    public string name;
    public int equip;
    public int count;
    public int equip_type;

    public ItemsModel()
    {
    }

    public ItemsModel(int _id, int _slot, string name, int _equip, int count, int _equip_type)
    {
      this.id = _id;
      this.name = name;
      this.count = count;
      this.slot = _slot;
      this.equip = _equip;
      this.equip_type = _equip_type;
    }
  }
}
