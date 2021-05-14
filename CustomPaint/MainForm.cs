using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomPaint
{
    public partial class MainForm : Form
    {
        private string currentFileName;
        public enum Tool { Кисточка, Ластик, Эллипс, Линия, Звезда};
        public static Color currentColor = Color.Black;
        public static int currentWidth = 3;
        public static Tool currentTool = 0;
        public static int NStar = 4;
        private Canvas canvas;

        public MainForm()
        {
            InitializeComponent();
            colorButton.BackColor = currentColor;
        }

        private void красныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentColor = Color.Red;
            colorButton.BackColor = currentColor;
        }

        private void зеленыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentColor = Color.LightGreen;
            colorButton.BackColor = currentColor;
        }

        private void синийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentColor = Color.Blue;
            colorButton.BackColor = currentColor;
        }

        private void другойToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.FullOpen = true;
            if (colorDialog.ShowDialog() == DialogResult.Cancel)
                return;
            currentColor = colorDialog.Color;
            colorButton.BackColor = currentColor;
        }

        private void новыйРисунокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            canvas = new Canvas();
            canvas.MdiParent = this;
            canvas.Show();
        }

        private void brushButton_Click(object sender, EventArgs e)
        {
            currentTool = 0;
            toolLabel.Text = currentTool.ToString();
        }

        private void eraserButton_Click(object sender, EventArgs e)
        {
            currentTool = (Tool) 1;
            toolLabel.Text = currentTool.ToString();
        }

        private void circleButton_Click(object sender, EventArgs e)
        {
            currentTool = (Tool) 2;
            toolLabel.Text = currentTool.ToString();
        }

        private void lineButton_Click(object sender, EventArgs e)
        {
            currentTool = (Tool) 3;
            toolLabel.Text = currentTool.ToString();
        }

        private void starButton_Click(object sender, EventArgs e)
        {
            currentTool = (Tool) 4;
            toolLabel.Text = currentTool.ToString();
            starVertex star = new starVertex();
            star.Show();    
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutPaint aboutPaint = new AboutPaint();
            aboutPaint.Show();
        }

        private void рисунокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            размерХолстаToolStripMenuItem.Enabled = !(ActiveMdiChild == null);
        }

        private void размерХолстаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CanvasSize cs = new CanvasSize(canvas.CanvasWidth, canvas.CanvasHeight);
            cs.ShowDialog();
            /*((Canvas)ActiveMdiChild).CanvasHeight = cs.canvasHeight;
            ((Canvas)ActiveMdiChild).CanvasWidth = cs.canvasWidth;*/
            canvas.ResizeCanvas(cs.canvasWidth, cs.canvasHeight);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            canvas.Zoom++;
            canvas.ZoomPicture(new Size(canvas.Zoom, canvas.Zoom));
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            canvas.Zoom--;
            canvas.ZoomPicture(new Size(canvas.Zoom, canvas.Zoom));
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Save()
        {

        }

        private void SaveAs()
        {
            saveFileDialog1.Filter = "JPEG Image(*.jpeg)|*.jpeg|JPG image(*.jpg)|*.jpg|Windows Bitmap(*.bmp)|*.bmp";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName;
            MessageBox.Show(saveFileDialog1.FileName.ToString());
            /*canvas.pictureBox1.Image.Save(filename);*/
        }

        private void OpenImage()
        {
            openFileDialog1.Filter = "JPEG Image(*.jpeg)|*.jpeg|JPG image(*.jpg)|*.jpg|Windows Bitmap(*.bmp)|*.bmp";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            currentFileName = openFileDialog1.FileName;
            canvas = new Canvas(currentFileName);
            canvas.MdiParent = this;
            canvas.Show();
        }

        private void открытьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenImage();
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        /*Image ZoomPicture(Image img, Size size)
        {
            Bitmap bm = new Bitmap(img, Convert.ToInt32(img.Width * size.Width),
                Convert.ToInt32(img.Height * size.Height));
            Graphics gpu = Graphics.FromImage(bm);
            gpu.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            return bm;
        }*/
    }
}
