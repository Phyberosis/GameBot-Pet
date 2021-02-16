using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using OSEnvironment;
namespace GameBot_pet
{
    class Program
    {
        static void Main(string[] args)
        {
            WindowManager w = new WindowManager();
            var l = w.FindWindows(new string[] { "discord", "youtube" });

            foreach (Process p in w.ListWindows())
            {
                Console.WriteLine(p.MainWindowTitle);
            }

            Console.WriteLine($"found {l}");
            for (int i = 0; i<l; i++)
            {
                Thread.Sleep(500);
                var img = w.CaptureWindow(i);
                img.Save($"test{i}.bmp");
                Console.WriteLine(i);
            }

            Console.ReadLine();
        }
    }
}
