using PBServer;
using PBServer.network;
using PBServer.network.serverpackets;
using PBServer.src.managers;
using System;
using System.Net;

namespace PBServer.network.clientpacket
{
internal class PROTOCOL_BASE_USER_TITLE_DETACH_REQ : ReceiveBaseGamePacket
{
    private int _slot;
    public PROTOCOL_BASE_USER_TITLE_DETACH_REQ(GameClient Client, byte[] data)
    {
        this.makeme(Client, data);
    }

    protected internal override void read()
    {
        readH();
        _slot = readH();
    }

    protected internal override void run()
    {
        CLogger.getInstance().info("O jogador " + this.getClient().getPlayer().getPlayerName().ToString() + " desequipou um título.");
        if (this.getClient() == null)
        return;
        this.getClient().sendPacket((SendBaseGamePacket)new SM_TITLE_DETACH(_slot));
        }
    }
}


