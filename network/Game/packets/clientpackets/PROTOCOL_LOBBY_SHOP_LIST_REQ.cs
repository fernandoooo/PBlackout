// Type: PBServer.network.Game.packets.clientpackets.PROTOCOL_LOBBY_SHOP_LIST_REQ
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.network;
using PBServer.network.Game.packets.serverpackets;
using PBServer.network.serverpackets;

namespace PBServer.network.Game.packets.clientpackets
{
    public class PROTOCOL_LOBBY_SHOP_LIST_REQ : ReceiveBaseGamePacket
    {
        private int error;
        public PROTOCOL_LOBBY_SHOP_LIST_REQ(GameClient gc, byte[] buff)
        {
            base.makeme(gc, buff);
        }
        protected internal override void run()
        {
            if (this.error == 0)
            {
                this.getClient().sendPacket(new S_SHOP_GET_ITEMS());
                this.getClient().sendPacket(new S_SHOP_GET_GOODS());
                this.getClient().sendPacket(new S_SHOP_TEST_1());
                this.getClient().sendPacket(new S_SHOP_TEST_2());
                this.getClient().sendPacket(new S_SHOP_GET_MATCHING());
            }
            this.getClient().sendPacket(new S_SHOP_LIST());
            //GameClient client = base.getClient();
            //if (client != null)
            //{
                //client.sendPacket(new S_SHOP_GET_MATCHING(1));
                //client.sendPacket(new S_SHOP_GET_MATCHING(2));
                //client.sendPacket(new S_SHOP_GET_GOODS(1));
                //client.sendPacket(new S_SHOP_GET_GOODS(2));
                //client.sendPacket(new S_SHOP_GET_ITEMS());
                //client.sendPacket(new S_SHOP_LIST());
            //}
        }
        protected internal override void read()
        {
            base.readH();
            this.error = base.readD();
        }
    }
}
