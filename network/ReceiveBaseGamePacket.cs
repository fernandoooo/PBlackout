// Type: PBServer.network.ReceiveBaseGamePacket
// Assembly: PBServerС, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 53622072-67ED-420B-9B0A-95B7B0DD27F6
// Assembly location: D:\Games\OZ-Network\pb\rev\server\Project Blackout Server Consolidation.exe

using PBServer;
using System;
using System.Text;

namespace PBServer.network
{
  public abstract class ReceiveBaseGamePacket
  {
    private byte[] _buffer;
    private int _offset;
    private GameClient _Client;

    protected internal GameClient getClient()
    {
      return this._Client;
    }

    protected internal byte[] getBuffer()
    {
      return this._buffer;
    }

    protected internal void makeme(GameClient Client, byte[] buffer)
    {
      this._Client = Client;
      this._buffer = buffer;
      this._offset = 2;
      this.read();
    }

    protected internal int readD()
    {
      int num = BitConverter.ToInt32(this._buffer, this._offset);
      this._offset += 4;
      return num;
    }

    protected internal byte readC()
    {
      byte num = this._buffer[this._offset];
      ++this._offset;
      return num;
    }

    protected internal byte[] readB(int Length)
    {
      byte[] numArray = new byte[Length];
      Array.Copy((Array) this._buffer, this._offset, (Array) numArray, 0, Length);
      this._offset += Length;
      return numArray;
    }

    protected internal short readH()
    {
      short num = BitConverter.ToInt16(this._buffer, this._offset);
      this._offset += 2;
      return num;
    }

    protected internal double readF()
    {
      double num = BitConverter.ToDouble(this._buffer, this._offset);
      this._offset += 8;
      return num;
    }

    protected internal long readQ()
    {
      long num = BitConverter.ToInt64(this._buffer, this._offset);
      this._offset += 8;
      return num;
    }

    protected internal string readS(int Length)
    {
      string str = "";
      try
      {
        str = Encoding.GetEncoding(1251).GetString(this._buffer, this._offset, Length);
        int length = str.IndexOf(char.MinValue);
        if (length != -1)
          str = str.Substring(0, length);
        this._offset += Length;
      }
      catch (Exception ex)
      {
        CLogger.getInstance().error("while reading string from packet, " + ex.Message + " " + ex.StackTrace);
      }
      return str;
    }

    protected internal string readS()
    {
      string str = "";
      try
      {
        str = Encoding.Unicode.GetString(this._buffer, this._offset, this._buffer.Length - this._offset);
        int length = str.IndexOf(char.MinValue);
        if (length != -1)
          str = str.Substring(0, length);
        this._offset += str.Length + 1;
      }
      catch (Exception ex)
      {
        CLogger.getInstance().error("while reading string from packet, " + ex.Message + " " + ex.StackTrace);
      }
      return str;
    }

    protected internal void ignore(int in_offset)
    {
      this._offset = this._offset + in_offset;
      CLogger.getInstance().info("Ignore " + (object) in_offset + "bytes");
    }

    protected internal abstract void read();

    protected internal abstract void run();
  }
}
