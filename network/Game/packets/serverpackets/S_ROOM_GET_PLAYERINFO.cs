using PBServer;
using PBServer.src.model.accounts;
using PBServer.src.model.rooms;
using PBServer.src.model;
using PBServer.model.clans;
using PBServer.managers;

namespace PBServer.network.Game.packets.serverpackets
{
    public class S_ROOM_GET_PLAYERINFO : SendBaseGamePacket
    {
        private int _slot;
        private Account _p;
        public S_ROOM_GET_PLAYERINFO(int slot, Account p)
        {
            this._slot = slot;
            this._p = p;
            this.makeme();
        }

        protected internal override void write()
        {
            CLogger.getInstance().info("Send: opcode_3587_ACK");
            if (this._p.getRoom() != null && this._p.getRoom().getPlayerBySlot(_slot) != null)
            {
                Room r = this._p.getRoom();
                Account p = r.getPlayerBySlot(_slot);
                CLogger.getInstance().info("Tipo1");
                writeD(1); // если есть инфа или игрок?
                writeS(p.getPlayerName(), 33); // Имя перса
                writeD(p.getExp()); // опыт
                writeD(p.getRank()); // ранк (0-54)
                writeD(0); // Пока не понятно за чего отвечают пустые байты...
                writeD(p.getGP()); // ГП
                writeD(0); // Рублики
                writeB(new byte[13]); // Хз что это такое
                writeD(p.getPcCafe()); // Какое то извещение....Оо PC_Cafe(0x02 - Премиум, 0x01 - Нормал, 0x00 - нет пс_кафе) =)
                writeH(0);
                if (p.getClanId() == 0)
                {
                    writeS("", 17); // Clan name 16 символов =) //ERRO PODE ESTAR AQUI PORQUE A QUANTIDADE DE CARACTERES NO MYINFO É DE 17 E AQUI É 16
                    writeC(0); // что то типа разделителя между названием клана и его рангом
                    writeH(0); // Ранг клана - 53 - max
                    writeC(255); // Лого1
                    writeC(255); // Лого2
                    writeC(255); // Лого3
                    writeC(255); // Лого4
                    writeH(0); // цвет названия клана.
                }
                if (p.getClanId() > 0)
                {
                    Clan clan = ClanManager.getInstance().get(p.getClanId());
                    writeS(clan.getClanName(), 17); // Clan name 16 символов =)
                    writeC(0); // что то типа разделителя между названием клана и его рангом
                    writeH((short)clan.getClanRank()); // Ранг клана - 53 - max
                    writeC((byte)clan.getLogo1()); // Лого1
                    writeC((byte)clan.getLogo2()); // Лого2
                    writeC((byte)clan.getLogo3()); // Лого3
                    writeC((byte)clan.getLogo4()); // Лого4
                    writeH((short)clan.getLogoColor()); // цвет названия клана.
                }
                writeD(0); // Непонятно чо
                writeD(0);
                writeD(0);
                writeD(p._statistic.getFights(true)); // Количество боев
                writeD(p._statistic.getWinFights(true)); // Количество побед
                writeD(p._statistic.getLostFights(true)); // Количество поражениев
                writeD(0); // Непонятно чо
                writeD(p._statistic.getKills(true)); // Количество убийств
                writeD(0); // Непонятно чо
                writeD(p._statistic.getDeaths(true)); // Количество смертей
                writeD(0); // Непонятно чо
                writeD(0); // Непонятно чо
                writeD(p._statistic.getEscapes(true)); // Количество побегов с поля боя
                writeD(p._statistic.getFights(true)); // Количество боев
                writeD(p._statistic.getWinFights(true)); // Количество побед
                writeD(p._statistic.getLostFights(true)); // Количество поражений
                writeD(0); // Непонятно чо
                writeD(p._statistic.getKills(true)); // Количество убийств
                writeD(0); // Непонятно чо
                writeD(p._statistic.getDeaths(true)); // Количество смертей
                writeD(0); // Непонятно чо
                writeD(0); // Непонятно чо
                writeD(p._statistic.getEscapes(true)); // Количество побегов с поля боя
                writeD(p.getCharRed()); // Скин Мужчина стандартный красные.
                writeD(p.getCharBlue()); // Скин Мужчина стандартный синие.
                writeD(p.getCharHelmet()); // Шлем.
                writeD(p.getCharBeret()); // Берет.
                writeD(p.getCharDino()); // Скин дино.
                writeD(p.getPrimaryWeapon()); // Основное оружие
                writeD(p.getSecondaryWeapon()); // Второстепенное оружие
                writeD(p.getMeleeWeapon()); // Ближнего боя
                writeD(p.getThrownNormalWeapon()); // Гранаты (Гранаты для взрыва)
                writeD(p.getThrownSpecialWeapon()); // Гранаты (Гранаты специальные, смок, слеповуха)
            }
            else
            {
                CLogger.getInstance().info("Tipo2");
                this.writeD(1);
            }
        }
    }
}
