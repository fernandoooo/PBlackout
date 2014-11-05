// Type: PBServer.src.templates.ArmorTemplate
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer.src.commons.utils;

namespace PBServer.src.templates
{
  public class ArmorTemplate : ItemTemplate
  {
    public static int _uron;
    public static int _dalnost;
    public static int _razbros;

    public ArmorTemplate(ParamSet set)
    {
      this.setParameters(set);
      this._id = set.getInt32("id");
      this._name = set.getString("name");
    }
  }
}
