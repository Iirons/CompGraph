using CGraphics1.Lab1;
using CGraphics1.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CGraphics1.Labs
{
    public class Lab3
    {
        public Contour plane = new Contour();
        public Contour planeto = new Contour();
        private PictureBox ModelView = new PictureBox();

        public Lab3()
        {

        }

        public Lab3(PictureBox picture)
        {
            ModelView = picture;
        }

        public void DrawPlane()
        {
            const int linewidth = 1;
            Graphics g = Graphics.FromImage(ModelView.Image);

            plane.curves.Add(new CurveB3(new Point(200,250),
                new Point(210,220),
                new Point(240,220),
                new Point(250,255),
                new Pen(Brushes.Black, linewidth)));
            plane.curves.Add(new CurveB3(new Point(250, 255),
                new Point(255, 256),
                new Point(265, 256),
                new Point(270, 257),
                new Pen(Brushes.Black, linewidth)));
            plane.curves.Add(new CurveB3(new Point(270, 257),
                new Point(350, 90),
                new Point(380, 80),
                new Point(420, 100),
                new Pen(Brushes.Black, linewidth)));
            plane.curves.Add(new CurveB3(new Point(420, 100),
                new Point(450, 90),
                new Point(460, 120),
                new Point(400, 270),
                new Pen(Brushes.Black, linewidth)));

            plane.Draw(g);

            ModelView.Refresh();
        }

        public void TransformPlaneIntoSmth(Drawing gridandaxis)
        {
            const double timeout = 50;
            Graphics g = Graphics.FromImage(ModelView.Image);
            for(double t = 0.1; t <= 1; t+=0.1)
            {
                System.Threading.Thread.Sleep(50);

            }

            ModelView.Refresh();
        }

        public void EditPlane()
        {

        }

        public void Test()
        {

            Graphics g = Graphics.FromImage(ModelView.Image);

            CurveB3 b3 = new CurveB3(new Point(100, 50), new Point(200, 100),
                new Point(300, 300), new Point(400, 50), new Pen(Brushes.Black));
            b3.Draw(g);

            ModelView.Refresh();
        }
    }
}
