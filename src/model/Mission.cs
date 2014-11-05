namespace PBServer.src.model
{
  public class Mission
  {
    public int[] cards_tutorial = new int[40];
    public int owner_id;

    public int getOwnerId()
    {
        return this.owner_id;
    }

    public int getMission1()
    {
        return cards_tutorial[1];
    }

    public int getMission2()
    {
        return cards_tutorial[2];
    }

    public int getMission3()
    {
        return cards_tutorial[3];
    }

    public int getMission4()
    {
        return cards_tutorial[4];
    }

    public int getMission5()
    {
        return cards_tutorial[5];
    }

    public int getMission6()
    {
        return cards_tutorial[6];
    }

    public int getMission7()
    {
        return cards_tutorial[7];
    }

    public int getMission8()
    {
        return cards_tutorial[8];
    }
  }
}
