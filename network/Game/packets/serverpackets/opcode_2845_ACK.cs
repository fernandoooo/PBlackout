using PBServer;

namespace PBServer.network.Game.packets.serverpackets
{
    public class opcode_2845_ACK : SendBaseGamePacket
    {

        public opcode_2845_ACK()
        {
            this.makeme();
        }

        protected internal override void write()
        {
            CLogger.getInstance().info("Send: opcode_2845_ACK");
            this.writeH((short)2845);
            this.writeD(200004006); //PRIMARY
            this.writeD(601002003); //SECONDARY
            this.writeD(702001001); //MELEE
            this.writeD(803007001); //THROWN_NORMAL
            this.writeD(904007002); //THROWN_SPECIAL
            this.writeD(1001001005); //RED_TIME
            this.writeD(1001002006); //BLUE_TIME
            this.writeD(1102003001); //HELMET
            this.writeD(0); //BERET
            this.writeD(1006001024); //DINO
        }
    }
}
