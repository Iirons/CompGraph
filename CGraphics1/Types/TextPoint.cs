using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGraphics1.Types
{
    public class TextPoint
    {
        public string text;
        public Point point;
        public Font font = new Font("Arial", 10);
        public double w;

        public TextPoint(string txt, Point pnt)
        {
            text = txt;
            point = pnt;
            w = 1;
        }

        public void Draw(Graphics g)
        {
            g.DrawString(text, font, Brushes.Black, point);
        }
    }
}
