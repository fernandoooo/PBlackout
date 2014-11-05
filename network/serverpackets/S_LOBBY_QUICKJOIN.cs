using PBServer;
using PBServer.src.model;

namespace PBServer.network.serverpackets
{
    public class S_LOBBY_QUICKJOIN : SendBaseGamePacket
    {
        private Channel _client;
        public S_LOBBY_QUICKJOIN(Channel client)
        {
            this.makeme();
            this._client = client;
        }

        protected internal override void write()
        {
            CLogger.getInstance().info("Recebendo: S_LOBBY_QUICKJOIN");
            if (this._client.getRooms().Count > 0)
            {
                this.writeB(new byte[] {
            8, 11
        });
                this.writeD(1); //Ao definir o valor padrão 0 ele não lhe joga a nenhuma sala, valor maior que 0 ele lhe joga para uma sala nula (ou sejá sem informações)
                this.writeB(new byte[38]);
                this.writeB(new byte[41]);
                this.writeC(0);
                this.writeC(0);
            }
            if (this._client.getRooms().Count == 0)
            {
                this.writeD(0);
            }
        }
    }
}
