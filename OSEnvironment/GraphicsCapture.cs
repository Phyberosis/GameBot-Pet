using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace OSEnvironment
{
    public class GraphicsCapture
    {
        public Bitmap Img { get; private set; }

        public GraphicsCapture() : this(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height)
        { }

        public GraphicsCapture(int x, int y, int w, int h) : this(new Point(x, y), new Size(w, h))
        { }

        public GraphicsCapture(Point loc, Size s)
        {
            Img = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(Img);
            g.CopyFromScreen(loc.X, loc.Y, 0, 0, s);
        }

        //public void Dispose()
        //{
        //    Img = null;
        //}
    }
}
