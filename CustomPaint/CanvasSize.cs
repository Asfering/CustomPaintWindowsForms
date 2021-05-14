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
    public partial class CanvasSize : Form
    {
        public int canvasWidth;
        public int canvasHeight;
        public CanvasSize(int CanvasWidth, int CanvasHeight)
        {
            InitializeComponent();
            widthTextBox.Text = CanvasWidth.ToString();
            heightTextBox.Text = CanvasHeight.ToString();
            canvasHeight = CanvasHeight;
            canvasWidth = CanvasWidth;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (correctValue())
            {
                canvasHeight = Int32.Parse(heightTextBox.Text);
                canvasWidth = Int32.Parse(widthTextBox.Text);
                this.Close();
            }
            else
            {
                MessageBox.Show("Введены некорректные значения. Ширина не более 1500, высота не более 1000", "Ошибка ввода",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void widthTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void heightTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private bool correctValue()
        {
            return (widthTextBox.Text != "") && (Int32.Parse(widthTextBox.Text) > 10)
                && (Int32.Parse(widthTextBox.Text) < 1500) && (heightTextBox.Text != "") && (Int32.Parse(heightTextBox.Text) > 10)
                && (Int32.Parse(heightTextBox.Text) < 1000);
        }
    }
}
