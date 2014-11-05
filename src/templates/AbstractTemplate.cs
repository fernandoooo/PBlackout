// Type: PBServer.src.templates.AbstractTemplate
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer.src.commons.utils;

namespace PBServer.src.templates
{
  public abstract class AbstractTemplate
  {
    private ParamSet _parameters;

    protected void setParameters(ParamSet set)
    {
      this._parameters = set;
    }

    public ParamSet getParameters()
    {
      return this._parameters;
    }
  }
}
