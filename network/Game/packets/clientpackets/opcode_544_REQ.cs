using PBServer;
using System.Xml;
using PBServer.network.Game.packets.serverpackets;
using System.IO;

namespace PBServer.network.Game.packets.clientpackets
{
public class opcode_544_REQ : ReceiveBaseGamePacket
{

    public opcode_544_REQ(GameClient gc, byte[] buff)
    {
        base.makeme(gc, buff);
    }

    protected internal override void read()
    {
        
    }

    protected internal override void run()
    {
        this.getClient().sendPacket((SendBaseGamePacket)new opcode_544_ACK());
    }
}
}
