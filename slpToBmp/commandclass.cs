// Decompiled with JetBrains decompiler
// Type: DllPatchAok20.commandclass
// Assembly: DllPatchAok20, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C8909016-A2BC-428B-B1D1-F2128DD0464E
// Assembly location: C:\Users\m\Desktop\toolsAoe\DllPatchAok20.exe

using System;

namespace slpToBmp
{
  internal class commandclass
  {
    public byte cmdbyte;
    public byte next_byte;
    public byte[] data;
    public string type;

    internal commandclass(byte b)
    {
      this.cmdbyte = b;
      this.type = "one";
    }

    internal commandclass(byte b, byte n)
    {
      this.cmdbyte = b;
      this.next_byte = n;
      this.type = "two length";
    }

    internal commandclass(byte b, byte[] d)
    {
      this.cmdbyte = b;
      this.data = d;
      this.type = "two data";
    }

    internal commandclass(byte b, byte nb, byte[] d)
    {
      this.cmdbyte = b;
      this.next_byte = nb;
      this.data = d;
      this.type = "three";
    }

    internal virtual void print()
    {
      if (this.type.Equals("one"))
        Console.WriteLine("command: " + this.byteToHex(this.cmdbyte));
      else if (this.type.Equals("two length"))
        Console.WriteLine("command: " + this.byteToHex(this.cmdbyte) + " next byte " + this.byteToHex(this.next_byte));
      else if (this.type.Equals("two data"))
      {
        Console.Write("command: " + this.byteToHex(this.cmdbyte) + " data ");
        for (int index = 0; index < this.data.Length; ++index)
          Console.Write(((int) this.data[index] & (int) byte.MaxValue).ToString() + " ");
        Console.WriteLine();
      }
      else
      {
        if (!this.type.Equals("three"))
          return;
        string[] strArray = new string[6]
        {
          "command: ",
          this.byteToHex(this.cmdbyte),
          " next byte ",
          this.byteToHex(this.next_byte),
          " data  length ",
          null
        };
        int num = this.data.Length;
        strArray[5] = num.ToString();
        Console.WriteLine(string.Concat(strArray));
        for (int index = 0; index < this.data.Length; ++index)
        {
          num = (int) this.data[index] & (int) byte.MaxValue;
          Console.Write(num.ToString() + " ");
        }
        Console.WriteLine();
      }
    }

    internal virtual int commandlength()
    {
      if (this.type.Equals("one"))
        return 1;
      if (this.type.Equals("two length"))
        return 2;
      if (this.type.Equals("two data"))
        return 1 + this.data.Length;
      if (this.type.Equals("three"))
        return 2 + this.data.Length;
      Console.WriteLine("Whoa, weird type of command");
      return 0;
    }

    public virtual string byteToHex(byte d) => ((int) d & (int) byte.MaxValue).ToString("x");
  }
}
