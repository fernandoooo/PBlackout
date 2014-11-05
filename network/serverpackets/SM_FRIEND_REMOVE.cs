using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PBServer.network.serverpackets
{
    public class SM_FRIEND_REMOVE : SendBaseGamePacket
    {

        public SM_FRIEND_REMOVE()
        {
            this.makeme();
        }

        protected internal override void write()
        {
            CLogger.getInstance().info("Send: PROTOCOL_LOBBY_REMOVE_FRIEND_ACK");
            this.writeH((short)285);
            this.writeB(new byte[] { 0x04, 0x00, 0x1D, 0x01, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x12, 0x01, 0x00 });
        }
    }
}
