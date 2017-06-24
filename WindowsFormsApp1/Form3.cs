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
            // Create a notification rectangle in the bottom right corner
            Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Location = new Point((workingArea.Right-10) - 300,
                                      (workingArea.Bottom-10) - 120);

            // Add delay options
            comboBox1Notification.Items.Add("5 Minuten");
            comboBox1Notification.Items.Add("15 Minuten");
            comboBox1Notification.Items.Add("30 Minuten");
            comboBox1Notification.Items.Add("45 Minuten");
            comboBox1Notification.SelectedIndex = 0;

            delayButtonNotification.Select();
        }

        // On click, open exercise form, refresh exercise form content to load possible new exercise
        private void Form3_MouseClick(object sender, MouseEventArgs e)
        {
            Form1 form1 = Application.OpenForms.OfType<Form1>().First();
            form1.getExerciseAsync();
        
            form1.Show();
            form1.WindowState = FormWindowState.Normal;

            this.Hide();
            this.WindowState = FormWindowState.Minimized;
        }

        // If the user delays from notification, set the timer in form1, disable delay functionality for future actions and hide notification
        private void delayButtonNotification_MouseClick(object sender, MouseEventArgs e)
        {
            Form1 form1 = Application.OpenForms.OfType<Form1>().First();
            form1.delayTimer(comboBox1Notification.Text);
            disableDelayExercise();
            disableDelayNotification();

            this.Hide();
            this.WindowState = FormWindowState.Minimized;
        }

        // Disable delay functionality in this form (notification)
        public void disableDelayNotification()
        {
            delayButtonNotification.Enabled = false;
            comboBox1Notification.Enabled = false;
        }

        // Enable delay functionality in this form (notification)
        public void enableDelayNotification()
        {
            delayButtonNotification.Enabled = true;
            comboBox1Notification.Enabled = true;
        }

        // Disabale delay functionality in exercise form
        private void disableDelayExercise()
        {
            Form1 form1 = Application.OpenForms.OfType<Form1>().First();
            form1.disableDelayExercise();
        }
    }
}
