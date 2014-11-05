using PBServer;

namespace PBServer.network.Game.packets.serverpackets
{
    public class S_ROOM_CHANGE_PASS : SendBaseGamePacket
    {
        private string _pass;

        public S_ROOM_CHANGE_PASS(string pass)
        {
            this.makeme();
            this._pass = pass;
        }

        protected internal override void write()
        {
            this.writeH((short)3907);
            this.writeS(this._pass, 4);
        }
    }
}
