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
            //for (int i = 0; i < circle1.Count - 1; i++)
            //{
            //    g.DrawLine(simple_pen, circle1[i], circle1[i + 1]);
            //}
            //for (int i = 0; i < circle2.Count - 1; i++)
            //{
            //    g.DrawLine(simple_pen, circle2[i], circle2[i + 1]);
            //}

            //for (int i = 0; i < arc1.Count - 1; i++)
            //{
            //    g.DrawLine(simple_pen, arc1[i], arc1[i + 1]);
            //}
            //for (int i = 0; i < arc2.Count - 1; i++)
            //{
            //    g.DrawLine(simple_pen, arc2[i], arc2[i + 1]);
            //}
            //for (int i = 0; i < arc3.Count - 1; i++)
            //{
            //    g.DrawLine(simple_pen, arc3[i], arc3[i + 1]);
            //}
            //for (int i = 0; i < arc4.Count - 1; i++)
            //{
            //    g.DrawLine(simple_pen, arc4[i], arc4[i + 1]);
            //}

            //g.DrawLine(simple_pen, p1, p2);
            //g.DrawLine(simple_pen, p3, p4);
            //g.DrawLine(simple_pen, p5, p6);
            //g.DrawLine(simple_pen, p7, p8);

            //g.DrawLine(simple_pen, p1, p5);
            //g.DrawLine(simple_pen, p6, p3);
            //g.DrawLine(simple_pen, p4, p8);
            //g.DrawLine(simple_pen, p7, p2);

            //g.DrawLine(simple_pen, p5, p9);
            //g.DrawLine(simple_pen, p6, p9);

            //g.DrawLine(simple_pen, p3, p10);
            //g.DrawLine(simple_pen, p4, p10);

            //g.DrawLine(simple_pen, p7, p11);
            //g.DrawLine(simple_pen, p8, p11);

            //g.DrawLine(simple_pen, p1, p12);
            //g.DrawLine(simple_pen, p2, p12);

            //Drawing axis for circles
            //g.DrawLine(dashed_pen, AxisP1, AxisP2);

            //g.DrawLine(dashed_pen, AxisP3, AxisP4);
        }

        //public void DrawGridAndAxis(Graphics g)
        //{
        //    //g.DrawString("50", fnt, Brushes.Black, size1);
        //    //g.DrawString("50", fnt, Brushes.Black, size2);
        //    //g.DrawLine(arrow_pen, AxisX0, AxisXN);

        //    //g.DrawLine(arrow_pen, AxisY0, AxisYN);
        //    //for (int i= 0; i < gridandaxis.Count - 1; i += 2)
        //    //{
        //    //    g.DrawLine(new Pen(Color.FromArgb(50, 0, 0, 0), 1), gridandaxis[i], gridandaxis[i + 1]);
        //    //}
        //}

        public void Transform(Matrix T)
        {
            Matrix rez;
            for (int i = 0; i < lines.Count; i++)
            {
                lines[i].start.X -= 20;
                lines[i].start.Y -= 20;
                lines[i].end.X -= 20;
                lines[i].end.Y -= 20;

                rez = new Matrix(lines[i].end.X, lines[i].end.Y, lines[i].we);
                rez = rez.Multiple(T);
                lines[i].we = Convert.ToInt32(rez.matrix[0, 2]);
                lines[i].end.X = Convert.ToInt32(rez.matrix[0, 0]/lines[i].we);
                lines[i].end.Y = Convert.ToInt32(rez.matrix[0, 1] / lines[i].we);
                


                rez = new Matrix(lines[i].start.X, lines[i].start.Y, lines[i].ws);
                rez = rez.Multiple(T);
                lines[i].ws = Convert.ToInt32(rez.matrix[0, 2]);
                lines[i].start.X = Convert.ToInt32(rez.matrix[0, 0]/ lines[i].ws);
                lines[i].start.Y = Convert.ToInt32(rez.matrix[0, 1]/ lines[i].ws);
                

                lines[i].start.X += 20;
                lines[i].start.Y += 20;
                lines[i].end.X += 20;
                lines[i].end.Y += 20;

            }

            foreach (TextPoint p in textpoints)
            {
                p.point.X -= 20;
                p.point.Y -= 20;

                rez = new Matrix(p.point.X, p.point.Y, p.w);
                rez = rez.Multiple(T);
                p.point.X = Convert.ToInt32(rez.matrix[0, 0]);
                p.point.Y = Convert.ToInt32(rez.matrix[0, 1]);
                p.w = Convert.ToInt32(rez.matrix[0, 2]);

                p.point.X += 20;
                p.point.Y += 20;
            }

            /*Point[] ar = circle1.ToArray();
            T.TransformPoints(ar);
            circle1 = ar.ToList();

            Point[] ar1 = circle2.ToArray();
            T.TransformPoints(ar1);
            circle2 = ar1.ToList();

            Point[] ar2 = arc1.ToArray();
            T.TransformPoints(ar2);
            arc1 = ar2.ToList();

            Point[] ar3 = arc2.ToArray();
            T.TransformPoints(ar3);
            arc2 = ar3.ToList();

            Point[] ar4 = arc3.ToArray();
            T.TransformPoints(ar4);
            arc3 = ar4.ToList();

            Point[] ar5 = arc4.ToArray();
            T.TransformPoints(ar5);
            arc4 = ar5.ToList();

            Point[] ar6 = {
                p1,
                p2,
                p3,
                p4,
                p5,
                p6,
                p7,
                p8,
                p9,
                p10,
                p11,
                p12
            };
            T.TransformPoints(ar6);
            p1 = ar6[0];
            p2 = ar6[1];
            p3 = ar6[2];
            p4 = ar6[3];
            p5 = ar6[4];
            p6 = ar6[5];
            p7 = ar6[6];
            p8 = ar6[7];
            p9 = ar6[8];
            p10 = ar6[9];
            p11 = ar6[10];
            p12 = ar6[11];

            g.Clear(Color.White);
            DrawGridAndAxis(g);

            DrawModel(g);*/
        }

        //public void AffineTransform(Matrix T)
        //{
        //}

        //public void ProjectiveTransform(Matrix T)
        //{

        //}
    }
}
