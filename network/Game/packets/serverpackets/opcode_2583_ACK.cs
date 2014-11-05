using PBServer;

namespace PBServer.network.Game.packets.serverpackets
{
public class opcode_2583_ACK : SendBaseLoginPacket
{

    public opcode_2583_ACK()
    {
    }

    protected internal override void write()
    {
        CLogger.getInstance().info("Send: opcode_2583_ACK");
        this.writeH(2583);
        this.writeB(new byte[42]
      {
        (byte) 40,
        (byte) 0,
        (byte) 93,
        (byte) 22,
        (byte) 50,
        (byte) 27,
        (byte) 52,
        (byte) 119,
        (byte) 127,
        (byte) 99,
        (byte) 106,
        (byte) 37,
        (byte) 30,
        (byte) 3,
        (byte) 14,
        (byte) 21,
        (byte) 47,
        (byte) 93,
        (byte) 60,
        (byte) 91,
        (byte) 89,
        (byte) 104,
        (byte) 22,
        (byte) 118,
        (byte) 77,
        (byte) 4,
        (byte) 107,
        (byte) 35,
        (byte) 14,
        (byte) 59,
        (byte) 73,
        (byte) 66,
        (byte) 105,
        (byte) 34,
        (byte) 2,
        (byte) 36,
        (byte) 70,
        (byte) 104,
        (byte) 118,
        (byte) 84,
        (byte) 50,
        (byte) 16
      });
    }
}
}
