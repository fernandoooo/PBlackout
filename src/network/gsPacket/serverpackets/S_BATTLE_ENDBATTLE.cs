using PBServer;
using PBServer.model.ENUMS;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;
using PBServer.model.clans;
using PBServer.managers;
using System;

namespace PBServer.src.network.gsPacket.serverpackets
{
    internal class S_BATTLE_ENDBATTLE : SendBaseGamePacket
    {
        private Account _player;
        private Room r;
        public S_BATTLE_ENDBATTLE(Account p)
        {
            this.makeme();
            this._player = p;
            this.r = p.getRoom();
            this.r.changeSlotState(p.getSlot(), SLOT_STATE.SLOT_STATE_NORMAL, true);
        }

        protected internal override void write()
        {
            Clan c = ClanManager.getInstance().get(_player.getClanId());
            this.writeH((short)0xD08);
            //|==COMEÇO DO COMANDO DE QUEM VENCEU==|
            if (this.r.getKills(TeamEnum.CHARACTER_TEAM_BLUE) == this.r.getKills(TeamEnum.CHARACTER_TEAM_RED))
                this.writeC((byte)2);
            else if (this.r.getKills(TeamEnum.CHARACTER_TEAM_BLUE) > this.r.getKills(TeamEnum.CHARACTER_TEAM_RED))
                this.writeC((byte)1);
            else
                this.writeC((byte)0);
            //|==FINAL DO COMANDO DE QUEM VENCEU==|
            if (this._player == null || this.r.getLeader() == null || this._player.player_id != this.r.getLeader().player_id)
                return;
            this.r.stopBattle(this._player);
            this.writeH((short)383);
            this.writeH((short)266);
            for (int id = 0; id < 16; ++id)
            {
                SLOT slot = this.r.getSlot(id);
                if (slot != null)
                    this.writeH((short)slot.exp);
                else
                    this.writeH((short)0);
            }
            for (int id = 0; id < 16; ++id)
            {
                SLOT slot = this.r.getSlot(id);
                if (slot != null)
                    this.writeH((short)slot.gp);
                else
                    this.writeH((short)0);
            }
            for (int index = 0; index < 16; ++index)
            {
                if (this.r.getPlayerBySlot(index) != null && this.r.special == 6)
                    this.writeH((short)this.r.getSlot(index).botScore);
                else
                    this.writeH((short)0);
            }
            writeB(new byte[]{
				0x00, 0x00, 0x00, 0x00, 0x00, 0x00,//
				0x00, 0x00, 0x00, 0x00, 0x00, 0x00,//
				0x00, 0x00, 0x00, 0x00, 0x00, 0x00,//
				0x00, 0x00, 0x00, 0x00, 0x00, 0x00,//
				0x00, 0x00, 0x00, 0x00, 0x00, 0x00,//
				0x00, 0x00,//
				// и тут ХЗ
				0x00, 0x00, 0x00, 0x00, 0x00, 0x00,//
				0x00, 0x00, 0x00, 0x00, 0x00, 0x00,//
				0x00, 0x00, 0x00, 0x00
				//
		});
            this.writeS(this._player.getPlayerName(), 33);
            this.writeD(this._player.getExp());
            this.writeD(this._player.getRank());
            this.writeD(this._player.getRank());
            this.writeD(this._player.getGP());
            this.writeD(this._player.getMoney());
            this.writeD(0); // ClanID
            this.writeD(0); // ClanNameColor
            this.writeD(0); // Unk
            this.writeD(0); // Unk
            this.writeH((short)this._player.getPcCafe());
            this.writeC((byte)this._player.getNameColor()); // 0-9 Color name
            if (this._player.getClanId() == 0)
            {
                this.writeS("", 16);
                this.writeC((byte)0);
                this.writeH((short)0);
                this.writeC((byte)255);
                this.writeC((byte)255);
                this.writeC((byte)255);
                this.writeC((byte)255);
                this.writeH((short)0);
            }
            if (this._player.getClanId() > 0)
            {
                this.writeS(c.getClanName(), 16);
                this.writeC((byte)0);
                this.writeH((short)c.getClanRank());
                this.writeC((byte)c.getLogo1());
                this.writeC((byte)c.getLogo2());
                this.writeC((byte)c.getLogo3());
                this.writeC((byte)c.getLogo4());
                this.writeH((short)c.getLogoColor());
            }
            this.writeD(0); // Непонятно чо

            this.writeB(new byte[9]);
            this.writeD(this._player._statistic.getFights(true));
            this.writeD(this._player._statistic.getWinFights(true)); //VÍTORIAS
            this.writeD(this._player._statistic.getLostFights(true));
            this.writeD(0);
            this.writeD(this._player._statistic.getKills(true));
            this.writeD(0);
            this.writeD(this._player._statistic.getDeaths(true));
            this.writeD(0);
            this.writeD(0);
            this.writeD(this._player._statistic.getEscapes(true));
            this.writeD(this._player._statistic.getFights(true));
            this.writeD(this._player._statistic.getWinFights(true)); //VÍTORIAS
            this.writeD(this._player._statistic.getLostFights(true));
            this.writeD(0);
            this.writeD(this._player._statistic.getKills(true));
            this.writeD(0);
            this.writeD(this._player._statistic.getDeaths(true));
            this.writeD(0);
            this.writeD(0);
            this.writeD(this._player._statistic.getEscapes(true));
            this.writeB(new byte[53]);
        }
    }
}
