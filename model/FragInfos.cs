using System.Collections.Generic;

namespace PBServer.model
{
    public class FragInfos
    {
        public int unkCount;
        public int killer;
        public int killsCount;
        public int weapon;
        public byte[] bytes13;
        public byte[] bytes131;
        public byte[] remBytes;
        public int unk_c_1;
        public int unk_c_2;
        public int unk_c_3;
        public int unk_c_4;
        public FragInfos()
        {
            
        }
        public int getDeatSlot()
        {
            return this.unk_c_2 & 15;
        }
    }
}

