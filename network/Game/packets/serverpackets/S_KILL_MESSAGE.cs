// Type: PBServer.network.Game.packets.serverpackets.opcode_3854_ACK
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using PBServer.model;
using PBServer.model.ENUMS;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;

namespace PBServer.network.Game.packets.serverpackets
{
    public class S_KILL_MESSAGE : SendBaseGamePacket
    {
        private Room _room;
        private FragInfos _kills;
        private Account _player;

        public S_KILL_MESSAGE(Account p, FragInfos kills)
        {
            this._room = p.getRoom();
            this._kills = kills;
            this._player = p;
            this.makeme();
        }

        protected internal override void write()
        {
            this.writeH((short)3854);
            this.writeC((byte)this._kills.unkCount);
            this.writeC((byte)this._kills.killsCount);
            this.writeC((byte)this._kills.killer);
            this.writeD(this._kills.weapon);
            this.writeB(this._kills.bytes13);
            SLOT slot1 = this._player.getRoom().getSlots()[this._kills.killer];
            this.writeC((byte)this._kills.unk_c_1);
            this.writeC((byte)this._kills.unk_c_2);
            switch (this._player.getRoom().getSlot(this._player.getSlot()).killMessage)
            {
                case 0:
                    slot1.killsOnLife += 1;
                    this.writeH((short)0); //KILLING_MESSAGE_NONE
                    break;
                case 1:
                    this.writeH((short)1); //KILLING_MESSAGE_PIERCINGSHOT
                    break;
                case 2:
                    this.writeH((short)2); //KILLING_MESSAGE_MASSKILL
                    break;
                case 3:
                    this.writeH((short)4); //KILLING_MESSAGE_CHAINSTOPPER
                    break;
                case 4:
                    this.writeH((short)8); //KILLING_MESSAGE_HEADSHOT
                    break;
                case 5:
                    this.writeH((short)16); //KILLING_MESSAGE_CHAINHEADSHOT
                    break;
                case 6:
                    this.writeH((short)32); //KILLING_MESSAGE_CHAINSLUGGER
                    break;
                case 7:
                    this.writeH((short)64); //KILLING_MESSAGE_SUICIDE
                    break;
                case 8:
                    this.writeH((short)128); //KILLING_MESSAGE_OBJECTDEFENCE
                    break;
                default:
                    this.writeH((short)0); //KILLING_MESSAGE_NONE
                    break;
            }
            this.writeB(this._kills.bytes13);
            this.writeH((short)this._room.getKills(TeamEnum.CHARACTER_TEAM_RED));
            this.writeH((short)this._room.getDeaths(TeamEnum.CHARACTER_TEAM_RED));
            this.writeH((short)this._room.getKills(TeamEnum.CHARACTER_TEAM_BLUE));
            this.writeH((short)this._room.getDeaths(TeamEnum.CHARACTER_TEAM_BLUE));
            foreach (SLOT slot in this._room.getSlots())
            {
                this.writeH((short)slot.allKills);
                this.writeH((short)slot.allDeaths);
            }
            if (slot1.killsOnLife == 1)
            {
                this.writeC(0);
            }
            if (slot1.killsOnLife == 2)
            {
                this.writeC(1);
            }
            if (slot1.killsOnLife == 3)
            {
                this.writeC(2);
            }
            if (slot1.killsOnLife > 3)
            {
                this.writeC(3);
            }

            this.writeH((short)this._room.getSlot(this._kills.killer).botScore);
            int hz = 0;
            if (hz > 0)
            {
                writeD(0);
            }
        }
    }
}
