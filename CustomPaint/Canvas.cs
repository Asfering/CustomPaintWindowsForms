using System.Drawing;
using System.Windows.Forms;
using System;


//TODO Zoom

namespace CustomPaint
{
    public partial class Canvas : Form
    {
        private bool drawing;
        private int oldX, oldY;
        private Bitmap bmp;
        private int zoom = 1;

        public int Zoom
        {
            get
            {
                return zoom;
            }
            set
            {
                if ((zoom < 6) && (zoom > 1))
                {
                    zoom = value;
                }
            }
        }

        public int CanvasWidth
        {
            get
            {
                return pictureBox1.Width;
            }
            /*set
            {
                pictureBox1.Width = value;
                Bitmap tbmp = new Bitmap(value, pictureBox1.Height);
                Graphics g = Graphics.FromImage(tbmp);
                g.Clear(Color.White);
                g.DrawImage(bmp, new Point(0, 0));
                bmp = tbmp;
                pictureBox1.Image = bmp;
            }*/
        }
        public int CanvasHeight
        {
            get
            {
                return pictureBox1.Height;
            }
            /*set
            {
                pictureBox1.Height = value;
                Bitmap tbmp = new Bitmap(pictureBox1.Width, value);
                Graphics g = Graphics.FromImage(tbmp);
                g.Clear(Color.White);
                g.DrawImage(bmp, new Point(0, 0));
                bmp = tbmp;
                pictureBox1.Image = bmp;
            }*/
        }

        public Canvas()
        {
            InitializeComponent();
            bmp = new Bitmap(ClientSize.Width, ClientSize.Height);
            pictureBox1.Image = bmp;
        }

        public Canvas(String FileName)
        {
            InitializeComponent();
            bmp = new Bitmap(FileName);
            Graphics g = Graphics.FromImage(bmp);
            pictureBox1.Width = bmp.Width;
            pictureBox1.Height = bmp.Height;
            pictureBox1.Image = bmp;
        }

        public void ResizeCanvas(int width, int height)
        {
            pictureBox1.Width = width;
            pictureBox1.Height = height;
            Bitmap tbmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(tbmp);
            g.Clear(Color.White);
            g.DrawImage(bmp, new Point(0, 0));
            bmp = tbmp;
            pictureBox1.Image = bmp;
        }


        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            switch (MainForm.currentTool)
            {
                case MainForm.Tool.Кисточка:
                    if (e.Button == MouseButtons.Left)
                    {
                        Graphics graphics = Graphics.FromImage(bmp);
                        graphics.DrawLine(new Pen(MainForm.currentColor, MainForm.currentWidth), oldX, oldY, e.X, e.Y);
                        oldX = e.X;
                        oldY = e.Y;
                        pictureBox1.Invalidate();
                    }
                    break;
                case MainForm.Tool.Ластик:
                    if (e.Button == MouseButtons.Left)
                    {
                        Graphics graphics = Graphics.FromImage(bmp);
                        graphics.DrawLine(new Pen(pictureBox1.BackColor, MainForm.currentWidth), oldX, oldY, e.X, e.Y);
                        oldX = e.X;
                        oldY = e.Y;
                        pictureBox1.Invalidate();
                    }
                    break;
                case MainForm.Tool.Эллипс:
                    if (drawing)
                    {
                        Graphics graphics = pictureBox1.CreateGraphics();
                        graphics.DrawEllipse(new Pen(MainForm.currentColor, MainForm.currentWidth), oldX, oldY, e.X - oldX, e.Y - oldY);
                        pictureBox1.Invalidate();
                    }
                    break;
                case MainForm.Tool.Линия:
                    if (drawing)
                    {
                        Graphics graphics = pictureBox1.CreateGraphics();
                        graphics.DrawLine(new Pen(MainForm.currentColor, MainForm.currentWidth), oldX, oldY, e.X, e.Y);
                        pictureBox1.Invalidate();
                    }
                    break;
                case MainForm.Tool.Звезда:
                    if (drawing)
                    {
                        Graphics graphics = pictureBox1.CreateGraphics();
                        graphics.DrawPolygon(new Pen(MainForm.currentColor, MainForm.currentWidth), GetStarPoints(e));
                        pictureBox1.Invalidate();
                    }
                    break;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)   
        {
            if (MainForm.Tool.Эллипс == MainForm.currentTool)
            {
                Graphics graphics = Graphics.FromImage(bmp);
                graphics.DrawEllipse(new Pen(MainForm.currentColor, MainForm.currentWidth), oldX, oldY, e.X - oldX, e.Y - oldY);
                pictureBox1.Invalidate();
            }
            if (MainForm.Tool.Линия == MainForm.currentTool)
            {
                Graphics graphics = Graphics.FromImage(bmp);
                graphics.DrawLine(new Pen(MainForm.currentColor, MainForm.currentWidth), oldX, oldY, e.X, e.Y);
                pictureBox1.Invalidate();
            }
            if (MainForm.Tool.Звезда == MainForm.currentTool)
            {
                Graphics graphics = Graphics.FromImage(bmp);
                graphics.DrawPolygon(new Pen(MainForm.currentColor, MainForm.currentWidth), GetStarPoints(e));
                pictureBox1.Invalidate();
            }
            drawing = false;
        }

        private void pictureBox1_MouseDown_1(object sender, MouseEventArgs e)
        {
            oldX = e.X;
            oldY = e.Y;
            drawing = true;
        }

        private PointF[] GetStarPoints(MouseEventArgs e)
        {

            double BRadius, SRadius;
            BRadius = Math.Sqrt((oldX - e.X) * (oldX - e.X) + (oldY - e.Y) * (oldY - e.Y));
            SRadius = BRadius / 2.0;
            double x0 = oldX, y0 = oldY;
            int n = MainForm.NStar;
            double alpha = 0;
            double a = alpha, da = Math.PI / n, l;
            PointF[] points = new PointF[2*n + 1];


            for (int k = 0; k < 2 * n + 1; k++)
            {
                l = k % 2 == 0 ? BRadius : SRadius;
                points[k] = new PointF((float)(x0 + l * Math.Cos(a)), (float)(y0 + l * Math.Sin(a)));
                a += da;
            }

            return points;
        }

        public void ZoomPicture(Size size)
        {
            Bitmap bm = new Bitmap(bmp, Convert.ToInt32(bmp.Width * size.Width),
                Convert.ToInt32(bmp.Height * size.Height));
            Graphics gpu = Graphics.FromImage(bm);
            
            pictureBox1.Image = null;
            pictureBox1.Image = bm;
        }
    }
}
