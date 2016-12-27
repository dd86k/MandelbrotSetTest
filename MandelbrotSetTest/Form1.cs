using System;
using System.Windows.Forms;

namespace MandelbrotSetTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            mandelbrotControl1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mandelbrotControl1.Render();
        }
    }
}
