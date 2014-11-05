// Type: PBServer.src.commons.utils.ParamSet
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using System;
using System.Collections.Generic;

namespace PBServer.src.commons.utils
{
  public class ParamSet
  {
    private Dictionary<string, object> _params;

    public ParamSet()
    {
      this._params = new Dictionary<string, object>();
    }

    public void set(string paramName, object value)
    {
      this._params.Add(paramName, value);
    }

    public object getObject(string name)
    {
      return this.getObject(name, false);
    }

    public object getObject(string name, bool remove)
    {
      if (!this._params.ContainsKey(name))
        return (object) null;
      if (remove)
        return (object) (this._params.Remove(name) ? 1 : 0);
      else
        return this._params[name];
    }

    public string getString(string name)
    {
      return this.getString(name, false);
    }

    public string getString(string name, string deflt)
    {
      return this.getString(name, deflt, false);
    }

    public string getString(string name, bool remove)
    {
      object @object = this.getObject(name, remove);
      if (@object == null)
        throw new ArgumentException("String value required, but not specified: " + name);
      else
        return @object.ToString();
    }

    public string getString(string name, string deflt, bool remove)
    {
      object @object = this.getObject(name, remove);
      if (@object == null)
        return deflt;
      else
        return @object.ToString();
    }

    public int getInt32(string name)
    {
      return this.getInt32(name, false);
    }

    public int getInt32(string name, int deflt)
    {
        return this.getInt32(name, deflt, false);
    }

    public int getInt32(string name, bool remove)
    {
      object @object = this.getObject(name, remove);
      if (@object == null)
        throw new ArgumentException("Int32 value required, but not specified: " + name);
      else
        return (int) @object;
    }

    public int getInt32(string name, int deflt, bool remove)
    {
      object @object = this.getObject(name, remove);
      if (@object == null)
        return deflt;
      if (@object.GetType() == typeof (int))
        return (int) @object;
      try
      {
        return int.Parse((string) @object);
      }
      catch (Exception ex)
      {
        throw new ArgumentException("Int32 value required, but found: " + @object);
        throw ex;
      }
    }
  }
}
