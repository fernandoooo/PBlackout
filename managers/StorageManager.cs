using PBServer;
using PBServer.src.model;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;
using System.Collections.Generic;
using PBServer.src.managers;

namespace PBServer.managers
{
  public class StorageManager
  {
    private static StorageManager _instance;
    private List<Channel> channels;

    public StorageManager()
    {
      this.channels = new List<Channel>(10);
      CLogger.getInstance().info("[SM] Loaded.");
    }

    public static StorageManager getInstance()
    {
      if (StorageManager._instance == null)
        StorageManager._instance = new StorageManager();
      return StorageManager._instance;
    }

    public List<Room> getRooms(int channel)
    {
      return this.channels[channel].getRooms();
    }

    public Room getRoom(int channel, int id)
    {
      return this.channels[channel].getRooms()[id];
    }

    public List<int> getPlayers(int channel)
    {
      return this.channels[channel].getAllPlayers();
    }

    public void AddPlayerInChannel(int channel, Account p)
    {
      this.channels[channel].addPlayer(p);
    }
  }
}
