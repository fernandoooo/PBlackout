using PBServer;
using PBServer.data.model;
using System.Collections.Generic;

namespace PBServer.data.xml.holders
{
  public class StartedInventoryItemsHolder
  {
    private static StartedInventoryItemsHolder _instance;
    private static List<PlayerTemplateInventory> _templates;

    public StartedInventoryItemsHolder()
    {
      StartedInventoryItemsHolder._templates = new List<PlayerTemplateInventory>();
    }

    public static StartedInventoryItemsHolder getInstance()
    {
      if (StartedInventoryItemsHolder._instance == null)
        StartedInventoryItemsHolder._instance = new StartedInventoryItemsHolder();
      return StartedInventoryItemsHolder._instance;
    }

    public List<PlayerTemplateInventory> getPlayerInventoryStatic()
    {
        return StartedInventoryItemsHolder._templates;
    }

    internal void log()
    {
      CLogger.getInstance().info("|[SIIH]| Foram carregados " + (object) StartedInventoryItemsHolder._templates.Count + " itens iniciais.");
    }

    public void clear()
    {
      StartedInventoryItemsHolder._templates.Clear();
    }

    public void addInventoryStatic(PlayerTemplateInventory playerTemplate)
    {
      StartedInventoryItemsHolder._templates.Add(playerTemplate);
    }
  }
}
