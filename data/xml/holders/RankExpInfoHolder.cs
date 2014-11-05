using PBServer;
using PBServer.data.model;
using System.Collections.Generic;

namespace PBServer.data.xml.holders
{
  public class RankExpInfoHolder
  {
    private static RankExpInfoHolder _instance;
    private static List<RankExpModel> _ranks;

    public RankExpInfoHolder()
    {
      RankExpInfoHolder._ranks = new List<RankExpModel>();
    }

    public static RankExpInfoHolder getInstance()
    {
      if (RankExpInfoHolder._instance == null)
        RankExpInfoHolder._instance = new RankExpInfoHolder();
      return RankExpInfoHolder._instance;
    }

    internal void log()
    {
      CLogger.getInstance().info("|[REIH]| Foram carregados " + (object) RankExpInfoHolder._ranks.Count + " modos de upar.");
    }

    public static RankExpModel getRankExpInfo(int remlId)
    {
      return RankExpInfoHolder._ranks[remlId];
    }

    internal void addRankExpInfo(RankExpModel rem)
    {
      RankExpInfoHolder._ranks.Add(rem);
    }

    public List<RankExpModel> getAllRankExpInfos()
    {
      return RankExpInfoHolder._ranks;
    }

    public void clear()
    {
      RankExpInfoHolder._ranks.Clear();
    }
  }
}
