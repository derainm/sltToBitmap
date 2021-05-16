// Decompiled with JetBrains decompiler
// Type: DllPatchAok20.Aokbitmap
// Assembly: DllPatchAok20, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C8909016-A2BC-428B-B1D1-F2128DD0464E
// Assembly location: C:\Users\m\Desktop\toolsAoe\DllPatchAok20.exe

using System;
using System.IO;

namespace slpToBmp
{
  internal class Aokbitmap
  {
    public const int BITMAPFILEHEADER_SIZE = 14;
    public const int BITMAPINFOHEADER_SIZE = 40;
    public byte[] bitmapFileHeader = new byte[14];
    public byte[] bfType = new byte[2]
    {
      (byte) 66,
      (byte) 77
    };
    public int bfSize;
    public int bfReserved1;
    public int bfReserved2;
    public int bfOffset = 54;
    public byte[] bitmapInfoHeader = new byte[40];
    public int biSize = 40;
    public int biWidth;
    public int biHeight;
    public int biPlanes = 1;
    public int biBitCount = 8;
    public int biCompression;
    public int biSizeImage = 196608;
    public int biXPelsPerMeter;
    public int biYPelsPerMeter;
    public int biClrUsed;
    public int biClrImportant;
    public byte[] colortable;
    public byte[] bitmap;
    public FileStream fo;
    public int mask;
    public int outline1;
    public int outline2;
    public int shadow;
    public string sample;

    internal Aokbitmap()
    {
      this.mask = 244;
      this.outline1 = this.outline2 = 251;
      this.shadow = 13;
      this.sample = "50500.bmp";
    }

    internal Aokbitmap(int m, int o1, int o2, int sh)
    {
      this.mask = m;
      this.outline1 = o1;
      this.outline2 = o2;
      this.shadow = sh;
      this.sample = "50500.bmp";
    }

    internal virtual void Write(string outputfile, int[][] picture, int width, int height)
    {
      try
      {
        this.fo = new FileStream(outputfile, FileMode.Create, FileAccess.Write);
        this.convertimage(picture, width, height);
        BinaryWriter binaryWriter = new BinaryWriter((Stream) this.fo);
        binaryWriter.Write(this.bfType);
        binaryWriter.Write(this.intToDWord(this.bfSize));
        binaryWriter.Write(this.intToWord(this.bfReserved1));
        binaryWriter.Write(this.intToWord(this.bfReserved2));
        binaryWriter.Write(this.intToDWord(this.bfOffset));
        binaryWriter.Write(this.intToDWord(this.biSize));
        binaryWriter.Write(this.intToDWord(this.biWidth));
        binaryWriter.Write(this.intToDWord(this.biHeight));
        binaryWriter.Write(this.intToWord(this.biPlanes));
        binaryWriter.Write(this.intToWord(this.biBitCount));
        binaryWriter.Write(this.intToDWord(this.biCompression));
        binaryWriter.Write(this.intToDWord(this.biSizeImage));
        binaryWriter.Write(this.intToDWord(this.biXPelsPerMeter));
        binaryWriter.Write(this.intToDWord(this.biYPelsPerMeter));
        binaryWriter.Write(this.intToDWord(this.biClrUsed));
        binaryWriter.Write(this.intToDWord(this.biClrImportant));
        binaryWriter.Write(this.colortable);
        binaryWriter.Write(this.bitmap);
        binaryWriter.Close();
        this.fo.Close();
        this.fo.Dispose();
      }
      catch (Exception ex)
      {
        Console.WriteLine("Caught exception in saving bitmap!" + ex?.ToString());
      }
    }

    internal virtual void convertimage(int[][] picture, int width, int height)
    {
      this.bfOffset = 1078;
      int num1 = (4 - width % 4) * height;
      if (4 - width % 4 == 4)
        num1 = 0;
      this.biSizeImage = width * height + num1;
      this.bfSize = this.biSizeImage + 14 + 40 + 1024;
      this.biWidth = width;
      this.biHeight = height;
      imagehandler imagehandler = new imagehandler();
      imagehandler.sampleused = this.sample;
      imagehandler.loadbitmap(this.sample, 1);
      this.colortable = imagehandler.returnaokpalette();
      int num2 = 4 - width % 4;
      if (num2 == 4)
        num2 = 0;
      this.bitmap = new byte[(width + num2) * height];
      int index1 = 0;
      for (int index2 = height - 1; index2 >= 0; --index2)
      {
        for (int index3 = 0; index3 < width; ++index3)
        {
          int num3 = picture[index2][index3];
          if (num3 == -1)
            num3 = this.mask;
          if (num3 == -2)
            num3 = this.outline1;
          if (num3 == -3)
            num3 = this.outline2;
          if (num3 == -4)
            num3 = this.shadow;
          this.bitmap[index1] = (byte) num3;
          ++index1;
        }
        for (int index3 = 0; index3 < num2; ++index3)
        {
          this.bitmap[index1] = (byte) 0;
          ++index1;
        }
      }
    }

    public void WriteBitmapFileHeader()
    {
    }

    public void WriteBitmapInfoHeader()
    {
      try
      {
        BinaryWriter binaryWriter = new BinaryWriter((Stream) this.fo);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
        Console.Write(ex.StackTrace);
      }
    }

    public void Writecolortable()
    {
      try
      {
        BinaryWriter binaryWriter = new BinaryWriter((Stream) this.fo);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
        Console.Write(ex.StackTrace);
      }
    }

    public void WriteBitmap()
    {
      try
      {
        BinaryWriter binaryWriter = new BinaryWriter((Stream) this.fo);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
        Console.Write(ex.StackTrace);
      }
    }

    public byte[] intToWord(int parValue) => new byte[2]
    {
      (byte) (parValue & (int) byte.MaxValue),
      (byte) (parValue >> 8 & (int) byte.MaxValue)
    };

    public byte[] intToDWord(int parValue) => new byte[4]
    {
      (byte) (parValue & (int) byte.MaxValue),
      (byte) (parValue >> 8 & (int) byte.MaxValue),
      (byte) (parValue >> 16 & (int) byte.MaxValue),
      (byte) (parValue >> 24 & (int) byte.MaxValue)
    };
  }
}
