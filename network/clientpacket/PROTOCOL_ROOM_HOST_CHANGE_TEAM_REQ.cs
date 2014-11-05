using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PBServer.src.model.rooms;
using PBServer.src.model.accounts;
using PBServer.network.serverpackets;

namespace PBServer.network.clientpacket
{
    public class PROTOCOL_ROOM_HOST_CHANGE_TEAM_REQ : ReceiveBaseGamePacket
    {
        //private int _frost;
        public PROTOCOL_ROOM_HOST_CHANGE_TEAM_REQ(GameClient Client, byte[] data)
        {
            this.makeme(Client, data);
        }

        protected internal override void read()
        {
            //_frost = readH();
        }

        protected internal override void run()
        {
            Account p = this.getClient().getPlayer();
            Room room = this.getClient().getPlayer().getRoom();
            if (room != null)
            {
                //foreach (Account account in p.getRoom().getAllPlayers())
                    //account.sendPacket((SendBaseGamePacket)new S_ROOM_CHANGE_TEAM(account, room, 2, room.RED_TEAM.Length, 0));
            }
        }
    }
}
