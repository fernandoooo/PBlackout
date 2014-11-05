using PBServer;
using PBServer.src.model;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;

namespace PBServer.network.serverpackets
{
    public class S_CHAT_TEAM : SendBaseGamePacket
    {
        protected Chat _chat;
        private Account player;
        public S_CHAT_TEAM(Chat chat, Account player)
        {
            this._chat = chat;
            this.player = player;
            this.makeme();
        }

        protected internal override void write()
        {
            SLOT slot = this.player.getRoom().getSlots()[this.player.getPlayerId()];
            this.writeH(this._chat.chat_type);
            this.writeD(slot.getPlayer().getSlot());
            this.writeD(this._chat.chat.Length);
            this.writeS(this._chat.chat);
        }
    }
}
