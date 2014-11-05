using PBServer;
using PBServer.network;
using PBServer.network.serverpackets;

namespace PBServer.network.clientpacket
{
  internal class PROTOCOL_BASE_ENTER_CHANNELSELECT_REQ : ReceiveBaseGamePacket
  {
    public PROTOCOL_BASE_ENTER_CHANNELSELECT_REQ(GameClient Client, byte[] data)
    {
      this.makeme(Client, data);
    }

    protected internal override void read()
    {
    }

    protected internal override void run()
    {
        CLogger.getInstance().extra_info("IP do jogador: " + this.getClient().getIPString());
        CLogger.getInstance().info("O jogador " + this.getClient().getPlayer().getPlayerName() + " escolheu um canal.");
      if (this.getClient() == null)
        return;
      this.getClient().sendPacket((SendBaseGamePacket) new S_BASE_ENTER_CHANNELSELECT());
    }
  }
}
