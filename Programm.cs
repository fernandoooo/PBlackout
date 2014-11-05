using PBServer.data.xml.parsers;
using PBServer.managers;
using PBServer.model;
using PBServer.network.BattleConnect;
using PBServer.network.web;
using PBServer.src.data.xml.parsers;
using PBServer.src.managers;
using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;
using PBServer.threading;

namespace PBServer
{
  public class Programm
  {
    private static void Main(string[] args)
    {
        Console.Title = "Project Blackout Server";
        CLogger.getInstance().form();
        Console.ForegroundColor = ConsoleColor.Green;
        CLogger.getInstance().info("-----------------------------");
        CLogger.getInstance().info("|   Servidor Project Wars   |");
        CLogger.getInstance().info("|         PVP 4X4           |");
        CLogger.getInstance().info("|         FIX 88%           |");
        CLogger.getInstance().info("|  developer by Skelleton   |");
        CLogger.getInstance().info("|      SystemPBlackout      |");
        CLogger.getInstance().info("|     Version: " + ((object)ServerVersion.version).ToString() + "     |");
        CLogger.getInstance().info("|       New Battle          |");
        CLogger.getInstance().info("|   Creation By Skelleton   |");
        CLogger.getInstance().info("-----------------------------");
        Console.ForegroundColor = ConsoleColor.White;
        Config.load();
        StartedInventoryItemsParser.getInstance();
        UdpHandler.getInstance().SendPacket((short)byte.MaxValue, new byte[4]);
        GameServerInfoParser.getInstance();
        ChannelInfoParser.getInstance();
        ClanManager.getInstance();
        AccountManager.getInstance();
        ConfigManager.getInstance();
        FriendManager.getInstance();
        RankExpInfoParser.getInstance();
        ShopInfoManager.getInstance();
        MissionManager.getInstance();
        PlayerTemplateParser.getInstance();
        LoginClientManager.getInstance();
        GameClientManager.getInstance();
        NetworkLoginFactory.getInstance();
        NetworkGameFactory.getInstance();
        CLogger.getInstance().warning("Servidor aberto");
        CLogger.getInstance().warning("Servidor aberto");
        CLogger.getInstance().warning("Servidor aberto");
        CLogger.getInstance().warning("Servidor aberto");
        Process.GetCurrentProcess().WaitForExit();
    }
  }
}
