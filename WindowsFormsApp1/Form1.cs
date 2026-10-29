
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Bitmap canvas;
        Point lastPoint;
        bool firstClick = true;

        public Form1()
        {
            InitializeComponent();

            this.Text = "Drawing App";
            this.ClientSize = new Size(900, 600);
            this.BackColor = Color.White;
            this.DoubleBuffered = true;

            canvas = new Bitmap(900, 600);

            using (Graphics g = Graphics.FromImage(canvas))
            {
                g.Clear(Color.White);
            }

            this.MouseClick += Form1_MouseClick;
            this.Paint += Form1_Paint;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                using (Graphics g = Graphics.FromImage(canvas))
                {
                    g.SmoothingMode =
                        System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    // Өмнөх цэгтэй холбох
                    if (!firstClick)
                    {
                        using (Pen pen = new Pen(Color.Black, 3))
                        {
                            g.DrawLine(pen, lastPoint, e.Location);
                        }
                    }

                    // Дарсан газарт хар цэг зурах
                    g.FillEllipse(
                        Brushes.Black,
                        e.X - 5,
                        e.Y - 5,
                        10,
                        10
                    );
                }

                lastPoint = e.Location;
                firstClick = false;
                this.Invalidate();
            }

            // Баруун товчоор арилгах
            if (e.Button == MouseButtons.Right)
            {
                using (Graphics g = Graphics.FromImage(canvas))
                {
                    g.Clear(Color.White);
                }

                firstClick = true;
                this.Invalidate();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (canvas != null)
            {
                e.Graphics.DrawImageUnscaled(canvas, 0, 0);
            }
        }
    }
}
