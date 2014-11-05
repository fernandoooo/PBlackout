using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PBServer;
using PBServer.network;
using PBServer.network.Game.packets.serverpackets;

namespace PBServer.network.Game.packets.clientpackets
{
    public class opcode_417_REQ : ReceiveBaseGamePacket
    {

        private int name_lenght;
        private string name;
        private int text_lenght;
        private string text;

        public opcode_417_REQ(GameClient Client, byte[] data)
        {
            this.makeme(Client, data);
        }

        protected internal override void read()
        {
            readH();
            name_lenght = readC();
            text_lenght = readC();
            name = readS(name_lenght);
            text = readS(text_lenght); //FIXED
            CLogger.getInstance().info("Destinatario: " + name);
            CLogger.getInstance().info("Messagem: " + text);
        }

        protected internal override void run()
        {
            this.getClient().sendPacket((SendBaseGamePacket)new opcode_417_ACK()); //TEST
        }
    }
}
