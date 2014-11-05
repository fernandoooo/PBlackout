namespace crypt
{
  internal class BitRotate
  {
    public static void decrypt(byte[] a1)
    {
      byte num1 = (byte) a1.Length;
      byte num2 = (byte) 7;
      int num3 = a1.Length - 1;
      int num4 = 1;
      byte num5 = a1[a1.Length - 1];
      while (num3 >= 0)
      {
        byte num6 = (byte) ((num3 > 0 ? (int) a1[num3 - 1] : (int) num5) << num4 | (int) a1[num3--] >> (int) num2);
        a1[num3 + 1] = num6;
      }
    }

    public static void encrypt(byte[] a1)
    {
      int num1 = 7;
      byte[] numArray = a1;
      byte num2 = a1[0];
      int num3 = 1;
      int num4 = 0;
      int num5 = 1;
      if (a1.Length <= 0)
        return;
      while (true)
      {
        int num6 = num4 >= a1.Length - 1 ? (int) num2 : (int) numArray[num4 + 1];
        int num7 = (int) numArray[num4++] << num1;
        numArray[num4 - 1] = (byte) (num7 | num6 >> num3);
        if (num4 < a1.Length)
        {
          int num8 = (int) (ushort) num3 & (int) byte.MaxValue;
          int num9 = (int) (ushort) num3 >> 8;
          num3 = num5 & (int) byte.MaxValue | (num9 & (int) byte.MaxValue) << 8;
        }
        else
          break;
      }
    }
  }
}
