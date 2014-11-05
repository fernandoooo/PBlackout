using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PBServer.src.model.accounts;

namespace PBServer.network.serverpackets
{
    public class PROTOCOL_CUPON_CHANGE_PLAYER_NAME_ACK : SendBaseGamePacket
    {
        public PROTOCOL_CUPON_CHANGE_PLAYER_NAME_ACK()
        {
            this.makeme();
        }

        protected internal override void write()
        {
            CLogger.getInstance().info("Send: PROTOCOL_CUPON_CHANGE_PLAYER_NAME_ACK");
            this.writeH((short)549);
            this.writeD(0);
        }
    }
}
