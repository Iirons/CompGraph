using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CGraphics1.Types
{
    public class CurveB3
    {
        public Point A, B, C, D;

        public CurveB3()
        {

        }

        public CurveB3(Point A1, Point B1, Point C1, Point D1, Pen pen1)
        {
            A = A1; B = B1; C = C1; D = D1; pen = pen1;
        }

        public Pen pen;

        public void Draw(Graphics g)
        {
            Point p1 = A, p2 = new Point();
            const double step = 0.1;
            try
            {
                for(double t = step; t < 1; t += step)
                {
                    p2.X = (int)(Math.Pow((1 - t), 3) * A.X + 3 * t * Math.Pow((1 - t), 2) * B.X 
                        + 3 * t * t * (1 - t) * C.X + t * t * t * D.X);
                    p2.Y = (int)(Math.Pow((1 - t), 3) * A.Y + 3 * t * Math.Pow((1 - t), 2) * B.Y
                        + 3 * t * t * (1 - t) * C.Y + t * t * t * D.Y);
                    g.DrawLine(pen, p1, p2);
                    p1 = p2;
                }
            }
            catch(Exception r)
            {
                MessageBox.Show(r.Message);
            }
        }

    }
}
