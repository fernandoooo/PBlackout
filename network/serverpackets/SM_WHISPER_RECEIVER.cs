// Type: PBServer.network.serverpackets.PROTOCOL_AUTH_RECV_WHISPER_ACK
// Assembly: PBServer, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// Autor: abujavar

using PBServer;

namespace PBServer.network.serverpackets
{
    public class SM_WHISPER_RECEIVER : SendBaseGamePacket
    {
        private string _sender, _msg;
        private int _msglen;

        public SM_WHISPER_RECEIVER(string sender, int msglen, string msg)
        {
            this.makeme();
            _sender = sender;
            _msglen = msglen;
            _msg = msg;
        }

        protected internal override void write()
        {
            this.writeH(0x126);
            this.writeS(_sender, 33);
            //this.writeC((byte)this._msg.Length);
            this.writeH((short)(this._msg.Length + 10)); //RETIRADO WRITE D E TROCADO POR H
            this.writeS(this._msg, this._msg.Length); //EDITED
        }
    }
}
