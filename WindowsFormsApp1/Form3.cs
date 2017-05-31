using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Location = new Point((workingArea.Right-10) - Size.Width,
                                      (workingArea.Bottom-10) - Size.Height);
        }

        private void Form3_MouseClick(object sender, MouseEventArgs e)
        {
            Form1 form1 = Application.OpenForms.OfType<Form1>().First();
            form1.getExerciseAsync();
        
            form1.Show();
            form1.WindowState = FormWindowState.Normal;

            this.Hide();
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
