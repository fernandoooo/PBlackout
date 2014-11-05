using PBServer;
using PBServer.network;
using PBServer.network.serverpackets;
using PBServer.src.managers;
using System;
using System.Net;
using PBServer.src.model.accounts;

namespace PBServer.network.clientpacket
{
    public class PROTOCOL_BASE_USER_TITLE_GET_REQ : ReceiveBaseGamePacket
    {
        private int _titleIdx;
        public PROTOCOL_BASE_USER_TITLE_GET_REQ(GameClient Client, byte[] data)
        {
            this.makeme(Client, data);
        }

        protected internal override void read()
        {
            this.readH();
            this._titleIdx = readB(1)[0];
        }

        protected internal override void run()
        {
            int errNo = int.MaxValue;
            int openedSlot = 1;

            Account player = this.getClient().getPlayer();

            if (player != null)
            {
                int rank = player.getRank();

                switch (this._titleIdx)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        if (rank >= 1)
                        {
                            openedSlot = 1;
                            errNo = 0;
                        }
                        break;

                    case 5:
                    case 6:
                    case 7:
                        if (rank >= 5)
                        {
                            openedSlot = 2;
                            errNo = 0;
                        }
                        break;

                    case 8:
                    case 26:
                    case 14:
                    case 30:
                    case 20:
                    case 40:
                    case 35:
                        if (rank >= 8)
                        {
                            openedSlot = 2;
                            errNo = 0;
                        }
                        break;

                    case 9:
                    case 27:
                    case 15:
                    case 31:
                    case 21:
                    case 41:
                    case 36:
                        if (rank >= 12)
                        {
                            openedSlot = 2;
                            errNo = 0;
                        }
                        break;

                    case 10:
                    case 28:
                    case 16:
                    case 32:
                    case 22:
                    case 42:
                    case 37:
                        if (rank >= 17)
                        {
                            openedSlot = 2;
                            errNo = 0;
                        }
                        break;

                    case 11:
                    case 17:
                    case 23:
                    case 43:
                        if (rank >= 21)
                        {
                            openedSlot = 3;
                            errNo = 0;
                        }
                        break;

                    case 12:
                    case 18:
                    case 33:
                    case 24:
                    case 38:
                        if (rank >= 26)
                        {
                            openedSlot = 3;
                            errNo = 0;
                        }
                        break;

                    case 13:
                    case 29:
                    case 19:
                    case 34:
                    case 25:
                    case 44:
                    case 39:
                        if (rank >= 31)
                        {
                            openedSlot = 3;
                            errNo = 0;
                        }
                        break;
                }
            }

            this.getClient().sendPacket((SendBaseGamePacket)new SM_TITLE_GET(errNo, openedSlot));

            if (errNo != 0)
            {
                this.getClient().sendPacket((SendBaseGamePacket)new S_MESSAGE_SEND("Title aquisiton failed!\nMake sure you have met the title aquisiton requirements."));
            }
            CLogger.getInstance().info("O jogador " + this.getClient().getPlayer().getPlayerName().ToString() + " adquiriu um título.");
        }
    }
}
