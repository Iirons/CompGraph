﻿using System;
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
using CGraphics1.Types;

namespace CGraphics1
{
    

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

        }

        List<Drawing> alldrawings = new List<Drawing>();
        Drawing drawing = new Drawing();
        Drawing gridandaxis = new Drawing();

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
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (CheckParams())
            {
                ModelView.Image = new Bitmap(ModelView.Width, ModelView.Height);
                DrawAxisAndGrid();
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
                p5 = new Point(0, 20), p6 = new Point(ModelView.Right, 20),
                p7 = new Point(20, 0), p8 = new Point(20, ModelView.Bottom),
                p9 = new Point(70, 5),
                p10 = new Point(5, 70);

            //drawing.gridandaxis.Add(p1);
            //drawing.gridandaxis.Add(p2);
            //drawing.gridandaxis.Add(p3);
            //drawing.gridandaxis.Add(p4);
            //drawing.AxisX0 = p5;
            //drawing.AxisXN = p6;
            //drawing.AxisY0 = p7;
            //drawing.AxisYN = p8;
            //drawing.size1 = p9;
            //drawing.size2 = p10;

            gridandaxis.lines.Add(new Line(new Pen(Brushes.Black, 1), p1, p2));
            gridandaxis.lines.Add(new Line(new Pen(Brushes.Black, 1), p3, p4));
            
            //g.DrawLine(new Pen(Brushes.Black, 1), p1, p2);
            //g.DrawLine(new Pen(Brushes.Black, 1), p3, p4);

            //g.DrawString("50", fnt, Brushes.Black, p9);
            //g.DrawString("50", fnt, Brushes.Black, p10);

            gridandaxis.textpoints.Add(new TextPoint("50", p9));
            gridandaxis.textpoints.Add(new TextPoint("50", p10));


            gridandaxis.lines.Add(new Line(arrow_pen, p5, p6));
            gridandaxis.lines.Add(new Line(arrow_pen, p7, p8));

            //g.DrawLine(arrow_pen, p5, p6);
            //g.DrawLine(arrow_pen, p7, p8);

            //Draw Grid
            if (GridCheckBox.Checked)
            {
                for (int i = 20; i <= ModelView.Right; i += 50)
                {
                    //drawing.gridandaxis.Add(new Point(i, ModelView.Top));
                    //drawing.gridandaxis.Add(new Point(i, ModelView.Bottom));
                    //g.DrawLine(new Pen(Color.FromArgb(50, 0, 0, 0), 1), 
                    //    new Point(i, ModelView.Top), new Point(i, ModelView.Bottom));

                    gridandaxis.lines.Add(new Line(new Pen(Color.FromArgb(50, 0, 0, 0), 1),
                            new Point(i, ModelView.Top), new Point(i, ModelView.Bottom)));
                }
                for (int i = 20; i <= ModelView.Bottom; i += 50)
                {
                    //drawing.gridandaxis.Add(new Point(ModelView.Left, i));
                    //drawing.gridandaxis.Add(new Point(ModelView.Right, i));
                    //g.DrawLine(new Pen(Color.FromArgb(50, 0, 0, 0), 1), 
                    //    new Point(ModelView.Left, i), new Point(ModelView.Right, i));

                    gridandaxis.lines.Add(new Line(new Pen(Color.FromArgb(50, 0, 0, 0), 1),
                          new Point(ModelView.Left, i), new Point(ModelView.Right, i)));
                }
            }
        }

        private bool CheckParams()
        {
            if (R1box.Text == "" || R2box.Text == "" || R3box.Text == "" || Abox.Text == "")
            {
                return false;
            }
            if(!Int32.TryParse(R1box.Text, out int r1) ||
            !Int32.TryParse(R2box.Text, out int r2) ||
            !Int32.TryParse(R3box.Text, out int r3) ||
            !Int32.TryParse(Abox.Text, out int a)||
            r1 >= r2 || a <= r3 + 20 || r1 <= 5 || r2 <= 5 || r3 <= 5 || a <= 0) { return false; }
            return true;
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

            //g.DrawLine(dashed_pen,new Point(center.X, center.Y - a - r3 - 5),
            //new Point(center.X, center.Y + a + r3 + 5));

            //g.DrawLine(dashed_pen, new Point(center.X - a - r3 - 5, center.Y),
            //new Point(center.X + a + r3 + 5, center.Y));

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
            GetAxisAndGrid();
            DrawAxisAndGrid();
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)//ScaleButton
        {
            ModelView.Image = new Bitmap(ModelView.Width, ModelView.Height);
            Graphics g = Graphics.FromImage(ModelView.Image);
            DrawAxisAndGrid();
            Types.Matrix matrix = new Types.Matrix(Convert.ToDouble(ScX.Text), 0, 0,
                0, Convert.ToDouble(ScY.Text), 0, 
                0, 0, 1);
            drawing.Transform(matrix);
            drawing.Draw(g);
            ModelView.Refresh();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void RotateOzButton_Click(object sender, EventArgs e)
        {
            ModelView.Image = new Bitmap(ModelView.Width, ModelView.Height);
            Graphics g = Graphics.FromImage(ModelView.Image);
            DrawAxisAndGrid();
            double angle = Convert.ToInt32(RtAngOz.Text)*Math.PI/180;
            drawing.Transform(new Types.Matrix(Math.Cos(angle), Math.Sin(angle), 0,
                -Math.Sin(angle), Math.Cos(angle), 0,
                0, 0, 1));
            drawing.Draw(g);
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
            DrawAxisAndGrid();
            drawing.Draw(g);
            ModelView.Refresh();
        }
    }
}
