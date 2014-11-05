namespace PBServer.model
{
    public class Frag
    {
        public int unk_c_1;
        public int unk_c_2;
        public int unk_c_3;
        public int unk_c_4;
        public byte[] bytes13;
        public int getDeatSlot()
        {
            return this.unk_c_2 & 15;
        }
    }
}