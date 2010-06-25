using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace Convert2Ico
{
    class Program
    {
        static void Main(string[] args)
        {
            string inFilename = args[0];
            string outFilename = args[1];
            Bitmap bitmap = new Bitmap(inFilename);
            IconConverter convertor = new IconConverter();
            Icon icon = Icon.FromHandle(bitmap.GetHicon());
            FileStream fs = new FileStream(outFilename, FileMode.Create);
            icon.Save(fs);
            fs.Close();
            Console.WriteLine("Done.");
        }
    }
}
