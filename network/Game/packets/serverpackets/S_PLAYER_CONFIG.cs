using PBServer;
using System;
using System.Globalization;
using PBServer.src.model.accounts;
using PBServer.src.managers;

namespace PBServer.network.Game.packets.serverpackets
{

    public class S_PLAYER_CONFIG : SendBaseLoginPacket
    {
        private LoginClient _lc;
        public S_PLAYER_CONFIG(LoginClient player)
        {
            this.makeme();
            this._lc = player;
        }

        protected internal override void write()
        {
            this.writeH(2568);
            Account account = AccountManager.getInstance().get(this._lc.getLogin());
            this.writeD(1); // Если значение больше или равно 0 то читает дальше
            this.writeC(2);
            this.writeB(new byte[] { 
                Convert.ToByte(account.sangue), //Blood
                0,
                Convert.ToByte(account.mira), //Tipo de mira? //0 - tipo1 | 1 - tipo2 | 2 - tipo3 | 3 -tipo4
                Convert.ToByte(account.mao), //Mão da arma? //0 - Destro | 1 - Canhoto
                55,
                0,
                0,
                0,
                Convert.ToByte(account.audio_enable), //Aba Aúdio
                0,
                0,
                0,
                0,
                0,
                Convert.ToByte(account.audio1),
                Convert.ToByte(account.audio2),
                Convert.ToByte(account.visao),
                0,
                Convert.ToByte(account.sensibilidade),
                0,
                0,
                0,
                0,
                0,
                31,
                0,
                0,
                0,
                0,
                57,
                (byte) 248, 16, 0, 0, 0x0a, 0, 0, 0, 0, 0x0d, 0, 0, 0, 0, 0x20, 0, 0, 0, 0, 0x1c, 0, 0, 0, 0, 0x2c, 0, 0, 0, 0, 0x28, 0, 0, 0, 0, 0x26, 0, 0, 0, 0, 0x0f, 0, 0, 0, 1, 1, 0, 0, 0, 1, 0x02, 0, 0, 0, 0, 0x1b, 0, 0, 0, 0, 0x1d, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0x02, 0, 0, 0, 0, 0x03, 0, 0, 0, 0, 0x04, 0, 0, 0, 0, 0x05, 0, 0, 0, 0, 0x06, 0, 0, 0, 0, 0x1a, 0, 0, 0, 1, 0, 0, 0, 0x10, 1, 0, 0, 0, 0x20, 0, 0x10, 0, 0, 0, 0, 0x37, 0, 0, 0, 0, 0x16, 0, 0, 0, 0, 0x5c, 0, 0, 0, 0, 0x5b, 0, 0, 0, 0, 0x25, 0, 0, 0, 0, 0x40, 0, 0, 0, 0, 0x41, 0, 0, 0, 0, 0x15, 0, 0, 0, 0, 0x1f, 0, 0, 0, 0, 0x23, 0, 0, 0, 0, 0x21, 0, 0, 0, 0, 0x0c, 0, 0, 0, 0, 0x0e, 0, 0, 0, 0, 0x31, 0, 0, 0, 0, 0x32, 0, 0, 0, 0, 0x46, 0, 0, 0, 0, 0x42, 0, 0, 0, 0, 0x0b, 0, 0, 0, 0, 0x3a, 0, 0, 0, 0, (byte)255, (byte)255, (byte)255, (byte)255, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0 });
        }
    }
}