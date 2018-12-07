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
        Contour plane = new Contour();
        Contour planeto = new Contour();
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
            Graphics g = Graphics.FromImage(ModelView.Image);

            plane.Draw(g);

            ModelView.Refresh();
        }

        public void TransformPlaneIntoSmth(Drawing gridandaxis)
        {
            const double timeout = 1000;
            Graphics g = Graphics.FromImage(ModelView.Image);
            

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
