using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PBServer.model.clans
{
  public class Clan
  {
    public string clan_name;
    public int clan_rank;
    public int clan_id;
    public int owner_id;
    public int _logo1;
    public int _logo2;
    public int _logo3;
    public int _logo4;
    public int _color;
    public int dateCreated; //временно
    public string clan_info;

        public Clan()
        {
            clan_id = 0;
            clan_name = ""; //CLAN_NAME = NULL
            clan_rank = 0; //CLAN_RANK = NULL
            clan_info = "";
            dateCreated = 0;
            _logo1 = 255; //LOGO1 = NULL
            _logo2 = 255; //LOGO2 = NULL
            _logo3 = 255; //LOGO3 = NULL
            _logo4 = 255; //LOGO4 = NULL
            _color = 0; //COLOR = NULL
        }

        public int getLogo1()
        {
            return this._logo1;
        }

        public int getLogo2()
        {
            return this._logo2;
        }

        public int getLogo3()
        {
            return this._logo3;
        }

        public int getLogo4()
        {
            return this._logo4;
        }

        public void setDateCreated(int date)
        {
            this.dateCreated = date;
        }

        public int getDateCreated()
        {
            return dateCreated;
        }

        public int getLogoColor()
        {
            return this._color;
        }

        public void setLogo1(int logo)
        {
            this._logo1 = logo;
        }

        public void setLogo2(int logo)
        {
            this._logo2 = logo;
        }

        public void setLogo3(int logo)
        {
            this._logo3 = logo;
        }

        public void setLogo4(int logo)
        {
            this._logo4 = logo;
        }

        public void setLogoColor(int color)
        {
            this._color = color;
        }

        public void setClanRank(int rank)
        {
            this.clan_rank = rank;
        }

        public int getClanRank()
        {
            return clan_rank;
        }

        public void setClanName(string name)
        {
            this.clan_name = name;
        }

        public string getClanName()
        {
            return clan_name;
        }

        public string getClanInfo()
        {
            return clan_info;
        }

        public void setOwnerId(int id)
        {
            this.owner_id = id;
        }

        public int getOwnerId()
        {
            return owner_id;
        }

        public void setClanId(int id)
        {
            this.clan_id = id;
        }

        public int getClanId()
        {
            return this.clan_id;
        }

        public String toString()
        {
            return "Clan{" +
                    "id=" + clan_id +
                    ", name='" + clan_name + '\'' +
                    ", rank=" + clan_rank +
                    ", ownerId=" + owner_id +
                    ", dateCreated=" + dateCreated +
                    ", color=" + _color +
                    ", logo1=" + _logo1 +
                    ", logo2=" + _logo2 +
                    ", logo3=" + _logo3 +
                    ", logo4=" + _logo4 +
                    '}';
        }

        public Object[] toObject()
        {

            Object[] args = new Object[8];
            args[0] = this.getClanName();
            args[1] = this.getClanRank();
            args[2] = this.getOwnerId();
            args[3] = this.getLogoColor();
            args[4] = this.getLogo1();
            args[5] = this.getLogo2();
            args[6] = this.getLogo3();
            args[7] = this.getLogo4();
            return args;
        }
    }
}
