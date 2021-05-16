// Decompiled with JetBrains decompiler
// Type: DllPatchAok20.frame
// Assembly: DllPatchAok20, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C8909016-A2BC-428B-B1D1-F2128DD0464E
// Assembly location: C:\Users\m\Desktop\toolsAoe\DllPatchAok20.exe

using System;

namespace slpToBmp
{
  internal class frame
  {
    public int frame_data_offsets;
    public int frame_outline_offset;
    public int palette_offset;
    public int properties;
    public int width;
    public int height;
    public int anchorx;
    public int anchory;
    public rowedge[] edges;
    public int[] commandoff;
    public commandclass[] commands;
    public int numcommands;
    public int numcommandbytes;
    public int[][] picture;
    public int[] pointer;

    internal virtual void initrowedge(int n)
    {
      this.edges = new rowedge[n];
      for (int index = 0; index < n; ++index)
        this.edges[index] = new rowedge();
      this.commandoff = new int[n];
      this.pointer = new int[n];
    }

    internal virtual void initpic(int w, int h) => this.picture = frame.RectangularArrays.RectangularIntArray(h, w);

    internal virtual void initcommands()
    {
      this.commands = new commandclass[500000];
      this.numcommands = 0;
    }

    internal virtual void drawmask(int row, int n)
    {
      for (int index = 0; index < n; ++index)
      {
        this.picture[row][this.pointer[row]] = -1;
        this.pointer[row] = this.pointer[row] + 1;
      }
    }

    internal virtual void drawselectionmask(int row, int n)
    {
      for (int index = 0; index < n; ++index)
      {
        this.picture[row][this.pointer[row]] = -2;
        this.pointer[row] = this.pointer[row] + 1;
      }
    }

    internal virtual void drawselectionmask2(int row, int n)
    {
      for (int index = 0; index < n; ++index)
      {
        this.picture[row][this.pointer[row]] = -3;
        this.pointer[row] = this.pointer[row] + 1;
      }
    }

    internal virtual void drawshadowpixels(int row, int n)
    {
      for (int index = 0; index < n; ++index)
      {
        this.picture[row][this.pointer[row]] = -4;
        this.pointer[row] = this.pointer[row] + 1;
      }
    }

    internal virtual void drawbytearray(int row, byte[] data)
    {
      int length = data.Length;
      for (int index = 0; index < length; ++index)
      {
        int num = (int) data[index];
        if (num < 0)
          num += 256;
        if (this.pointer[row] < this.width)
        {
          this.picture[row][this.pointer[row]] = num;
          this.pointer[row] = this.pointer[row] + 1;
        }
      }
    }

    internal virtual void drawplayercolors(int row, byte[] data, int player)
    {
      int length = data.Length;
      for (int index = 0; index < length; ++index)
      {
        int num1 = (int) data[index];
        if (num1 < 0)
          num1 += 256;
        int num2 = player * 16 + 16 + num1;
        this.picture[row][this.pointer[row]] = num2;
        this.pointer[row] = this.pointer[row] + 1;
      }
    }

    internal virtual void fill(int row, byte[] color, int n)
    {
      for (int index = 0; index < n; ++index)
      {
        int num = (int) color[0];
        if (num < 0)
          num += 256;
        this.picture[row][this.pointer[row]] = num;
        this.pointer[row] = this.pointer[row] + 1;
      }
    }

    internal virtual void playercolorfill(int row, byte[] color, int n, int player)
    {
      for (int index = 0; index < n; ++index)
      {
        int num1 = (int) color[0];
        if (num1 < 0)
          num1 += 256;
        int num2 = player * 16 + 16 + num1;
        this.picture[row][this.pointer[row]] = num2;
        this.pointer[row] = this.pointer[row] + 1;
      }
    }

    internal virtual void display()
    {
      for (int index1 = 0; index1 < this.height; ++index1)
      {
        Console.WriteLine("line: " + index1.ToString());
        for (int index2 = 0; index2 < this.width; ++index2)
          Console.Write(this.picture[index1][index2].ToString() + " ");
        Console.WriteLine();
      }
    }

    internal virtual void converttobitmap(
      string filename,
      int mask,
      int outline1,
      int outline2,
      int shadow,
      string samp)
    {
      new Aokbitmap(mask, outline1, outline2, shadow)
      {
        sample = samp
      }.Write(filename, this.picture, this.width, this.height);
    }

    internal virtual void cmdcolorblock(byte[] array, bool playercolorused)
    {
      int num1 = 0;
      int length1 = 0;
      int num2 = 0;
      int length2 = 0;
      int index1 = 0;
      bool flag1 = ((array[index1] < (byte) 16 ? 0 : (array[index1] <= (byte) 23 ? 1 : 0)) & (playercolorused ? 1 : 0)) != 0;
      if (flag1)
      {
        num2 = 0;
        length2 = 1;
      }
      else
      {
        num1 = 0;
        length1 = 1;
      }
      for (int index2 = 1; index2 < array.Length; ++index2)
      {
        bool flag2 = ((array[index2] < (byte) 16 ? 0 : (array[index2] <= (byte) 23 ? 1 : 0)) & (playercolorused ? 1 : 0)) != 0;
        if (flag2 && !flag1)
        {
          byte[] array1 = new byte[length1];
          for (int index3 = 0; index3 < length1; ++index3)
            array1[index3] = array[num1 + index3];
          this.cmdcolor(array1);
          num2 = index2;
          length2 = 1;
          flag1 = true;
        }
        else if (!flag2 & flag1)
        {
          byte[] array1 = new byte[length2];
          for (int index3 = 0; index3 < length2; ++index3)
            array1[index3] = array[num2 + index3];
          this.cmdplayercolor(array1);
          num1 = index2;
          length1 = 1;
          flag1 = false;
        }
        else if (flag2)
          ++length2;
        else
          ++length1;
      }
      if (flag1)
      {
        byte[] array1 = new byte[length2];
        for (int index2 = 0; index2 < length2; ++index2)
          array1[index2] = array[num2 + index2];
        this.cmdplayercolor(array1);
      }
      else
      {
        byte[] array1 = new byte[length1];
        for (int index2 = 0; index2 < length1; ++index2)
          array1[index2] = array[num1 + index2];
        this.cmdcolor(array1);
      }
    }

    internal virtual void cmdplayercolor(byte[] array)
    {
      byte b = 6;
      int length1 = array.Length;
      byte[] d = new byte[length1];
      if (length1 >= 256)
      {
        int length2;
        int length3;
        if (length1 % 2 == 0)
        {
          length2 = length1 / 2;
          length3 = length1 / 2;
        }
        else
        {
          length2 = (length1 - 1) / 2;
          length3 = (length1 + 1) / 2;
        }
        byte[] array1 = new byte[length2];
        byte[] array2 = new byte[length3];
        for (int index = 0; index < length2; ++index)
          array1[index] = array[index];
        for (int index = 0; index < length3; ++index)
          array2[index] = array[length2 + index - 1];
        this.cmdplayercolor(array1);
        this.cmdplayercolor(array2);
      }
      else
      {
        byte nb = (byte) length1;
        for (int index = 0; index < array.Length; ++index)
          d[index] = (byte) (((int) array[index] & (int) byte.MaxValue) - 16);
        this.commands[this.numcommands] = new commandclass(b, nb, d);
        ++this.numcommands;
      }
    }

    internal virtual void cmdcolor(byte[] array)
    {
      int num = 0;
      while (num < array.Length)
        ++num;
      int length1 = array.Length;
      if (length1 >= 64)
      {
        int length2;
        int length3;
        if (length1 % 2 == 0)
        {
          length2 = length1 / 2;
          length3 = length1 / 2;
        }
        else
        {
          length2 = (length1 - 1) / 2;
          length3 = (length1 + 1) / 2;
        }
        byte[] array1 = new byte[length2];
        byte[] array2 = new byte[length3];
        for (int index = 0; index < length2; ++index)
          array1[index] = array[index];
        for (int index = 0; index < length3; ++index)
          array2[index] = array[length2 + index - 1];
        this.cmdcolor(array1);
        this.cmdcolor(array2);
      }
      else
      {
        this.commands[this.numcommands] = new commandclass((byte) (length1 << 2), array);
        ++this.numcommands;
      }
    }

    internal virtual void cmdskip(int n)
    {
      int num = n;
      if (num >= 64)
      {
        int n1;
        int n2;
        if (n % 2 == 0)
        {
          n1 = n / 2;
          n2 = n / 2;
        }
        else
        {
          n1 = (n - 1) / 2;
          n2 = (n + 1) / 2;
        }
        this.cmdskip(n1);
        this.cmdskip(n2);
      }
      else
      {
        byte b = (byte) (num << 2);
        switch ((int) b & 12)
        {
          case 0:
            b = (byte) (((int) b & (int) byte.MaxValue) + 1);
            break;
          case 4:
            b = (byte) (((int) b & (int) byte.MaxValue) + 1);
            break;
          case 8:
            b = (byte) (((int) b & (int) byte.MaxValue) + 1);
            break;
          case 12:
            b = (byte) (((int) b & (int) byte.MaxValue) + 1);
            break;
          default:
            Console.WriteLine("uh oh");
            break;
        }
        this.commands[this.numcommands] = new commandclass(b);
        ++this.numcommands;
      }
    }

    internal virtual void cmdoutline1(int n)
    {
      int num = n;
      if (num == 1)
      {
        this.commands[this.numcommands] = new commandclass((byte) 78);
        ++this.numcommands;
      }
      else
      {
        this.commands[this.numcommands] = new commandclass((byte) 94, (byte) num);
        ++this.numcommands;
      }
    }

    internal virtual void cmdoutline2(int n)
    {
      int num = n;
      if (num == 1)
      {
        this.commands[this.numcommands] = new commandclass((byte) 110);
        ++this.numcommands;
      }
      else
      {
        this.commands[this.numcommands] = new commandclass((byte) 126, (byte) num);
        ++this.numcommands;
      }
    }

    internal virtual void cmdshadowpix(int n)
    {
      byte b = 11;
      int num = n;
      if (num >= 256)
      {
        int n1;
        int n2;
        if (n % 2 == 0)
        {
          n1 = n / 2;
          n2 = n / 2;
        }
        else
        {
          n1 = (n - 1) / 2;
          n2 = (n + 1) / 2;
        }
        this.cmdshadowpix(n1);
        this.cmdshadowpix(n2);
      }
      else
      {
        byte n1 = (byte) num;
        this.commands[this.numcommands] = new commandclass(b, n1);
        ++this.numcommands;
      }
    }

    internal virtual void cmdeol()
    {
      this.commands[this.numcommands] = new commandclass((byte) 15);
      ++this.numcommands;
    }

    internal virtual void displaycommands()
    {
      int num = 0;
      int index = 0;
      Console.WriteLine("----------------------Line 0");
      for (; index < this.numcommands; ++index)
      {
        this.commands[index].print();
        if (this.commands[index].cmdbyte == (byte) 15)
        {
          ++num;
          Console.WriteLine("----------------------Line" + num.ToString());
        }
      }
    }

    internal virtual void convertcvt(int[] array)
    {
      for (int index1 = 0; index1 < this.picture.Length; ++index1)
      {
        for (int index2 = 0; index2 < this.picture[0].Length; ++index2)
        {
          for (int index3 = 0; index3 < 256; ++index3)
          {
            if (this.picture[index1][index2] == index3 && array[index3] >= 0)
            {
              this.picture[index1][index2] = array[index3];
              if (array[index3] == 131)
                this.picture[index1][index2] = -4;
            }
          }
        }
      }
    }

    internal virtual string bytestring(byte b)
    {
      int num = (int) b;
      string str = "";
      for (int index = 1; index <= 8; ++index)
      {
        str = (num % 2).ToString() + str;
        num /= 2;
      }
      return str;
    }

    internal virtual void computeframeheader()
    {
      this.palette_offset = 0;
      this.properties = 24;
    }

    internal virtual void setanchor(int a, int b)
    {
      this.anchorx = a;
      this.anchory = b;
    }

    internal virtual void computecommandbytes()
    {
      int num = 0;
      for (int index = 0; index < this.numcommands; ++index)
      {
        string type = this.commands[index].type;
        if (type.Equals("one"))
          ++num;
        else if (type.Equals("two Length"))
          num += 2;
        else if (type.Equals("two data"))
          num += 1 + this.commands[index].data.Length;
        else if (type.Equals("three"))
          num += 2 + this.commands[index].data.Length;
        else
          Console.WriteLine("Whoa, weird command.type in computecommandbytes");
      }
      this.numcommandbytes = num;
    }

    internal virtual int linecommandLength(int n)
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      for (int index = 0; index < this.numcommands; ++index)
      {
        if (this.commands[index].type.Equals("one"))
          ++num2;
        else if (this.commands[index].type.Equals("two Length"))
          num2 += 2;
        else if (this.commands[index].type.Equals("two data"))
          num2 += 1 + this.commands[index].data.Length;
        else if (this.commands[index].type.Equals("three"))
          num2 += 2 + this.commands[index].data.Length;
        else
          Console.WriteLine("Whoa, weird command.type in linecommandLength");
        if (this.commands[index].cmdbyte == (byte) 15)
        {
          if (num1 == n)
            num3 = num2;
          ++num1;
          num2 = 0;
        }
      }
      return num3;
    }

    internal static class RectangularArrays
    {
      public static int[][] RectangularIntArray(int size1, int size2)
      {
        int[][] numArray = new int[size1][];
        for (int index = 0; index < size1; ++index)
          numArray[index] = new int[size2];
        return numArray;
      }
    }
  }
}
