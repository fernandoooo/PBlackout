using PBServer;
using PBServer.network;
using PBServer.network.Game.packets.serverpackets;
using PBServer.model.clans;
using PBServer.managers;

namespace PBServer.network.Game.packets.clientpackets
{
    public class opcode_1447_REQ : ReceiveBaseGamePacket
    {
        private string clanName;
        public opcode_1447_REQ(GameClient gc, byte[] buff)
        {
            this.makeme(gc, buff);
        }

        protected internal override void read()
        {
            this.readH();
            this.clanName = this.readS(this.readC());//название клана для его создания
        }

        protected internal override void run()
        {
            if (ClanManager.getInstance().getClanForName(this.clanName) == null)
            {
                this.getClient().sendPacket((SendBaseGamePacket)new S_CLAN_CHECK_NAME(0));
            }
            else
            {
                this.getClient().sendPacket((SendBaseGamePacket)new S_CLAN_CHECK_NAME(int.MaxValue));
            }
        }
    }
}
