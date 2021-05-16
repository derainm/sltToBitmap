using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace slpToBmp
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args[0]!=null)
                slpToBmp(args[0].ToString());   

        }

        private static void slpToBmp(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            slpReader slpname = new slpReader();
 
            foreach(var f in di.GetFiles())
            {
                slpname = new slpReader();
                //set mask check if exist
                slpname.sample = "50500.bmp";
                if (f.Extension == ".slp")
                {
                    
                    slpname.name = f.Name.Split('.').First();
                    slpname.Read(f.FullName);
                    slpname.save(f.DirectoryName+"\\");
                } 
            }
        }
    }
}
