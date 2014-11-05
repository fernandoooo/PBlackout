// Type: PBServer.network.serverpackets.PROTOCOL_AUTH_SEND_WHISPER_ACK
// Assembly: PBServer, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// Autor: abujavar

using PBServer;
using PBServer.src.data.xml.parsers;
using PBServer.src.managers;
using PBServer.src.model.accounts;
using PBServer.src.model;
using PBServer.network.clientpacket;
using System;
using System.Collections;
using PBServer.managers;
using System.Collections.Generic;
using PBServer.src.data.xml.holders;

namespace PBServer.network.serverpackets
{
    public class SM_WHISPER_CHECK : SendBaseGamePacket
    {
        private int _msglen;
        private string _name;
        private string _msg;
        private string _sender;
        private int _error;
        public SM_WHISPER_CHECK(string name, int msglen, string msg, string sender, int error)
        {
            this.makeme();
            this._name = name;
            this._msglen = msglen;
            this._msg = msg;
            this._sender = sender;
            this._error = error;
        }

        protected internal override void write()
        {
            this.writeH((short)291);
            if (this._error == 0)
            {
                this.writeD(0);
                this.writeS(this._name, 33);
                this.writeH((short)this._msglen);
                this.writeS(this._msg, this._msglen);
            }
            else
            {
                this.writeD(this._error); //error
            }
        }
    }
}
