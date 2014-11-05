// Type: PBServer.src.templates.WeaponTemplate
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer.src.commons.utils;

namespace PBServer.src.templates
{
  public class WeaponTemplate : ItemTemplate
  {
    public static int _dalnost;
    public static int _razbros;
    public static int _uron;

    public WeaponTemplate(ParamSet set)
    {
      this.setParameters(set);
      this._id = set.getInt32("id");
      this._name = set.getString("name");
      WeaponTemplate._uron = set.getInt32("uron", 30);
      WeaponTemplate._dalnost = set.getInt32("dalnost", 100);
      WeaponTemplate._razbros = set.getInt32("uron", 10);
    }
  }
}
