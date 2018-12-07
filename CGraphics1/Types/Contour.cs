using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGraphics1.Types
{
    public class Contour
    {
        public List<CurveB3> curves = new List<CurveB3>();

        public void Draw(Graphics g)
        {
            foreach(CurveB3 curve in curves)
            {
                curve.Draw(g);
            }
        }

        public void Transform(Contour contour2,Graphics g, double t)
        {
            CurveB3 c;
            for (int i = 0; i < curves.Count; i++)
            {
                c = new CurveB3(new Point((int)(curves[i].A.X * (1 - t) + contour2.curves[i].A.X),(int)(curves[i].A.Y * (1 - t) + contour2.curves[i].A.Y)),
                    new Point((int)(curves[i].B.X * (1 - t) + contour2.curves[i].B.X), (int)(curves[i].B.Y * (1 - t) + contour2.curves[i].B.Y)),
                    new Point((int)(curves[i].C.X * (1 - t) + contour2.curves[i].C.X), (int)(curves[i].C.Y * (1 - t) + contour2.curves[i].C.Y)),
                    new Point((int)(curves[i].D.X * (1 - t) + contour2.curves[i].D.X), (int)(curves[i].D.Y * (1 - t) + contour2.curves[i].D.Y)),
                    new Pen(Brushes.Black));
                c.Draw(g);
            }
        }
    }
}
