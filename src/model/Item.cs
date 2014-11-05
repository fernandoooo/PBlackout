// Type: PBServer.src.model.Item
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer.src.templates;

namespace PBServer.src.model
{
  public abstract class Item
  {
    private ItemTemplate _template;

    public Item(ItemTemplate template)
    {
      this._template = template;
    }

    public ItemTemplate getTemplate()
    {
      return this._template;
    }
  }
}
