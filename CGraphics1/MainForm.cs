using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CGraphics1.Lab1;
using CGraphics1.Labs;
using CGraphics1.Types;

namespace CGraphics1
{
    

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

        }
        
        Lab2 lab2;
        Lab3 lab3;

        List<Drawing> alldrawings = new List<Drawing>();
        Curve curve = new Curve();
        Drawing drawing = new Drawing();
        Drawing gridandaxis = new Drawing();
        Validation val = new Validation();

        private PictureBox ModelView = new PictureBox();
        // Cache font instead of recreating font objects each time we paint.
        private Font fnt = new Font("Arial", 10);
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Dock the PictureBox to the form and set its background to white.
            ModelView.Dock = DockStyle.Fill;
            ModelView.BackColor = Color.White;
            // Connect the Paint event of the PictureBox to the event handler method.
            //System.Windows.Forms.PaintEventHandler paintEvent = new System.Windows.Forms.PaintEventHandler(ModelView_Paint);
            //ModelView.Paint += paintEvent;

            // Add the PictureBox control to the Form.
            panel1.Controls.Add(ModelView);
            ModelView.Image = new Bitmap(ModelView.Width, ModelView.Height);

            GetAxisAndGrid();
            DrawAxisAndGrid();

            lab3 = new Lab3(ModelView);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (val.CheckDrawingParams(R1box.Text, R2box.Text, R3box.Text, Abox.Text))
            {
                ModelView.Image = new Bitmap(ModelView.Width, ModelView.Height);
                gridandaxis = new Drawing();
                GetAxisAndGrid();
                DrawAxisAndGrid();
                curve = new Curve();
                drawing = new Drawing();
                DrawModel();
            }
            else
            {
                MessageBox.Show("Incorrect values", "Error");
            }
        }

        private void DrawAxisAndGrid()
        {
            // Create a local version of the graphics object for the PictureBox.
            Graphics g = Graphics.FromImage(ModelView.Image);

            gridandaxis.Draw(g);
            ModelView.Refresh();
        }

        private void GetAxisAndGrid()
        {
            const int range = 300;
            
            // Draw Axis
            Pen arrow_pen = new Pen(Brushes.Black, 3);

            GraphicsPath hPath = new GraphicsPath();

            // Create the outline for our custom end cap.
            hPath.AddLine(0, 0, -2, -2);
            hPath.AddLine(0, 0, 2, -2);
            CustomLineCap ArrowCap = new CustomLineCap(null, hPath);

            arrow_pen.CustomEndCap = ArrowCap;

            Point p1 = new Point(70, 15), p2 = new Point(70, 25),
                p3 = new Point(15, 70), p4 = new Point(25, 70),
                p5 = new Point(-range, 20), p6 = new Point(ModelView.Right, 20),
                p7 = new Point(20, -range), p8 = new Point(20, ModelView.Bottom),
                p9 = new Point(70, 5),
                p10 = new Point(5, 70);

            gridandaxis.lines.Add(new Line(new Pen(Brushes.Black, 1), p1, p2));
            gridandaxis.lines.Add(new Line(new Pen(Brushes.Black, 1), p3, p4));

            gridandaxis.textpoints.Add(new TextPoint("50", p9));
            gridandaxis.textpoints.Add(new TextPoint("50", p10));


            gridandaxis.lines.Add(new Line(arrow_pen, p5, p6));
            gridandaxis.lines.Add(new Line(arrow_pen, p7, p8));

            //Draw Grid
            if (GridCheckBox.Checked)
            {
                for (int i = 20-range; i <= ModelView.Right; i += 50)
                {
                    gridandaxis.lines.Add(new Line(new Pen(Color.FromArgb(50, 0, 0, 0), 1),
                            new Point(i, ModelView.Top-range), new Point(i, ModelView.Bottom)));
                }
                for (int i = 20-range; i <= ModelView.Bottom; i += 50)
                {
                    gridandaxis.lines.Add(new Line(new Pen(Color.FromArgb(50, 0, 0, 0), 1),
                          new Point(ModelView.Left-range, i), new Point(ModelView.Right, i)));
                }
            }
        }

        private void DrawModel()
        {
            Graphics g = Graphics.FromImage(ModelView.Image);

            Point center = new Point(320, 270);
            //Point center = new Point(100, 100);
            //drawing.center = center;
            Int32.TryParse(R1box.Text, out int r1);
            Int32.TryParse(R2box.Text, out int r2);
            Int32.TryParse(R3box.Text, out int r3);
            Int32.TryParse(Abox.Text, out int a);

            Point o1 = new Point(center.X - a, center.Y);
            Point o2 = new Point(center.X, center.Y + a);
            Point o3 = new Point(center.X + a, center.Y);
            Point o4 = new Point(center.X, center.Y - a);
            
            //Creating pens
            Pen dashed_pen = new Pen(Brushes.Black, 1);
            dashed_pen.DashPattern = new float[] { 15, 5, 5, 5 };

            Pen simple_pen = new Pen(Brushes.Black, 1);

            Pen arrow_pen = new Pen(Brushes.Black, 2);
            GraphicsPath hPath = new GraphicsPath();
            hPath.AddLine(0, 0, -2, -2);
            hPath.AddLine(0, 0, 2, -2);
            CustomLineCap ArrowCap = new CustomLineCap(null, hPath);
            arrow_pen.CustomEndCap = ArrowCap;

            //Drawing Model

            Point p1 = new Point(center.X - a, center.Y - r3);
            Point p2 = new Point(center.X - a, center.Y + r3);
            Point p3 = new Point(center.X + a, center.Y - r3);
            Point p4 = new Point(center.X + a, center.Y + r3);
            Point p5 = new Point(center.X - r3, center.Y - a);
            Point p6 = new Point(center.X + r3, center.Y - a);
            Point p7 = new Point(center.X - r3, center.Y + a);
            Point p8 = new Point(center.X + r3, center.Y + a);

            Point p9 = new Point(center.X, center.Y - r2);
            Point p10 = new Point(center.X + r2, center.Y);
            Point p11 = new Point(center.X, center.Y + r2);
            Point p12 = new Point(center.X - r2, center.Y);

            drawing.lines.Add(new Line(simple_pen, p1, p2));
            drawing.lines.Add(new Line(simple_pen, p3, p4));
            drawing.lines.Add(new Line(simple_pen, p5, p6));
            drawing.lines.Add(new Line(simple_pen, p7, p8));

            drawing.lines.Add(new Line(simple_pen, p1, p5));
            drawing.lines.Add(new Line(simple_pen, p6, p3));
            drawing.lines.Add(new Line(simple_pen, p4, p8));
            drawing.lines.Add(new Line(simple_pen, p7, p2));

            drawing.lines.Add(new Line(simple_pen, p5, p9));
            drawing.lines.Add(new Line(simple_pen, p6, p9));

            drawing.lines.Add(new Line(simple_pen, p3, p10));
            drawing.lines.Add(new Line(simple_pen, p4, p10));

            drawing.lines.Add(new Line(simple_pen, p7, p11));
            drawing.lines.Add(new Line(simple_pen, p8, p11));

            drawing.lines.Add(new Line(simple_pen, p1, p12));
            drawing.lines.Add(new Line(simple_pen, p2, p12));

            drawing.lines.AddRange(drawing.GetCircle(center, r1, 0, 6.28319));
            drawing.lines.AddRange(drawing.GetCircle(center, r2, 0, 6.28319));

            drawing.lines.AddRange(drawing.GetCircle(o1, r3, 1.5708, 4.71239));
            drawing.lines.AddRange(drawing.GetCircle(o2, r3, 0, 3.14159));
            drawing.lines.AddRange(drawing.GetCircle(o3, r3, 4.71239, 7.85398));
            drawing.lines.AddRange(drawing.GetCircle(o4, r3, 3.14159, 6.28319));

            //Drawing axis for circles

            drawing.lines.Add(new Line(dashed_pen, new Point(center.X, center.Y - a - r3 - 5)
                , new Point(center.X, center.Y + a + r3 + 5)));
            drawing.lines.Add(new Line(dashed_pen, new Point(center.X - a - r3 - 5, center.Y)
                , new Point(center.X + a + r3 + 5, center.Y)));

            drawing.Draw(g);

            //Drawing sizes

            if (SizesCheckBox.Checked)
            {
                //R1

                g.DrawLine(arrow_pen, center, new Point(Convert.ToInt32(center.X + (r1 * Math.Cos(1.0472))),
                    Convert.ToInt32(center.Y + (r1 * Math.Sin(1.0472)))));

                Point forsizes = new Point(Convert.ToInt32(center.X + ((r2 + 20) * Math.Cos(1.0472))),
                    Convert.ToInt32(center.Y + ((r2 + 20) * Math.Sin(1.0472))));

                g.DrawLine(simple_pen, center, forsizes);

                g.DrawLine(simple_pen, forsizes, new Point(forsizes.X + 30, forsizes.Y));

                g.DrawString(r1.ToString(), fnt, Brushes.Black, new Point(forsizes.X, forsizes.Y - 15));

                //R2

                g.DrawLine(arrow_pen, center, new Point(Convert.ToInt32(center.X + (r2 * Math.Cos(5.23599))),
                    Convert.ToInt32(center.Y + (r2 * Math.Sin(5.23599)))));

                forsizes = new Point(Convert.ToInt32(center.X + ((r2 + 20) * Math.Cos(5.23599))),
                    Convert.ToInt32(center.Y + ((r2 + 20) * Math.Sin(5.23599))));

                g.DrawLine(simple_pen, center, forsizes);

                g.DrawLine(simple_pen, forsizes, new Point(forsizes.X + 30, forsizes.Y));

                g.DrawString(r2.ToString(), fnt, Brushes.Black, new Point(forsizes.X, forsizes.Y - 15));

                //R3

                g.DrawLine(arrow_pen, new Point(center.X, center.Y - a), new Point(Convert.ToInt32(center.X + (r3 * Math.Cos(5.23599))),
                    Convert.ToInt32(center.Y - a + (r3 * Math.Sin(5.23599)))));

                forsizes = new Point(Convert.ToInt32(center.X + ((r3 + 20) * Math.Cos(5.23599))),
                    Convert.ToInt32(center.Y - a + ((r3 + 20) * Math.Sin(5.23599))));

                g.DrawLine(simple_pen, new Point(center.X, center.Y - a), forsizes);

                g.DrawLine(simple_pen, forsizes, new Point(forsizes.X + 30, forsizes.Y));

                g.DrawString(r3.ToString(), fnt, Brushes.Black, new Point(forsizes.X, forsizes.Y - 15));

                //A

                g.DrawLine(simple_pen, center, new Point(center.X + a + r3 + 30, center.Y));
                g.DrawLine(simple_pen, new Point(center.X, center.Y - a), new Point(center.X + a + r3 + 30, center.Y - a));

                g.DrawLine(arrow_pen, new Point(center.X + a + r3 + 15, center.Y), new Point(center.X + a + r3 + 15, center.Y - a));
                g.DrawLine(arrow_pen, new Point(center.X + a + r3 + 15, center.Y - a), new Point(center.X + a + r3 + 15, center.Y));

                g.DrawString(a.ToString(), fnt, Brushes.Black, new Point(center.X + a + r3 + 17, center.Y - a / 2));

            }

            //Refreshing ModelView

            ModelView.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)//Reset
        {
            ModelView.Image = new Bitmap(ModelView.Width, ModelView.Height);
            gridandaxis = new Drawing();
            drawing = new Drawing();
            curve = new Curve();
            GetAxisAndGrid();
            DrawAxisAndGrid();
        }

        //lab1

        private void RotateOzButton_Click(object sender, EventArgs e)
        {
            ModelView.Image = new Bitmap(ModelView.Width, ModelView.Height);
            Graphics g = Graphics.FromImage(ModelView.Image);
            DrawAxisAndGrid();
            double angle = Convert.ToInt32(RtAngOz.Text)*Math.PI/180;
            Types.Matrix matrix = new Types.Matrix(Math.Cos(angle), Math.Sin(angle), 0,
                -Math.Sin(angle), Math.Cos(angle), 0,
                0, 0, 1);
            drawing.Transform(matrix);
            drawing.Draw(g);
            curve.Transform(matrix);
            curve.Draw(g);

            ModelView.Refresh();
        }

        private void RotatePointButton_Click(object sender, EventArgs e)
        {
            ModelView.Image = new Bitmap(ModelView.Width, ModelView.Height);
            Graphics g = Graphics.FromImage(ModelView.Image);
            DrawAxisAndGrid();
            double angle = Convert.ToInt32(RtPtAng.Text) * Math.PI / 180;
            int x = Convert.ToInt32(RtPtX.Text);
            int y = Convert.ToInt32(RtPtY.Text);
            Types.Matrix matrix = new Types.Matrix(1, 0, 0,
                0, 1, 0,
                -x, -y, 1);
            matrix = matrix.Multiple(new Types.Matrix(Math.Cos(angle), Math.Sin(angle), 0,
                -Math.Sin(angle), Math.Cos(angle), 0,
                0, 0, 1));
            matrix = matrix.Multiple(new Types.Matrix(1, 0, 0,
                0, 1, 0,
                x, y, 1));
            drawing.Transform(matrix);
            drawing.Draw(g);
            curve.Transform(matrix);
            curve.Draw(g);
            ModelView.Refresh();
        }

        private void TranslateButton_Click(object sender, EventArgs e)
        {
            ModelView.Image = new Bitmap(ModelView.Width, ModelView.Height);
            Graphics g = Graphics.FromImage(ModelView.Image);
            DrawAxisAndGrid();
            int x = Convert.ToInt32(TrX.Text);
            int y = Convert.ToInt32(TrY.Text);
            Types.Matrix matrix = new Types.Matrix(1, 0, 0,
                0, 1, 0,
                x, y, 1);
            drawing.Transform(matrix);
            drawing.Draw(g);
            curve.Transform(matrix);
            curve.Draw(g);
            ModelView.Refresh();
        }

        private void AffTransformButton_Click(object sender, EventArgs e)
        {
            ModelView.Image = new Bitmap(ModelView.Width, ModelView.Height);
            Graphics g = Graphics.FromImage(ModelView.Image);
            double x1 = Convert.ToInt32(AffRx1X.Text)/50;
            double y1 = Convert.ToInt32(AffRx1Y.Text) / 50;
            double x2 = Convert.ToInt32(AffRx2X.Text) / 50;
            double y2 = Convert.ToInt32(AffRx2Y.Text) / 50;
            double x0 = Convert.ToInt32(AffOX.Text) / 50;
            double y0 = Convert.ToInt32(AffOY.Text) / 50;
            Types.Matrix matrix = new Types.Matrix(x1, y1, 0,
                x2, y2, 0,
                x0, y0, 1);
            drawing.Transform(matrix);
            gridandaxis.Transform(matrix);
            curve.Transform(matrix);
            curve.Draw(g);
            DrawAxisAndGrid();
            drawing.Draw(g);
            ModelView.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)//ScaleButton
        {
            ModelView.Image = new Bitmap(ModelView.Width, ModelView.Height);
            Graphics g = Graphics.FromImage(ModelView.Image);
            Types.Matrix matrix = new Types.Matrix(Convert.ToDouble(ScX.Text), 0, 0,
                0, Convert.ToDouble(ScY.Text), 0,
                0, 0, 1);
            drawing.Transform(matrix);
            gridandaxis.Transform(matrix);
            curve.Transform(matrix);
            curve.Draw(g);
            DrawAxisAndGrid();
            drawing.Draw(g);
            ModelView.Refresh();
        }

        private void PrTransform_Click(object sender, EventArgs e)
        {
            ModelView.Image = new Bitmap(ModelView.Width, ModelView.Height);
            Graphics g = Graphics.FromImage(ModelView.Image);
            int x1 = Convert.ToInt32(PrRx1X.Text);
            int y1 = Convert.ToInt32(PrRx1Y.Text);
            int w1 = Convert.ToInt32(PrRx1w.Text);
            int x2 = Convert.ToInt32(PrRx2X.Text);
            int y2 = Convert.ToInt32(PrRx2Y.Text);
            int w2 = Convert.ToInt32(PrRx2w.Text);
            int x0 = Convert.ToInt32(PrOX.Text);
            int y0 = Convert.ToInt32(PrOY.Text);
            int w0 = Convert.ToInt32(PrOw.Text);
            Types.Matrix matrix = new Types.Matrix(x1*w1, y1*w1, w1,
                x2*w2, y2*w2, w2,
                x0*w0, y0*w0, w0);
            drawing.Transform(matrix);
            gridandaxis.Transform(matrix);
            curve.Transform(matrix);
            curve.Draw(g);
            DrawAxisAndGrid();
            drawing.Draw(g);
            ModelView.Refresh();
        }


        //lab2

        
        private void CurveDrawButton_Click(object sender, EventArgs e)
        {
            int a = 0;
            if (val.CheckCurveParams(CurveATextBox.Text, ref a))
            {
                ModelView.Image = new Bitmap(ModelView.Width, ModelView.Height);
                gridandaxis = new Drawing();
                GetAxisAndGrid();
                Types.Matrix matrix = new Types.Matrix(1, 0, 0,
                    0, 1, 0,
                    300, 250, 1);
                gridandaxis.Transform(matrix);
                DrawAxisAndGrid();
                gridandaxis._x = 320;
                gridandaxis._y = 270;
                drawing = new Drawing();
                curve = new Curve();
                DrawCurve(a);
            }
            else
            {
                MessageBox.Show("Incorrect values", "Error");
            }
        }

        private void DrawCurve(int a)
        {
            Graphics g = Graphics.FromImage(ModelView.Image);
            //Drawing Curve
            if (a != 0)
            {
                curve._a = a;
                curve.lines = curve.GetCurve(a);
            }
            else
            {
                MessageBox.Show("Error. Wrong a value");
                return;
            }

            if (CurveAnimCheckBox.Checked)
            {
                curve.DrawAnim(g, ModelView);
            }
            else
            {
                curve.Draw(g);
            }
            ModelView.Refresh();
        }

        private void TangentCurveButton_Click(object sender, EventArgs e)
        {
            if(TangentCurveTextBox.Text!= "" && Int32.TryParse(TangentCurveTextBox.Text, out int x))
            {
                try
                {
                    Point q = curve.FindPoint(x);
                    Point p = new Point(q.X-curve._x,q.Y-curve._y);
                    Graphics g = Graphics.FromImage(ModelView.Image);

                    double der = -Math.Pow(curve._a, 3) * 2 * x / (Math.Pow((x * x + Math.Pow(curve._a, 2)), 2));

                    curve.DrawTangentLine(p, der, g);
                    ModelView.Refresh();
                }
                catch(Exception r)
                {
                    MessageBox.Show(r.Message);
                }
            }
        }

        private void NormalCurveButton_Click(object sender, EventArgs e)
        {
            if (NormalCurveTextBox.Text != "" && Int32.TryParse(NormalCurveTextBox.Text, out int x))
            {
                try
                {
                    Point q = curve.FindPoint(x);
                    Point p = new Point(q.X - curve._x, q.Y - curve._y);
                    Graphics g = Graphics.FromImage(ModelView.Image);

                    double der = -Math.Pow(curve._a, 3) * 2 * x / (Math.Pow((x * x + Math.Pow(curve._a, 2)), 2));

                    curve.DrawNormalLine(p, der, g);
                    ModelView.Refresh();
                }
                catch (Exception r)
                {
                    MessageBox.Show(r.Message);
                }
            }
        }

        //Lab3

        private void DrawContourButton_Click(object sender, EventArgs e)
        {
            lab3.Test();
        }

        private void ChangeContourButton_Click(object sender, EventArgs e)
        {

        }

        private void EditContourButton_Click(object sender, EventArgs e)
        {

        }
    }
}
