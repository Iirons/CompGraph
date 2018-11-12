using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGraphics1.Types
{
    public class Line
    {
        public Point start;
        public Point end;

        public double ws,we;

        public Pen pen;

        public Line(Pen pen1,Point st, Point en)
        {
            start = st;
            end = en;
            pen = pen1;
            ws = 1;
            we = 1;
        }

        public void Draw(Graphics g)
        {
            g.DrawLine(pen, start, end);
        }
    }
}
