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

        public int _a;

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
            bool xneg, yneg;
            Matrix rez;
            for (int i = 0; i < lines.Count; i++)
            {
                xneg = false;
                yneg = false;
                lines[i].start.X -= _x;
                lines[i].start.Y -= _y;
                lines[i].end.X -= _x;
                lines[i].end.Y -= _y;

                //if (lines[i].end.X < 0)
                //{
                //    lines[i].end.X = Math.Abs(lines[i].end.X);
                //    xneg = true;
                //}
                //if (lines[i].end.Y < 0)
                //{
                //    lines[i].end.Y = Math.Abs(lines[i].end.Y);
                //    yneg = true;
                //}

                rez = new Matrix(lines[i].end.X, lines[i].end.Y, lines[i].we);
                rez = rez.Multiple(T);
                lines[i].we = (int)(rez.matrix[0, 2]);
                lines[i].end.X = (int)(rez.matrix[0, 0] / lines[i].we);
                lines[i].end.Y = (int)(rez.matrix[0, 1] / lines[i].we);

                //if (xneg)
                //{
                //    lines[i].end.X = Convert.ToInt32(("-" + lines[i].end.X.ToString()));
                //    xneg = false;
                //}
                //if (yneg)
                //{
                //    lines[i].end.Y = Convert.ToInt32(("-" + lines[i].end.Y.ToString()));
                //    yneg = false;
                //}

                //if (lines[i].start.X < 0)
                //{
                //    lines[i].start.X = Math.Abs(lines[i].start.X);
                //    xneg = true;
                //}
                //if (lines[i].start.Y < 0)
                //{
                //    lines[i].start.Y = Math.Abs(lines[i].start.Y);
                //    yneg = true;
                //}

                rez = new Matrix(lines[i].start.X, lines[i].start.Y, lines[i].ws);
                rez = rez.Multiple(T);
                lines[i].ws = (int)(rez.matrix[0, 2]);
                lines[i].start.X = (int)(rez.matrix[0, 0] / lines[i].ws);
                lines[i].start.Y = (int)(rez.matrix[0, 1] / lines[i].ws);

                //if (xneg)
                //{
                //    lines[i].start.X = Convert.ToInt32(("-" + lines[i].start.X.ToString()));
                //    xneg = false;
                //}
                //if (yneg)
                //{
                //    lines[i].start.Y = Convert.ToInt32(("-" + lines[i].start.Y.ToString()));
                //    yneg = false;
                //}

                lines[i].start.X += _x;
                lines[i].start.Y += _y;
                lines[i].end.X += _x;
                lines[i].end.Y += _y;

            }
        }

        public void DrawTangentLine(Point p, double der, Graphics g)
        {
            const int length = 50;

            int y = (int)(p.Y + der * (p.X - length - p.X));
            Point start = new Point(p.X - length + _x, y + _y);

            y = (int)(p.Y + der * (p.X + length - p.X));
            Point end = new Point(p.X + length + _x, y + _y);

            Line tangent = new Line(new Pen(Brushes.Black),
                start,
                end);
            tangent.Draw(g);
        }

        public void DrawNormalLine(Point p, double der, Graphics g)
        {
            const int length = 50;

            int y = (int)(p.Y - 1 / der * (p.X - length - p.X));
            Point start = new Point(p.X - length + _x, y + _y);

            y = (int)(p.Y - 1 / der * (p.X + length - p.X));
            Point end = new Point(p.X + length + _x, y + _y);

            Line tangent = new Line(new Pen(Brushes.Black),
                start,
                end);
            tangent.Draw(g);
        }

        public Point FindPoint(int x)
        {
            foreach (Line l in lines)
            {
                if(l.start.X == x+_x)
                {
                    return l.start;
                }
                else
                {
                    if (l.end.X == x+_x)
                    {
                        return l.end;
                    }
                }
            }
            throw new Exception("No point with x = "+ x.ToString() + " has been found");
        }
    }
}
