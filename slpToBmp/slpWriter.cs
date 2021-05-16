// Decompiled with JetBrains decompiler
// Type: DllPatchAok20.slpWriter
// Assembly: DllPatchAok20, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C8909016-A2BC-428B-B1D1-F2128DD0464E
// Assembly location: C:\Users\m\Desktop\toolsAoe\DllPatchAok20.exe

using System;
using System.IO;
using System.Text;

namespace slpToBmp
{
  public class slpWriter
  {
    private int shadow = 131;
    private int outline1 = 8;
    private int outline2 = 124;
    private int outputmask;
    private string csvfile = "dunno";
    private bool cvtused;
    private bool plcolorused;
    public string maskfile = "";
    private int numframes;
    private frame[] frames;
    private int[] mapping;
    private string version;
    private string comment;

    public void initframes(int n)
    {
      this.numframes = n;
      this.frames = new frame[n];
    }

    public void Writemasks(string inputfile, string outputfile, int choice)
    {
      imagehandler imagehandler = new imagehandler();
      imagehandler.loadbitmap(inputfile, 0);
      byte[][] numArray = imagehandler.returnrawpixels();
      int length1 = numArray[0].Length;
      int length2 = numArray.Length;
      int[][] picture = slpWriter.RectangularArrays.RectangularIntArray(length2, length1);
      for (int index1 = 0; index1 < length2; ++index1)
      {
        for (int index2 = 0; index2 < length1; ++index2)
          picture[index1][index2] = (int) numArray[index1][index2] & (int) byte.MaxValue;
      }
      int num = picture[length2 - 1][0];
      switch (choice)
      {
        case 1:
          for (int index1 = 0; index1 < length2; ++index1)
          {
            for (int index2 = 0; index2 < length1; ++index2)
              picture[index1][index2] = picture[index1][index2] != num ? (int) byte.MaxValue : this.outputmask;
          }
          break;
        case 2:
          for (int index1 = 0; index1 < length2; ++index1)
          {
            for (int index2 = 0; index2 < length1 - 1; ++index2)
            {
              if (picture[index1][index2] == num && picture[index1][index2 + 1] != num && index2 + 1 != length1 - 1)
                picture[index1][index2] = -2;
            }
            for (int index2 = length1 - 1; index2 > 0; --index2)
            {
              if (picture[index1][index2] == num && picture[index1][index2 - 1] != num && index2 + 1 != length1 - 1)
                picture[index1][index2] = -2;
            }
          }
          for (int index1 = 0; index1 < length1; ++index1)
          {
            for (int index2 = 0; index2 <= length2 - 2; ++index2)
            {
              if (picture[index2][index1] == num && picture[index2 + 1][index1] != num && picture[index2 + 1][index1] != -2)
                picture[index2][index1] = -2;
            }
            for (int index2 = length2 - 1; index2 > 0; --index2)
            {
              if (picture[index2][index1] == num && picture[index2 - 1][index1] != num && picture[index2 - 1][index1] != -2)
                picture[index2][index1] = -2;
            }
          }
          for (int index1 = 0; index1 < length2; ++index1)
          {
            for (int index2 = 0; index2 < length1; ++index2)
            {
              if (picture[index1][index2] != -2)
                picture[index1][index2] = this.outputmask;
            }
          }
          break;
        case 3:
          for (int index1 = 0; index1 < length2; ++index1)
          {
            for (int index2 = 0; index2 < length1; ++index2)
            {
              if (picture[index1][index2] < 16 || picture[index1][index2] > 23)
                picture[index1][index2] = this.outputmask;
            }
          }
          break;
        case 4:
          for (int index1 = 0; index1 < length2; ++index1)
          {
            for (int index2 = 0; index2 < length1; ++index2)
              picture[index1][index2] = picture[index1][index2] != 131 ? this.outputmask : 131;
          }
          break;
        case 5:
          for (int index1 = 0; index1 < length2; ++index1)
          {
            for (int index2 = 0; index2 < length1 - 1; ++index2)
            {
              if (picture[index1][index2] == num && picture[index1][index2 + 1] != num && index2 + 1 != length1 - 1)
                picture[index1][index2] = -2;
            }
            for (int index2 = length1 - 1; index2 > 0; --index2)
            {
              if (picture[index1][index2] == num && picture[index1][index2 - 1] != num && index2 + 1 != length1 - 1)
                picture[index1][index2] = -2;
            }
          }
          for (int index1 = 0; index1 < length1; ++index1)
          {
            for (int index2 = 0; index2 <= length2 - 2; ++index2)
            {
              if (picture[index2][index1] == num && picture[index2 + 1][index1] != num && picture[index2 + 1][index1] != -2)
                picture[index2][index1] = -2;
            }
            for (int index2 = length2 - 2; index2 > 0; --index2)
            {
              if (picture[index2][index1] == num && picture[index2 - 1][index1] != num && picture[index2 - 1][index1] != -2)
                picture[index2][index1] = -2;
            }
          }
          for (int index1 = 0; index1 < length2; ++index1)
          {
            for (int index2 = 0; index2 < length1 - 1; ++index2)
            {
              if (picture[index1][index2] == num && picture[index1][index2 + 1] != num && index2 + 1 != length1 - 1)
                picture[index1][index2] = -3;
            }
            for (int index2 = length1 - 1; index2 > 0; --index2)
            {
              if (picture[index1][index2] == num && picture[index1][index2 - 1] != num && index2 + 1 != length1 - 1)
                picture[index1][index2] = -3;
            }
          }
          for (int index1 = 0; index1 < length1; ++index1)
          {
            for (int index2 = 0; index2 <= length2 - 2; ++index2)
            {
              if (picture[index2][index1] == num && picture[index2 + 1][index1] != num && picture[index2 + 1][index1] != -3)
                picture[index2][index1] = -3;
            }
            for (int index2 = length2 - 2; index2 > 0; --index2)
            {
              if (picture[index2][index1] == num && picture[index2 - 1][index1] != num && picture[index2 - 1][index1] != -3)
                picture[index2][index1] = -3;
            }
          }
          for (int index1 = 0; index1 < length2; ++index1)
          {
            for (int index2 = 0; index2 < length1; ++index2)
            {
              if (picture[index1][index2] != -3)
                picture[index1][index2] = this.outputmask;
            }
          }
          break;
        default:
          Console.WriteLine("Unrecognized command");
          break;
      }
      new Aokbitmap(this.outputmask, this.outline1, this.outline2, this.shadow).Write(outputfile, picture, numArray[0].Length, numArray.Length);
    }

    public virtual void getframe(
      int num,
      string path,
      string filename,
      bool msk,
      bool o1,
      bool o2,
      bool pl,
      bool sh)
    {
      imagehandler imagehandler = new imagehandler();
      imagehandler.loadbitmap(path.ToString() + filename, 0);
      byte[][] numArray1 = imagehandler.returnrawpixels();
      int length1 = numArray1[0].Length;
      int length2 = numArray1.Length;
      int[][] numArray2 = slpWriter.RectangularArrays.RectangularIntArray(length2, length1);
      for (int index1 = 0; index1 < length2; ++index1)
      {
        for (int index2 = 0; index2 < length1; ++index2)
          numArray2[index1][index2] = (int) numArray1[index1][index2] & (int) byte.MaxValue;
      }
      if (msk)
      {
        string maskfile = this.maskfile;
        if (File.Exists(maskfile))
          imagehandler.loadbitmap(maskfile, 0);
        byte[][] numArray3 = imagehandler.returnrawpixels();
        for (int index1 = 0; index1 < length2; ++index1)
        {
          for (int index2 = 0; index2 < length1; ++index2)
          {
            if (((int) numArray3[index1][index2] & (int) byte.MaxValue) == 0)
              numArray2[index1][index2] = -1;
          }
        }
      }
      if (o1)
      {
        string filename1 = path.ToString() + "U" + filename.Substring(1);
        imagehandler.loadbitmap(filename1, 0);
        byte[][] numArray3 = imagehandler.returnrawpixels();
        for (int index1 = 0; index1 < length2; ++index1)
        {
          for (int index2 = 0; index2 < length1; ++index2)
          {
            if (((int) numArray3[index1][index2] & (int) byte.MaxValue) != 0)
              numArray2[index1][index2] = -2;
          }
        }
      }
      if (o2)
      {
        string filename1 = path.ToString() + "O" + filename.Substring(1);
        imagehandler.loadbitmap(filename1, 0);
        byte[][] numArray3 = imagehandler.returnrawpixels();
        for (int index1 = 0; index1 < length2; ++index1)
        {
          for (int index2 = 0; index2 < length1; ++index2)
          {
            if (((int) numArray3[index1][index2] & (int) byte.MaxValue) != 0)
              numArray2[index1][index2] = -3;
          }
        }
      }
      if (pl)
      {
        string filename1 = path.ToString() + "P" + filename.Substring(1);
        imagehandler.loadbitmap(filename1, 0);
        byte[][] numArray3 = imagehandler.returnrawpixels();
        for (int index1 = 0; index1 < length2; ++index1)
        {
          for (int index2 = 0; index2 < length1; ++index2)
          {
            int num1 = (int) numArray3[index1][index2] & (int) byte.MaxValue;
            if (num1 != 0)
              numArray2[index1][index2] = num1;
          }
        }
        this.plcolorused = true;
      }
      if (sh)
      {
        string filename1 = path.ToString() + "H" + filename.Substring(1);
        imagehandler.loadbitmap(filename1, 0);
        byte[][] numArray3 = imagehandler.returnrawpixels();
        Console.WriteLine("Entering shadow stuff");
        for (int index1 = 0; index1 < length2; ++index1)
        {
          for (int index2 = 0; index2 < length1; ++index2)
          {
            if (((int) numArray3[index1][index2] & (int) byte.MaxValue) != 0)
              numArray2[index1][index2] = -4;
          }
        }
      }
      if (this.frames == null)
        this.frames = new frame[num + 1];
      this.frames[num] = new frame();
      this.frames[num].width = length1;
      this.frames[num].height = length2;
      this.frames[num].picture = numArray2;
      if (!this.cvtused)
        return;
      this.frames[num].convertcvt(this.mapping);
    }

    public virtual void convertcvt(string cvtname)
    {
      try
      {
        this.cvtused = true;
        StreamReader streamReader = new StreamReader(cvtname);
        this.mapping = new int[256];
        for (int index = 0; index < 256; ++index)
          this.mapping[index] = -11;
        if (streamReader.ReadLine().Equals("<MPS PALETTE REMAP>"))
          Console.WriteLine("Recognized as MPS cvt file");
        else
          Console.WriteLine("Not recognized as MPS cvt file");
        string str;
        while ((str = streamReader.ReadLine()) != null)
        {
          int length = str.IndexOf(" ", StringComparison.Ordinal);
          this.mapping[int.Parse(str.Substring(0, length))] = int.Parse(str.Substring(length + 1));
        }
        streamReader.Close();
      }
      catch (Exception ex)
      {
        Console.WriteLine("Caught exception in applying CVT! ");
        Console.WriteLine(ex.ToString());
        Console.Write(ex.StackTrace);
      }
    }

    public virtual void compress()
    {
      for (int index1 = 0; index1 < this.numframes; ++index1)
      {
        this.frames[index1].initrowedge(this.frames[index1].height);
        for (int index2 = 0; index2 < this.frames[index1].height; ++index2)
        {
          int l = 0;
          for (int index3 = 0; index3 < this.frames[index1].width && this.frames[index1].picture[index2][index3] == -1; ++index3)
            ++l;
          int r = 0;
          for (int index3 = this.frames[index1].width - 1; index3 >= 0 && this.frames[index1].picture[index2][index3] == -1; --index3)
            ++r;
          if (l == this.frames[index1].width)
            r = 0;
          this.frames[index1].edges[index2] = new rowedge(l, r);
        }
        this.frames[index1].initcommands();
        for (int index2 = 0; index2 < this.frames[index1].height; ++index2)
        {
          int num1 = -5;
          int left = this.frames[index1].edges[index2].left;
          int num2 = this.frames[index1].width - this.frames[index1].edges[index2].right - 1;
          int index3 = left;
          int n = 0;
          string str = "null";
          int length = -1;
          int num3 = -1;
          for (; index3 <= num2; ++index3)
          {
            int num4 = this.frames[index1].picture[index2][index3];
            if (num4 != num1 && num4 < 0)
            {
              if (!str.Equals("null") && !str.Equals("color"))
              {
                if (str.Equals("skip"))
                  this.frames[index1].cmdskip(n);
                else if (str.Equals("outline1"))
                  this.frames[index1].cmdoutline1(n);
                else if (str.Equals("outline2"))
                  this.frames[index1].cmdoutline2(n);
                else if (str.Equals("shadowpix"))
                  this.frames[index1].cmdshadowpix(n);
              }
              if (str.Equals("color"))
              {
                byte[] array = new byte[length];
                for (int index4 = 0; index4 < length; ++index4)
                  array[index4] = (byte) this.frames[index1].picture[index2][index4 + num3];
                this.frames[index1].cmdcolorblock(array, this.plcolorused);
                length = -1;
                num3 = -1;
              }
              switch (num4)
              {
                case -4:
                  str = "shadowpix";
                  break;
                case -3:
                  str = "outline2";
                  break;
                case -2:
                  str = "outline1";
                  break;
                case -1:
                  str = "skip";
                  break;
                default:
                  Console.WriteLine("Unknown pixel");
                  break;
              }
              num1 = num4;
              n = 1;
            }
            else if (num4 != num1 && num1 < 0)
            {
              if (!str.Equals("null"))
              {
                if (str.Equals("skip"))
                  this.frames[index1].cmdskip(n);
                else if (str.Equals("outline1"))
                  this.frames[index1].cmdoutline1(n);
                else if (str.Equals("outline2"))
                  this.frames[index1].cmdoutline2(n);
                else if (str.Equals("shadowpix"))
                  this.frames[index1].cmdshadowpix(n);
              }
              str = "color";
              num1 = num4;
              n = 1;
              num3 = index3;
              length = 1;
            }
            else if (str.Equals("color"))
            {
              ++length;
              num1 = num4;
              n = 1;
            }
            else
              ++n;
          }
          if (str.Equals("color"))
          {
            byte[] array = new byte[length];
            for (int index4 = 0; index4 < length; ++index4)
              array[index4] = (byte) this.frames[index1].picture[index2][index4 + num3];
            this.frames[index1].cmdcolorblock(array, this.plcolorused);
          }
          else if (str.Equals("skip"))
            this.frames[index1].cmdskip(n);
          else if (str.Equals("outline1"))
            this.frames[index1].cmdoutline1(n);
          else if (str.Equals("outline2"))
            this.frames[index1].cmdoutline2(n);
          else if (str.Equals("shadowpix"))
            this.frames[index1].cmdshadowpix(n);
          this.frames[index1].cmdeol();
        }
      }
    }

    public virtual void Write(string filename)
    {
      try
      {
        this.version = "2.0N";
        this.comment = "ArtDesk 1.00 SLP Writer ";
        Console.WriteLine("Number of frames " + this.numframes.ToString());
        FileStream fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write);
        BinaryWriter binaryWriter = new BinaryWriter((Stream) fileStream);
        byte[] bytes1 = Encoding.ASCII.GetBytes(this.version);
        binaryWriter.Write(bytes1);
        byte[] buffer1 = this.bytefromint(this.numframes);
        binaryWriter.Write(buffer1);
        byte[] bytes2 = Encoding.ASCII.GetBytes(this.comment);
        binaryWriter.Write(bytes2);
        int num1 = 32 + 32 * this.numframes;
        for (int index = 0; index < this.numframes; ++index)
        {
          this.frames[index].frame_outline_offset = num1;
          this.frames[index].computecommandbytes();
          int num2 = num1 + this.frames[index].height * 4;
          this.frames[index].frame_data_offsets = num2;
          num1 = num2 + this.frames[index].height * 4;
          for (int n = 0; n < this.frames[index].height; ++n)
          {
            this.frames[index].commandoff[n] = num1;
            num1 += this.frames[index].linecommandLength(n);
          }
        }
        for (int index = 0; index < this.numframes; ++index)
          this.frames[index].setanchor(0, 0);
        for (int index = 0; index < this.numframes; ++index)
        {
          this.frames[index].computeframeheader();
          byte[] buffer2 = this.bytefromint(this.frames[index].frame_data_offsets);
          binaryWriter.Write(buffer2);
          byte[] buffer3 = this.bytefromint(this.frames[index].frame_outline_offset);
          binaryWriter.Write(buffer3);
          byte[] buffer4 = this.bytefromint(this.frames[index].palette_offset);
          binaryWriter.Write(buffer4);
          byte[] buffer5 = this.bytefromint(this.frames[index].properties);
          binaryWriter.Write(buffer5);
          byte[] buffer6 = this.bytefromint(this.frames[index].width);
          binaryWriter.Write(buffer6);
          byte[] buffer7 = this.bytefromint(this.frames[index].height);
          binaryWriter.Write(buffer7);
          byte[] buffer8 = this.bytefromint(this.frames[index].anchorx);
          binaryWriter.Write(buffer8);
          byte[] buffer9 = this.bytefromint(this.frames[index].anchory);
          binaryWriter.Write(buffer9);
        }
        for (int index1 = 0; index1 < this.numframes; ++index1)
        {
          for (int index2 = 0; index2 < this.frames[index1].height; ++index2)
          {
            byte[] buffer2 = this.bytefromshort(this.frames[index1].edges[index2].left);
            byte[] buffer3 = this.bytefromshort(this.frames[index1].edges[index2].right);
            binaryWriter.Write(buffer2);
            binaryWriter.Write(buffer3);
          }
          for (int index2 = 0; index2 < this.frames[index1].height; ++index2)
          {
            byte[] buffer2 = this.bytefromint(this.frames[index1].commandoff[index2]);
            binaryWriter.Write(buffer2);
          }
          for (int index2 = 0; index2 < this.frames[index1].numcommands; ++index2)
          {
            string type = this.frames[index1].commands[index2].type;
            if (type.Equals("one"))
            {
              byte[] buffer2 = new byte[1]
              {
                this.frames[index1].commands[index2].cmdbyte
              };
              binaryWriter.Write(buffer2);
            }
            else if (type.Equals("two Length"))
            {
              byte[] buffer2 = new byte[2]
              {
                this.frames[index1].commands[index2].cmdbyte,
                this.frames[index1].commands[index2].next_byte
              };
              binaryWriter.Write(buffer2);
            }
            else if (type.Equals("two data"))
            {
              byte[] buffer2 = new byte[1]
              {
                this.frames[index1].commands[index2].cmdbyte
              };
              binaryWriter.Write(buffer2);
              binaryWriter.Write(this.frames[index1].commands[index2].data);
            }
            else if (type.Equals("three"))
            {
              byte[] buffer2 = new byte[2]
              {
                this.frames[index1].commands[index2].cmdbyte,
                this.frames[index1].commands[index2].next_byte
              };
              binaryWriter.Write(buffer2);
              binaryWriter.Write(this.frames[index1].commands[index2].data);
            }
            else
              Console.WriteLine("Whoa, unrecognized type");
          }
        }
        binaryWriter.Close();
        fileStream.Close();
      }
      catch (Exception ex)
      {
        Console.WriteLine("Caught exception in Write slp! ");
        Console.WriteLine(ex.ToString());
        Console.Write(ex.StackTrace);
      }
    }

    internal virtual byte[] bytefromint(int i) => new byte[4]
    {
      (byte) i,
      (byte) (i >> 8),
      (byte) (i >> 16),
      (byte) (i >> 24)
    };

    internal virtual byte[] bytefromshort(int i) => new byte[2]
    {
      (byte) i,
      (byte) (i >> 8)
    };

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
