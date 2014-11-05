using PBServer;
using PBServer.model;
using PBServer.model.ENUMS;
using PBServer.network;
using PBServer.network.Game.packets.serverpackets;
using PBServer.src.data.xml.parsers;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;
using System;
using System.Collections.Generic;
using System.Data;
using PBServer.src.data.xml.holders;

namespace PBServer.network.Game.packets.clientpackets
{
    public class opcode_3853_REQ : ReceiveBaseGamePacket
    {
        private FragInfos _kills;

        public opcode_3853_REQ(GameClient gc, byte[] buff)
        {
            this.makeme(gc, buff);
        }

        protected internal override void read()
        {
            this._kills = new FragInfos();
            base.readH();
            this._kills.unkCount = (int)base.readC();
            this._kills.killsCount = (int)base.readC();
            this._kills.killer = (int)base.readC();
            this._kills.weapon = base.readD();
            this._kills.bytes13 = base.readB(13);
            this._kills.unk_c_1 = (int)base.readC();
            this._kills.unk_c_2 = (int)base.readC();
            this._kills.unk_c_3 = (int)base.readC();
            this._kills.unk_c_4 = (int)base.readC();
            this._kills.bytes131 = base.readB(13);
        }

        protected internal override void run()
        {
            Account player = this.getClient().getPlayer();
            if (player == null)
                return;
            Room room = player.getRoom();
            if (room == null)
                return;
            SLOT slot1 = room.getSlots()[this._kills.killer];
            int num1 = this._kills.weapon / 100000;
            slot1.killMessage = 0;
            if (this._kills.killsCount > 1)
            {
                int num2 = num1 == 8030 ? 0 : (num1 != 9030 ? 1 : 0);
                slot1.killMessage = num2 != 0 ? 1 : 2;
                slot1.killsOnLife += this._kills.killsCount;
                SLOT slot2 = room.getSlots()[this._kills.getDeatSlot()];
                ++slot2.allDeaths;
                slot2.killMessage = 0;
                slot2.lastKillState = 0;
                slot2.resetkillsOnLife();
                slot2.repeatLastState = false;
                if (this._kills.killer != this._kills.getDeatSlot())
                {
                    slot1.allKills += this._kills.killsCount;
                }
                if (this._kills.getDeatSlot() % 2 == 0)
                {
                    room.addDeaths(TeamEnum.CHARACTER_TEAM_RED);
                    room.addKills(TeamEnum.CHARACTER_TEAM_BLUE);
                }
                else
                {
                    room.addDeaths(TeamEnum.CHARACTER_TEAM_BLUE);
                    room.addKills(TeamEnum.CHARACTER_TEAM_RED);
                }
                if (room.special == 6)
                {
                    room.getSlot(this._kills.killer).botScore += 5 + room.getSlot(this._kills.killer).killsOnLife * room._aiLevel;
                }
                this.getClient().sendPacket((SendBaseGamePacket)new S_KILL_MESSAGE(this.getClient().getPlayer(), this._kills));
            }
            else
            {
                int num2 = 0;
                if (this._kills.unk_c_2 >> 4 == 3)
                    num2 = 4;
                else if (this._kills.unk_c_2 >> 4 == 1 && this._kills.unk_c_2 % 2 == 1 && num1 == 7020)
                    num2 = 6;
                if (num2 > 0)
                {
                    int num3 = slot1.lastKillState >> 12;
                    if (num2 == 4)
                    {
                        if (num3 != 4)
                        {
                            slot1.repeatLastState = false;
                        }
                        slot1.killsOnLife += this._kills.killsCount;
                        slot1.lastKillState = num2 << 12 | slot1.killsOnLife;
                        int num4 = slot1.lastKillState & 15;
                        if (slot1.repeatLastState)
                        {
                            slot1.killMessage = num4 <= 1 ? 4 : 5;
                        }
                        else
                        {
                            slot1.killMessage = 4;
                            slot1.repeatLastState = true;
                        }
                    }
                    else if (num2 == 6)
                    {
                        if (num3 != 6)
                        {
                            slot1.repeatLastState = false;
                            slot1.resetkillsOnLife();
                        }
                        slot1.killsOnLife += this._kills.killsCount;
                        slot1.lastKillState = num2 << 12 | slot1.killsOnLife;
                        int num4 = slot1.lastKillState & 15;
                        if (slot1.repeatLastState)
                        {
                            if (num4 > 1)
                                slot1.killMessage = 6;
                        }
                        else
                            slot1.repeatLastState = true;
                    }
                    else if (num2 == 0)
                    {
                        //if (slot1.killsOnLife == 1)
                            //slot1.killMessage = 3;
                        //else if (slot1.killsOnLife == 2)
                            //slot1.killMessage = 3;
                    }
                }
                else
                {
                    slot1.lastKillState = 0;
                    slot1.repeatLastState = false;
                }
                SLOT slot2 = room.getSlots()[this._kills.getDeatSlot()];
                ++slot2.allDeaths;
                slot2.killMessage = 0;
                slot2.lastKillState = 0;
                slot2.resetkillsOnLife();
                slot2.repeatLastState = false;
                if (this._kills.killer != this._kills.getDeatSlot())
                    slot1.allKills += this._kills.killsCount;
                if (this._kills.getDeatSlot() % 2 == 0)
                {
                    room.addDeaths(TeamEnum.CHARACTER_TEAM_RED);
                    room.addKills(TeamEnum.CHARACTER_TEAM_BLUE);
                }
                else
                {
                    room.addDeaths(TeamEnum.CHARACTER_TEAM_BLUE);
                    room.addKills(TeamEnum.CHARACTER_TEAM_RED);
                }
                if (room.special == 6)
                    room.getSlot(this._kills.killer).botScore += 5 + room.getSlot(this._kills.killer).killsOnLife * room._aiLevel;
            }
            for (int id = 0; id < room.getSlots().Length; ++id)
            {
                int playerId = room.getSlot(id)._playerId;
                if (playerId > 0)
                {
                    Account playerFromPlayerId = ChannelInfoHolder.getChannel(this.getClient().getChannelId()).getPlayerFromPlayerId(playerId);
                    if (playerFromPlayerId != null)
                        playerFromPlayerId.sendPacket((SendBaseGamePacket)new S_KILL_MESSAGE(playerFromPlayerId, this._kills));
                }
            }
        }
    }
}
