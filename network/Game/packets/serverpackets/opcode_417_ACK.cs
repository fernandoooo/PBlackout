using PBServer;
using PBServer.src.model.accounts;
using PBServer.model.clans;
using PBServer.managers;

namespace PBServer.network.Game.packets.serverpackets
{
    /**
    * Пакет информации о клане
    * 
    * @author FumaPraQue
    * @version 0.01 - (Brasil)
    */
    public class opcode_417_ACK : SendBaseGamePacket
    {

        public opcode_417_ACK()
        {
            this.makeme();
        }

        protected internal override void write()
        {
            this.writeH(418);
            this.writeD(0); //Mensagem de erro
        }
    }
}
