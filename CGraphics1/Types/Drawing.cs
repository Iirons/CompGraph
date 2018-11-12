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

namespace CGraphics1.Lab1
{
    public class Drawing
    {
        public int _x = 20, _y = 20;

        public Drawing()
        {
        }

        public List<Line> lines = new List<Line>();

        public List<TextPoint> textpoints = new List<TextPoint>();

        public List<Line> GetCircle(Point c, int R, double angle1, double angle2)
        {
            List<Line> rez = new List<Line>();
            const double CircleAngles = 0.0174533;
            double currangle = angle1 + CircleAngles;
            Point p1 = new Point(Convert.ToInt32(c.X + (R * Math.Cos(angle1))),
                Convert.ToInt32(c.Y + (R * Math.Sin(angle1))));

            Point p2 = new Point();
            while (currangle <= angle2)
            {
                p2.X = Convert.ToInt32(c.X + (R * Math.Cos(currangle)));
                p2.Y = Convert.ToInt32(c.Y + (R * Math.Sin(currangle)));
                rez.Add(new Line(new Pen(Brushes.Black, 1), p1, p2));
                p1 = p2;
                currangle += CircleAngles;
            }

            return rez;
        }

        public void DrawCircle(Graphics g, Pen pen, Point c, int R, double angle1, double angle2)
        {
            const double CircleAngles = 0.0174533;
            double currangle = angle1 + CircleAngles;
            Point p1 = new Point(Convert.ToInt32(c.X + (R * Math.Cos(angle1))),
                Convert.ToInt32(c.Y + (R * Math.Sin(angle1))));
            Point p2 = new Point();
            while (currangle <= angle2)
            {

                p2.X = Convert.ToInt32(c.X + (R * Math.Cos(currangle)));
                p2.Y = Convert.ToInt32(c.Y + (R * Math.Sin(currangle)));
                g.DrawLine(pen, p1, p2);
                p1 = p2;
                currangle += CircleAngles;
            }
        }

        public void Draw(Graphics g)
        {
            foreach(Line l in lines)
            {
                l.Draw(g);
            }

            foreach(TextPoint txt in textpoints)
            {
                txt.Draw(g);
            }
        }

        public void DrawAnim(Graphics g,PictureBox modelview)
        {
            foreach (Line l in lines)
            {
                l.Draw(g);
                modelview.Refresh();
            }

            foreach (TextPoint txt in textpoints)
            {
                txt.Draw(g);
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
                lines[i].end.X = (int)(rez.matrix[0, 0]/lines[i].we);
                lines[i].end.Y = (int)(rez.matrix[0, 1] / lines[i].we);
                


                rez = new Matrix(lines[i].start.X, lines[i].start.Y, lines[i].ws);
                rez = rez.Multiple(T);
                lines[i].ws = (int)(rez.matrix[0, 2]);
                lines[i].start.X = (int)(rez.matrix[0, 0]/ lines[i].ws);
                lines[i].start.Y = (int)(rez.matrix[0, 1]/ lines[i].ws);
                

                lines[i].start.X += _x;
                lines[i].start.Y += _y;
                lines[i].end.X += _x;
                lines[i].end.Y += _y;

            }

            foreach (TextPoint p in textpoints)
            {
                p.point.X -= _x;
                p.point.Y -= _y;

                rez = new Matrix(p.point.X, p.point.Y, p.w);
                rez = rez.Multiple(T);
                p.point.X = (int)(rez.matrix[0, 0]);
                p.point.Y = (int)(rez.matrix[0, 1]);
                p.w = (int)(rez.matrix[0, 2]);

                p.point.X += _x;
                p.point.Y += _y;
            }
            
        }
    }
}
