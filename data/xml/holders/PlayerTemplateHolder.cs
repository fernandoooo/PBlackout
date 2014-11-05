using PBServer;
using PBServer.data.model;
using System.Collections.Generic;

namespace PBServer.data.xml.holders
{
  public class PlayerTemplateHolder
  {
    private static PlayerTemplateHolder _instance;
    private static List<PlayerTemplate> _templates;

    public PlayerTemplateHolder()
    {
      PlayerTemplateHolder._templates = new List<PlayerTemplate>();
    }

    public static PlayerTemplateHolder getInstance()
    {
      if (PlayerTemplateHolder._instance == null)
        PlayerTemplateHolder._instance = new PlayerTemplateHolder();
      return PlayerTemplateHolder._instance;
    }

    internal void log()
    {
        CLogger.getInstance().info("|[PTH]| Foram carregados " + (object)PlayerTemplateHolder._templates.Count + " templates de jogadores.");
    }

    public static PlayerTemplate getPlayerTemplate(int templateId)
    {
      if (templateId > -1)
        return PlayerTemplateHolder._templates[templateId - 1];
      else
        return (PlayerTemplate) null;
    }

    internal void addPlayerTemplateInfo(PlayerTemplate gsi)
    {
      PlayerTemplateHolder._templates.Add(gsi);
    }

    public List<PlayerTemplate> getAllPlayerTemplateInfos()
    {
      return PlayerTemplateHolder._templates;
    }

    public void clear()
    {
      PlayerTemplateHolder._templates.Clear();
    }
  }
}
