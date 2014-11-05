using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PBServer.network.Game.packets.serverpackets
{
    public class S_RINVITE_FROM_ROOM : SendBaseGamePacket
    {

        public S_RINVITE_FROM_ROOM()
        {
            this.makeme();
        }

        protected internal override void write()
        {
            this.writeH((short)3604);
            this.writeD(0);
        }
    }
}
