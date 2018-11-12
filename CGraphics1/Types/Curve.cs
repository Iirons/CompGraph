using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CGraphics1.Types;

namespace CGraphics1.Types
{
    public class Curve
    {
        public int _x = 320, _y = 270;

        public List<Line> lines = new List<Line>();

        public List<Line> GetCurve(int a)
        {
            const int xrange = 500;
            List<Line> rez = new List<Line>();

            int x = -xrange;
            double y = (Math.Pow(a, 3) / (Math.Pow(a, 2) + Math.Pow(x, 2)));
            Point p1 = new Point(x+_x, (int)y+_y);
            Point p2 = new Point();
            while (x < xrange)
            {
                x++;
                p2.X = x+_x;
                p2.Y = (int)(Math.Pow(a, 3) / (Math.Pow(a, 2) + Math.Pow(x, 2)))+_y;
                rez.Add(new Line(new Pen(Brushes.Black, 1), p1, p2));
                p1 = p2;
                
            }

            return rez;
        }
        public void Draw(Graphics g)
        {
            foreach (Line l in lines)
            {
                l.Draw(g);
            }
        }

        public void DrawAnim(Graphics g, PictureBox modelview)
        {
            foreach (Line l in lines)
            {

                l.Draw(g);
                modelview.Refresh();
            }
        }

        public void Transform(Matrix T)
        {
            
            Matrix rez;
            for (int i = 0; i < lines.Count; i++)
            {
                lines[i].start.X -= _x;
                lines[i].start.Y -= _y;
                lines[i].end.X -= _x;
                lines[i].end.Y -= _y;

                rez = new Matrix(lines[i].end.X, lines[i].end.Y, lines[i].we);
                rez = rez.Multiple(T);
                lines[i].we = (int)(rez.matrix[0, 2]);
                lines[i].end.X = (int)(rez.matrix[0, 0] / lines[i].we);
                lines[i].end.Y = (int)(rez.matrix[0, 1] / lines[i].we);



                rez = new Matrix(lines[i].start.X, lines[i].start.Y, lines[i].ws);
                rez = rez.Multiple(T);
                lines[i].ws = (int)(rez.matrix[0, 2]);
                lines[i].start.X = (int)(rez.matrix[0, 0] / lines[i].ws);
                lines[i].start.Y = (int)(rez.matrix[0, 1] / lines[i].ws);


                lines[i].start.X += _x;
                lines[i].start.Y += _y;
                lines[i].end.X += _x;
                lines[i].end.Y += _y;

            }
        }
    }
}
