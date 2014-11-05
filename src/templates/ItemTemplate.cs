// Type: PBServer.src.templates.ItemTemplate
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

namespace PBServer.src.templates
{
  public class ItemTemplate : AbstractTemplate
  {
    protected int _id;
    protected int _type2;
    protected string _name;
    protected int _price;
    protected int _price2;
    protected int _type;
    protected int _noname;
    protected int _new;
    public int _count
    {
        get;
        set;
    }
    public int _buy_type
    {
        get;
        set;
    }

    public int getItemId()
    {
      return this._id;
    }

    public int getItemNew()
    {
        return this._new;
    }

    public string getItemName()
    {
      return this._name;
    }

    public int getItemCount()
    {
        return this._count;
    }

    public int getItemPrice()
    {
        return this._price;
    }

    public int getItemPrice2()
    {
        return this._price2;
    }

    public int getItemType()
    {
        return this._type;
    }
    public int getBuyType2()
    {
        return this._type2;
    }
    public int getBuyType()
    {
        return this._buy_type;
    }

    public int getNoName()
    {
        return this._noname;
    }
  }
}
