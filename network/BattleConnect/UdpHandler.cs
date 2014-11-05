using PBServer;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace PBServer.network.BattleConnect
{
  public class UdpHandler
  {
    private List<Account> _players = new List<Account>();
    public static UdpHandler _instance = new UdpHandler();
    private UdpClient _client;

    static UdpHandler()
    {
    }

    public UdpHandler()
    {
      this._client = new UdpClient(59999);
    }

    public static UdpHandler getInstance()
    {
      return UdpHandler._instance;
    }

    public void SendPacket(short opcode, byte[] data)
    {
      MemoryStream memoryStream = new MemoryStream();
      memoryStream.Write(BitConverter.GetBytes(opcode), 0, 2);
      memoryStream.Write(data, 0, data.Length);
      this._client.Send(memoryStream.ToArray(), memoryStream.ToArray().Length, new IPEndPoint(IPAddress.Parse(Config.UDPHost), 60000));
    }

    public void CreateBattleUdpRoom(Room r, int type)
    {
      MemoryStream memoryStream = new MemoryStream();
      memoryStream.WriteByte((byte) 16);
      for (int index = 0; index < 16; ++index)
      {
        memoryStream.WriteByte((byte) index);
        if (r.getSlot(index).state == SLOT_STATE.SLOT_STATE_READY || r.getSlot(index).state == SLOT_STATE.SLOT_STATE_LOAD)
        {
          if (Config.ExternalOrInternalSendIpUdp)
          {
            try
            {
              if (r.getPlayerBySlot(index).customAddress == null)
                memoryStream.Write(IPAddress.Parse(r.getPlayerBySlot(index).getIP()).GetAddressBytes(), 0, 4);
              else
                memoryStream.Write(r.getPlayerBySlot(index).customAddress.GetAddressBytes(), 0, 4);
            }
            catch (Exception ex)
            {
              CLogger.getInstance().warning(ex.ToString());
              memoryStream.Write(r.getPlayerBySlot(index).getLocalAddress(), 0, 4);
            }
          }
          else
            memoryStream.Write(r.getPlayerBySlot(index).getLocalAddress(), 0, 4);
        }
        else
          memoryStream.Write(new byte[4], 0, 4);
      }
      memoryStream.WriteByte((byte) type);
      this.SendPacket((short) 1, memoryStream.ToArray());
    }

    public void PingUdpServer()
    {
      this.SendPacket((short) 9, new byte[4]);
    }

    public void DeleteRoom(IPAddress host)
    {
      MemoryStream memoryStream = new MemoryStream();
      memoryStream.Write(host.GetAddressBytes(), 0, 4);
      this.SendPacket((short) 3, memoryStream.ToArray());
    }

    public void AddPlayerInUdpRoom(Account p, Account host)
    {
      MemoryStream memoryStream = new MemoryStream();
      memoryStream.WriteByte((byte) 0);
      if (Config.ExternalOrInternalSendIpUdp)
        memoryStream.Write(p.publicAdddress(), 0, 4);
      else
        memoryStream.Write(p.getLocalAddress(), 0, 4);
      memoryStream.WriteByte((byte) p.getSlot());
      memoryStream.WriteByte((byte) 0);
      if (Config.ExternalOrInternalSendIpUdp)
        memoryStream.Write(host.publicAdddress(), 0, 4);
      else
        memoryStream.Write(host.getLocalAddress(), 0, 4);
      this.SendPacket((short) 2, memoryStream.ToArray());
    }

    public void RemovePlayerInRoom(Account p)
    {
      MemoryStream memoryStream = new MemoryStream();
      memoryStream.WriteByte((byte) p.getSlot());
      memoryStream.Write(p.publicAdddress(), 0, 4);
      this.SendPacket((short) 4, memoryStream.ToArray());
    }
  }
}
