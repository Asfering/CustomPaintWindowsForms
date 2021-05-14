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
    public partial class starVertex : Form
    {
        public starVertex()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void Okbutton_Click(object sender, EventArgs e)
        {
            if (correctValue())
            {
                MainForm.NStar = Int32.Parse(textBox1.Text);
                this.Close();
            }
            else
            {
                MessageBox.Show("Значение должно быть не менее 4 и не более 99", "Ошибка ввода",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool correctValue()
        {
            return (textBox1.Text != "") && (Int32.Parse(textBox1.Text) > 3)
                && (Int32.Parse(textBox1.Text) < 100);
        }
    }
}
