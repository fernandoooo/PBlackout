using PBServer;

namespace PBServer.src.model.accounts
{
    public class ConfigP
    {
        public int ownerid;
        public string ownername;
        public int mira;
        public int audio1, audio2;
        public int sensibilidade;
        public int visao;
        public int sangue;
        public int mao;
        public int audio_enable;

        public ConfigP()
        {
            ownerid = 0;
            ownername = "";
            mira = 1;
            audio1 = 100;
            audio2 = 100;
            sensibilidade = 50;
            visao = 70;
            sangue = 1;
            mao = 0;
            audio_enable = 7;
        }

        public void setSangue(int primary)
        {
            this.sangue = primary;
        }

        public int getSangue()
        {
            return this.sangue;
        }

        public void setAudioEnable(int primary)
        {
            this.audio_enable = primary;
        }

        public int getAudioEnable()
        {
            return this.audio_enable;
        }

        public void setMao(int primary)
        {
            this.mao = primary;
        }

        public int getMao()
        {
            return this.mao;
        }

        public void setAudio1(int primary)
        {
            this.audio1 = primary;
        }

        public int getAudio1()
        {
            return this.audio1;
        }

        public void setAudio2(int primary)
        {
            this.audio2 = primary;
        }

        public int getAudio2()
        {
            return this.audio2;
        }

        public void setOwnerName(string primary)
        {
            this.ownername = primary;
        }

        public string getOwnerName()
        {
            return this.ownername;
        }

        public void setMira(int primary)
        {
            this.mira = primary;
        }

        public int getMira()
        {
            return this.mira;
        }

        public void setOwnerId(int primary)
        {
            this.ownerid = primary;
        }

        public int getOwnerId()
        {
            return this.ownerid;
        }

        public void setVisao(int primary)
        {
            this.visao = primary;
        }

        public int getVisao()
        {
            return this.visao;
        }

        public void setSensibilidade(int primary)
        {
            this.sensibilidade = primary;
        }

        public int getSensibilidade()
        {
            return this.sensibilidade;
        }
    }
}
