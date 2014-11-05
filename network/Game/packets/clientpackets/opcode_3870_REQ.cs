using PBServer;
using PBServer.network;
using PBServer.network.Game.packets.serverpackets;
using PBServer.src.model.accounts;
using PBServer.src.managers;
using System;
using System.Net;
using System.Threading;
using System.Xml;

namespace PBServer.network.Game.packets.clientpackets
{
    public class opcode_3870_REQ : ReceiveBaseGamePacket
    {
        public int _zone;
        public int _slot;
        public int x;
        public int y;
        public int z;

        public opcode_3870_REQ(GameClient gc, byte[] buff)
        {
            this.makeme(gc, buff);
        }

        protected internal override void read()
        {
            this.readH();
            this._slot = this.readD();
            this._zone = (int)this.readC();
            this.x = this.readD();
            this.y = this.readD();
            this.z = this.readD();
        }

        protected internal override void run()
        {
            Account player = this.getClient().getPlayer();
            try
            {
                foreach (Account account in player.getRoom().getAllPlayers())
                {
                    if (account.getRoom().bomb != 2) //Caso o time azul não tenha desativado a bomba
                    {
                        account.sendPacket((SendBaseGamePacket)new S_BOMB_INSTALL(this._slot, this._zone, this.x, this.y, this.z, account.getRoom())); //BOMBA FOI PLANTADA
                        Thread.Sleep(42 * 1000); //ESPERAR 42 Segundos para esperar mais 5 segundos

                        Thread.Sleep(5); //ESPERAR 5 Segundos para mostrar o placar

                        account.sendPacket((SendBaseGamePacket)new S_BATTLE_END_ROUND()); //MOSTRAR PLACAR
                    }
                    else
                    {
                        return;
                    }

                    //Thread.Sleep(5);

                    //account.sendPacket((SendBaseGamePacket)new S_START_BATTLE(player.getRoom()));
                }
            }
            catch (ThreadInterruptedException e)
            {
                CLogger.getInstance().error(e.ToString());
            }
        }
    }
}
