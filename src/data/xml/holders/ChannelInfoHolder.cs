// Type: PBServer.src.data.xml.parsers.ChannelInfoHolder
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.src.model;
using System.Collections.Generic;
using System.Collections;
using PBServer.src.data.xml.parsers;

namespace PBServer.src.data.xml.holders
{
    public class ChannelInfoHolder
    {
        private static ChannelInfoHolder _instance;
        private static List<Channel> _servers;
        private static List<Channel> _channels = new List<Channel>();

        public ChannelInfoHolder()
        {
            ChannelInfoHolder._servers = new List<Channel>();
        }

        public static ChannelInfoHolder getInstance()
        {
            if (ChannelInfoHolder._instance == null)
                ChannelInfoHolder._instance = new ChannelInfoHolder();
            return ChannelInfoHolder._instance;
        }

        internal void log()
        {
            CLogger.getInstance().info("|[CIH]| Foram carregados " + (object)ChannelInfoHolder._servers.Count + " canais.");
        }

        public static Channel getChannel(int channelId)
        {
            if (channelId > -1)
                return ChannelInfoHolder._servers[channelId];
            else
                return (Channel)null;
        }

        internal void addChannelInfo(Channel gsi)
        {
            ChannelInfoHolder._servers.Add(gsi);
        }

        public List<Channel> getAllChannelInfos()
        {
            return ChannelInfoHolder._servers;
        }

        public static List<Channel> getAllChannels()
        {
            return _channels;
        }

        public void clear()
        {
            ChannelInfoHolder._servers.Clear();
        }
    }
}
