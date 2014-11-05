using PBServer;
using PBServer.network.serverpackets;
using PBServer.network.Game.packets.serverpackets;
using PBServer.src.managers;
using PBServer.src.model.accounts;
using PBServer.src.data.xml.holders;
using PBServer.src.model;

namespace PBServer.network.clientpacket
{
    internal class PROTOCOL_AUTH_SEND_WHISPER_REQ : ReceiveBaseGamePacket
    {
        private int _messageLength;
        private string _recvrName, _message, _sender;
        public PROTOCOL_AUTH_SEND_WHISPER_REQ(GameClient Client, byte[] data)
        {
            this.makeme(Client, data);
        }

        protected internal override void read()
        {
            _sender = this.getClient().getPlayer().getPlayerName();
            readH();
            _recvrName = readS(33);
            _messageLength = readH();
            _message = readS(_messageLength);
            CLogger.getInstance().info("O jogador " + this.getClient().getPlayer().getPlayerName() + " enviou uma mensagem privada para " + _recvrName);
        }

        protected internal override void run()
        {
            Account pl2 = AccountManager.getInstance().getAccountInName(_recvrName);
            if (pl2 == null)
            {
                this.getClient().sendPacket((SendBaseGamePacket)new SM_WHISPER_CHECK(_recvrName, _messageLength, _message, _sender, int.MaxValue));
            }
            else
            {
                this.getClient().sendPacket((SendBaseGamePacket)new SM_WHISPER_CHECK(_recvrName, _messageLength, _message, _sender, 0));
                pl2.sendPacket((SendBaseGamePacket)new SM_WHISPER_RECEIVER(_sender, _messageLength, _message));
            }
        }
    }
}