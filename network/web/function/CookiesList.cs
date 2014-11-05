// Type: PBServer.network.web.function.CookiesList
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

namespace PBServer.network.web.function
{
  public class CookiesList
  {
    private string _login = "";
    private string _value = "";

    public void setLogin(string login)
    {
      this._login = login;
    }

    public void setValue(string value)
    {
      this._value = value;
    }

    public string getName()
    {
      return this._login;
    }

    public string getValue()
    {
      return this._value;
    }
  }
}
