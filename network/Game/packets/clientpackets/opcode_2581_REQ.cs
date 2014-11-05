using PBServer;
using PBServer.src.managers;

namespace PBServer.network.Game.packets.clientpackets
{
    public class opcode_2581_REQ : ReceiveBaseGamePacket
    {

        private int sound;
        private int music;
        private int blood;
        private int mira;
        private int mao;
        private int visao;
        private int sensibilidade;
        private int audio_enable;
        public opcode_2581_REQ(GameClient gc, byte[] buff)
        {
            this.makeme(gc, buff);
        }

        protected internal override void read()
        {
            this.readB(6);
            this.blood = this.readC();
            this.readC();
            this.mira = this.readC();
            this.mao = this.readC();
            this.readB(4);
            this.audio_enable = this.readC();
            this.readB(5);
            this.sound = this.readC();
            this.music = this.readC();
            this.visao = this.readC();
            this.readC();
            this.sensibilidade = readC();
        }

        protected internal override void run()
        {
            ConfigManager.getInstance().UpdateConfig(this.getClient().getPlayerId(), this.sound, this.music, this.mira, this.sensibilidade, this.visao, this.blood, this.mao, this.audio_enable);
            CLogger.getInstance().extra_info("As configurações do jogador " + this.getClient().getPlayer().getPlayerName() + " foram salvas com sucesso.");
        }
    }
}
