using PBServer;
using PBServer.src.model.accounts;
using PBServer.model.clans;
using PBServer.managers;

namespace PBServer.network.Game.packets.serverpackets
{
 /**
 * Пакет информации о клане
 * 
 * @author DarkSkeleton
 * @version 0.01 - (Italia)
 */
    public class opcode_519_ACK : SendBaseGamePacket
    {

        public opcode_519_ACK()
        {
            this.makeme();
        }

        protected internal override void write()
        {
            this.writeH(519);
            this.writeD(0);
            this.writeC(11);
            this.writeC(1);
            this.writeH(0);
            this.writeS("pb", 16);
            this.writeC(0);
            this.writeB(new byte[29]);
            this.writeS("abbb", 33);
            this.writeS("CLAN TEST POINT BLANK", 255);
            this.writeB(new byte[389]);
        }
    }
}