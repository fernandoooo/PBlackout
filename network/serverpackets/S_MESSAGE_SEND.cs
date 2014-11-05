using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.CodeDom;
using PBServer.src.model.rooms;
using PBServer.src.model.accounts;
using PBServer.src.commons.utils;

namespace PBServer.network.serverpackets
{
    public class S_MESSAGE_SEND : SendBaseGamePacket
    {
        private string _message;
        private byte[] messageb;
        public S_MESSAGE_SEND(string msg)
        {
            this.makeme();
            this._message = msg;
        }

        protected internal override void write()
        {
            this.messageb = StrToByteArray(_message);
            if (this._message.Length > 0)
            {
                this.writeD(2);
                this.writeC((byte)this._message.Length);
                this.writeB(this.messageb);
            }
        }

        public static byte[] StrToByteArray(string str)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(str);
        }
    }
}
