namespace PBServer.data.model
{
  public class RankExpModel
  {
    public int _onNextLevel = 0;
    public int _rank = 0;
    public int _onGPUp = 0;
    public int _itemid = 0;
    public int _onAllExp = 0;
    public string _name;

    public RankExpModel(string name, int rank, int onNextLevel, int onGPUp, int itemid, int onAllExp)
    {
      this._name = name;
      this._rank = rank;
      this._onNextLevel = onNextLevel;
      this._onGPUp = onGPUp;
      this._itemid = itemid;
      this._onAllExp = onAllExp;
    }
  }
}
