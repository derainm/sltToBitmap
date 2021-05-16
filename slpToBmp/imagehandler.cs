// Decompiled with JetBrains decompiler
// Type: DllPatchAok20.imagehandler
// Assembly: DllPatchAok20, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C8909016-A2BC-428B-B1D1-F2128DD0464E
// Assembly location: C:\Users\m\Desktop\toolsAoe\DllPatchAok20.exe

using System;
using System.Drawing;
using System.IO;

namespace slpToBmp
{
  internal class imagehandler
  {
    public Image img;
    internal byte[][] colortable = imagehandler.RectangularArrays.RectangularbyteArray(256, 4);
    public byte[][] aoktable = imagehandler.RectangularArrays.RectangularbyteArray(256, 4);
    public int[] mapping = new int[256];
    public int mask;
    public byte[] rawpixels;
    public byte[] outputpixels;
    public byte[] aokpalette;
    public int aokmask = (int) byte.MaxValue;
    public string sampleused = "50500.bmp";
    public bool aoeused;
    public int imgheight;
    public int imgwidth;
    internal int maskoriginal;

    public virtual void loadbitmap(string filename, int whichfile)
    {
      try
      {
        if (string.IsNullOrEmpty(filename))
          return;
        if (whichfile == 1)
          filename = this.sampleused;
        FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
        int count1 = 14;
        byte[] buffer1 = new byte[count1];
        fileStream.Read(buffer1, 0, count1);
        int count2 = 40;
        byte[] buffer2 = new byte[count2];
        fileStream.Read(buffer2, 0, count2);
        int num1 = (int) buffer1[5];
        int num2 = (int) buffer1[4];
        int num3 = (int) buffer1[3];
        int num4 = (int) buffer1[2];
        int num5 = (int) buffer2[3];
        int num6 = (int) buffer2[2];
        int num7 = (int) buffer2[1];
        int num8 = (int) buffer2[0];
        int num9 = ((int) buffer2[7] & (int) byte.MaxValue) << 24 | ((int) buffer2[6] & (int) byte.MaxValue) << 16 | ((int) buffer2[5] & (int) byte.MaxValue) << 8 | (int) buffer2[4] & (int) byte.MaxValue;
        int num10 = ((int) buffer2[11] & (int) byte.MaxValue) << 24 | ((int) buffer2[10] & (int) byte.MaxValue) << 16 | ((int) buffer2[9] & (int) byte.MaxValue) << 8 | (int) buffer2[8] & (int) byte.MaxValue;
        int num11 = (int) buffer2[13];
        int num12 = (int) buffer2[12];
        int num13 = ((int) buffer2[15] & (int) byte.MaxValue) << 8 | (int) buffer2[14] & (int) byte.MaxValue;
        int num14 = (int) buffer2[19];
        int num15 = (int) buffer2[18];
        int num16 = (int) buffer2[17];
        int num17 = (int) buffer2[16];
        int num18 = ((int) buffer2[23] & (int) byte.MaxValue) << 24 | ((int) buffer2[22] & (int) byte.MaxValue) << 16 | ((int) buffer2[21] & (int) byte.MaxValue) << 8 | (int) buffer2[20] & (int) byte.MaxValue;
        int num19 = (int) buffer2[27];
        int num20 = (int) buffer2[26];
        int num21 = (int) buffer2[25];
        int num22 = (int) buffer2[24];
        int num23 = (int) buffer2[31];
        int num24 = (int) buffer2[30];
        int num25 = (int) buffer2[29];
        int num26 = (int) buffer2[28];
        int num27 = ((int) buffer2[35] & (int) byte.MaxValue) << 24 | ((int) buffer2[34] & (int) byte.MaxValue) << 16 | ((int) buffer2[33] & (int) byte.MaxValue) << 8 | (int) buffer2[32] & (int) byte.MaxValue;
        int num28 = (int) buffer2[39];
        int num29 = (int) buffer2[38];
        int num30 = (int) buffer2[37];
        int num31 = (int) buffer2[36];
        if (num13 != 8)
        {
          Console.WriteLine("Not a 256 color bitmap!");
        }
        else
        {
          int num32 = num27 <= 0 ? 1 << num13 : num27;
          if (num18 == 0)
            num18 = ((int) ((long) (num9 * num13 + 31) & 4294967264L) >> 3) * num10;
          byte[] buffer3 = new byte[num32 * 4];
          fileStream.Read(buffer3, 0, num32 * 4);
          if (whichfile == 1)
          {
            this.aokpalette = new byte[num32 * 4];
            this.aokpalette = buffer3;
          }
          int num33 = 0;
          for (int index1 = 0; index1 < num32; ++index1)
          {
            for (int index2 = 0; index2 <= 3; ++index2)
            {
              if (whichfile == 1)
                this.aoktable[index1][index2] = buffer3[num33 + index2];
              else
                this.colortable[index1][index2] = buffer3[num33 + index2];
            }
            num33 += 4;
          }
          int num34 = num18 / num10 - num9;
          if (whichfile == 0)
          {
            this.rawpixels = new byte[(num9 + num34) * num10];
            fileStream.Read(this.rawpixels, 0, (num9 + num34) * num10);
            this.imgheight = num10;
            this.imgwidth = num9;
          }
          fileStream.Close();
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
        Console.Write(ex.StackTrace);
        Console.WriteLine("Caught exception in loadbitmap!");
      }
    }

    public virtual void getmapping()
    {
      for (int index1 = 0; index1 < 256; ++index1)
      {
        int num1 = 200000;
        int num2 = 0;
        for (int index2 = 0; index2 < 256; ++index2)
        {
          int num3 = (int) this.colortable[index1][0];
          if (num3 < 0)
            num3 += 256;
          int num4 = (int) this.colortable[index1][1];
          if (num4 < 0)
            num4 += 256;
          int num5 = (int) this.colortable[index1][2];
          if (num5 < 0)
            num5 += 256;
          int num6 = (int) this.aoktable[index2][0];
          if (num6 < 0)
            num6 += 256;
          int num7 = (int) this.aoktable[index2][1];
          if (num7 < 0)
            num7 += 256;
          int num8 = (int) this.aoktable[index2][2];
          if (num8 < 0)
            num8 += 256;
          long num9 = (long) ((num3 - num6) * (num3 - num6) + (num4 - num7) * (num4 - num7) + (num5 - num8) * (num5 - num8));
          if (num9 < (long) num1)
          {
            num1 = (int) num9;
            num2 = index2;
          }
        }
        this.mapping[index1] = num2;
      }
      this.mask = (int) this.rawpixels[0];
      if (this.mask >= 0)
        return;
      this.mask += 256;
    }

    public virtual void getmask()
    {
      this.maskoriginal = this.mapping[this.mask];
      this.mapping[this.mask] = this.aokmask;
    }

    internal virtual void setmask(int x)
    {
      this.mapping[this.mask] = this.maskoriginal;
      this.mapping[x] = this.aokmask;
    }

    public virtual void setplayercolor()
    {
      int[] numArray = new int[256];
      for (int index1 = 0; index1 < 256; ++index1)
      {
        int num1 = 200000;
        int num2 = 0;
        for (int index2 = 17; index2 <= 23; ++index2)
        {
          int num3 = (int) this.aoktable[index1][0];
          if (num3 < 0)
            num3 += 256;
          int num4 = (int) this.aoktable[index1][1];
          if (num4 < 0)
            num4 += 256;
          int num5 = (int) this.aoktable[index1][2];
          if (num5 < 0)
            num5 += 256;
          int num6 = (int) this.aoktable[index2][0];
          if (num6 < 0)
            num6 += 256;
          int num7 = (int) this.aoktable[index2][1];
          if (num7 < 0)
            num7 += 256;
          int num8 = (int) this.aoktable[index2][2];
          if (num8 < 0)
            num8 += 256;
          long num9 = (long) ((num5 - num8) * (num5 - num8) + (num4 - num7) * (num4 - num7) + (num3 - num6) * (num3 - num6));
          if (num9 < (long) num1 && num3 >= 97 && (num3 > num4 + num5 && num5 < 120) && num4 < 120)
          {
            num1 = (int) num9;
            num2 = index2;
          }
        }
        if (num2 != 0)
        {
          for (int index2 = 0; index2 < 256; ++index2)
          {
            if (this.mapping[index2] == index1)
              this.mapping[index2] = num2;
          }
        }
      }
      this.getmask();
    }

    public virtual void savebitmap(string inputfile, string outputfile)
    {
      try
      {
        FileStream fileStream1 = new FileStream(inputfile, FileMode.Open, FileAccess.Read);
        FileStream fileStream2 = new FileStream(outputfile, FileMode.Create, FileAccess.Write);
        int count1 = 14;
        byte[] buffer1 = new byte[count1];
        fileStream1.Read(buffer1, 0, count1);
        fileStream2.Write(buffer1, 0, count1);
        int count2 = 40;
        byte[] buffer2 = new byte[count2];
        fileStream1.Read(buffer2, 0, count2);
        fileStream2.Write(buffer2, 0, count2);
        int num1 = (int) buffer1[5];
        int num2 = (int) buffer1[4];
        int num3 = (int) buffer1[3];
        int num4 = (int) buffer1[2];
        int num5 = (int) buffer2[3];
        int num6 = (int) buffer2[2];
        int num7 = (int) buffer2[1];
        int num8 = (int) buffer2[0];
        int num9 = ((int) buffer2[7] & (int) byte.MaxValue) << 24 | ((int) buffer2[6] & (int) byte.MaxValue) << 16 | ((int) buffer2[5] & (int) byte.MaxValue) << 8 | (int) buffer2[4] & (int) byte.MaxValue;
        int num10 = ((int) buffer2[11] & (int) byte.MaxValue) << 24 | ((int) buffer2[10] & (int) byte.MaxValue) << 16 | ((int) buffer2[9] & (int) byte.MaxValue) << 8 | (int) buffer2[8] & (int) byte.MaxValue;
        int num11 = (int) buffer2[13];
        int num12 = (int) buffer2[12];
        int num13 = ((int) buffer2[15] & (int) byte.MaxValue) << 8 | (int) buffer2[14] & (int) byte.MaxValue;
        int num14 = (int) buffer2[19];
        int num15 = (int) buffer2[18];
        int num16 = (int) buffer2[17];
        int num17 = (int) buffer2[16];
        int num18 = ((int) buffer2[23] & (int) byte.MaxValue) << 24 | ((int) buffer2[22] & (int) byte.MaxValue) << 16 | ((int) buffer2[21] & (int) byte.MaxValue) << 8 | (int) buffer2[20] & (int) byte.MaxValue;
        int num19 = (int) buffer2[27];
        int num20 = (int) buffer2[26];
        int num21 = (int) buffer2[25];
        int num22 = (int) buffer2[24];
        int num23 = (int) buffer2[31];
        int num24 = (int) buffer2[30];
        int num25 = (int) buffer2[29];
        int num26 = (int) buffer2[28];
        int num27 = ((int) buffer2[35] & (int) byte.MaxValue) << 24 | ((int) buffer2[34] & (int) byte.MaxValue) << 16 | ((int) buffer2[33] & (int) byte.MaxValue) << 8 | (int) buffer2[32] & (int) byte.MaxValue;
        int num28 = (int) buffer2[39];
        int num29 = (int) buffer2[38];
        int num30 = (int) buffer2[37];
        int num31 = (int) buffer2[36];
        if (num13 != 8)
        {
          Console.WriteLine("Not a 256 color bitmap!");
        }
        else
        {
          int num32 = num27 <= 0 ? 1 << num13 : num27;
          if (num18 == 0)
            num18 = ((int) ((long) (num9 * num13 + 31) & 4294967264L) >> 3) * num10;
          byte[] buffer3 = new byte[num32 * 4];
          fileStream1.Read(buffer3, 0, num32 * 4);
          fileStream2.Write(this.aokpalette, 0, num32 * 4);
          int num33 = num18 / num10 - num9;
          this.rawpixels = new byte[(num9 + num33) * num10];
          fileStream1.Read(this.rawpixels, 0, (num9 + num33) * num10);
          this.outputpixels = new byte[(num9 + num33) * num10];
          for (int index = 0; index < this.outputpixels.Length; ++index)
          {
            int rawpixel = (int) this.rawpixels[index];
            if (rawpixel < 0)
              rawpixel += 256;
            this.outputpixels[index] = (byte) this.mapping[rawpixel];
          }
          fileStream2.Write(this.outputpixels, 0, (num9 + num33) * num10);
          fileStream1.Close();
          fileStream2.Close();
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Caught exception in savebitmap!" + ex?.ToString());
      }
    }

    internal virtual byte[] returnaokpalette() => this.aokpalette;

    internal virtual byte[][] returnaoktable() => this.aoktable;

    internal virtual byte[][] returnrawpixels()
    {
      int num = this.rawpixels.Length / this.imgheight;
      byte[][] numArray = imagehandler.RectangularArrays.RectangularbyteArray(this.imgheight, this.imgwidth);
      for (int index1 = 0; index1 < this.imgheight; ++index1)
      {
        for (int index2 = 0; index2 < this.imgwidth; ++index2)
          numArray[this.imgheight - index1 - 1][index2] = this.rawpixels[index1 * num + index2];
      }
      return numArray;
    }

    public virtual void copybitmap(string infilename, string outfilename)
    {
      try
      {
        FileStream fileStream1 = new FileStream(infilename, FileMode.Open, FileAccess.Read);
        FileStream fileStream2 = new FileStream(outfilename, FileMode.Create, FileAccess.Write);
        int length = (int) fileStream1.Length;
        byte[] buffer = new byte[length];
        fileStream1.Read(buffer, 0, length);
        fileStream2.Write(buffer, 0, length);
        fileStream1.Close();
        fileStream2.Close();
      }
      catch (Exception ex)
      {
        Console.WriteLine("Caught exception in copybitmap!" + ex?.ToString());
      }
    }

    internal static class RectangularArrays
    {
      public static byte[][] RectangularbyteArray(int size1, int size2)
      {
        byte[][] numArray = new byte[size1][];
        for (int index = 0; index < size1; ++index)
          numArray[index] = new byte[size2];
        return numArray;
      }
    }
  }
}
