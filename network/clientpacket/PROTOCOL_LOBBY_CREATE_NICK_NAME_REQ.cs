using PBServer;
using PBServer.data.model;
using PBServer.data.xml.holders;
using PBServer.model.players;
using PBServer.network;
using PBServer.network.Game.packets.serverpackets;
using PBServer.network.serverpackets;
using PBServer.src.data.xml.parsers;
using PBServer.src.managers;
using PBServer.src.model.accounts;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Versioning;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using PBServer.src.data.xml.holders;

namespace PBServer.network.clientpacket
{
    internal class PROTOCOL_LOBBY_CREATE_NICK_NAME_REQ : ReceiveBaseGamePacket
    {
        private byte name_lenght;
        private string name;
        public PROTOCOL_LOBBY_CREATE_NICK_NAME_REQ(GameClient Client, byte[] data)
        {
            base.makeme(Client, data);
        }
        protected internal override void read()
        {
            readH();
            name_lenght = readC();
            name = readS(name_lenght - 1);
        }
        protected internal override void run()
        {
            GameClient gc = getClient();
            PlayerTemplate pt = PlayerTemplateHolder.getPlayerTemplate(Config.PlayerTemplateId);

            if (!AccountManager.getInstance().isPlayerNameExist(name))
            {
                AccountManager.getInstance().get(getClient().getPlayer().name).setRank(pt._rank);
                AccountManager.getInstance().get(getClient().getPlayer().name).setExp(pt._exp);
                AccountManager.getInstance().get(getClient().getPlayer().name).setGP(pt._gp);
                AccountManager.getInstance().get(getClient().getPlayer().name).setPlayerName(name);
                PlayerInventory pi = new PlayerInventory(getClient().getPlayer().getPlayerId());
                Account acc = AccountManager.getInstance().get(getClient().getPlayer().name);
                int success = AccountManager.getInstance().CreatePlayer(gc.getPlayer().name, acc);
                if (success >= 0)
                {
                    for (int r = 0; r < pt._startInventory.Count; r++)
                    {
                        ItemsModel io = new ItemsModel();
                        io.id = pt._startInventory[r].id;
                        io.slot = pt._startInventory[r].slot;
                        pi.getItemsAll().Add(io);
                    }
                    acc.setInventory(pi);
                    getClient().setAccount(acc.player_id);

                    getClient().sendPacket(new S_LOBBY_CREATE_NICKNAME(0));
                    ChannelInfoHolder.getChannel(getClient().getChannelId()).addPlayer(acc);
                    return;
                }
                else if (success == -1)
                {
                    getClient().sendPacket(new S_LOBBY_CREATE_NICKNAME(0x80000113));
                }
                else
                {
                    getClient().sendPacket(new S_LOBBY_CREATE_NICKNAME(0x80000113));
                }
            }
            else
            {
                getClient().sendPacket(new S_LOBBY_CREATE_NICKNAME(0x80000113));
            }
        }
    }
}
