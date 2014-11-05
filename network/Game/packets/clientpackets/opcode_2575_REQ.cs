using PBServer;
using PBServer.network.Game.packets.serverpackets;
using PBServer.src.model;
using PBServer.src.data.xml.holders;

namespace PBServer.network.Game.packets.clientpackets
{
    public class opcode_2575_REQ : ReceiveBaseGamePacket
    {
        public opcode_2575_REQ(GameClient gc, byte[] buff)
        {
            this.makeme(gc, buff);
        }

        protected internal override void read()
        {
            this.readH();
        }

        protected internal override void run()
        {
            if (this.getClient() == null)
                return;
            Channel channel = ChannelInfoHolder.getChannel(this.getClient().getChannelId());
            channel.RemoveEmptyRooms();
            this.getClient().sendPacket((SendBaseGamePacket)new S_UPDATE_ROOMLIST());
        }
    }
}
