using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Net;
using RestSharp;

namespace WindowsFormsApp1
{

    public partial class Form1 : Form
    {
        public static class Globals
        {
            public static String HOST_ADDRESS_CALLS = "http://localhost:8000/";
            public static String HOST_ADDRESS_IMAGES = "http://localhost:63342/2016-2017-Project-Uitdaging-EHI2Va15-Web/kom_in_beweging/img/";
            public static string treatment_exercise_id;


            public static string TreatmentExerciseID
            {
                get
                {
                    return treatment_exercise_id;
                }
                set
                {
                    treatment_exercise_id = value;
                }
            }

        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            initializeForm();
            if (authorized())
            {
                getExerciseAsync();
            }
            else {
                hideAll();
                refreshButton.Visible = true;
                Form2 form2 = new Form2();
                form2.Show();
            }

        }

        private void initializeForm()
        {
            // DEBUG CLEAR SETTINGS
            //Properties.Settings.Default.Reset();
            comboBox1.Items.Add(new Item("5 Minuten",1));
            comboBox1.Items.Add(new Item("15 Minuten", 2));
            comboBox1.Items.Add(new Item("30 Minuten", 3));
            comboBox1.Items.Add(new Item("45 Minuten", 4));
            comboBox1.SelectedIndex = 0;

            doneWithExercise.Select();

            this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            //Show();
            //WindowState = FormWindowState.Normal;
            //Hide();

        }

        /**
            private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Show();
            //WindowState = FormWindowState.Normal;

            notifyIcon1.ShowBalloonTip(2000, "hallo", "test", ToolTipIcon.Info);
        }
            **/


        /**
         * --------------   USER INTERACTION    ----------------
         **/
        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            Show();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            // Only if left click
            if (e.Button == MouseButtons.Left)
            {
                WindowState = FormWindowState.Normal;
                Show();
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            doneExerciseAsync("1");
            buttonDone.BackColor = Color.FromArgb(191, 229, 191);
            buttonNotDone.BackColor = Color.White;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            doneExerciseAsync("-1");
            buttonDone.BackColor = Color.White;
            buttonNotDone.BackColor = Color.FromArgb(249, 156, 156);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            rateExerciseAsync("1");
            buttonLike.BackColor = Color.FromArgb(191, 229, 191);
            buttonDislike.BackColor = Color.White;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            rateExerciseAsync("-1");
            buttonLike.BackColor = Color.White;
            buttonDislike.BackColor = Color.FromArgb(249, 156, 156);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            e.Cancel = true;
            hideExerciseForm();

        }

        private void refreshButton_MouseClick(object sender, MouseEventArgs e)
        {
            getExerciseAsync();
        }

        private void doneWithExercise_MouseClick(object sender, MouseEventArgs e)
        {
            hideExerciseForm();
        }

        // Tray icon options
        private void quitTrayOption_Click(object sender, EventArgs e)
        {
            exitApplication();
        }

        private void openTrayOption_Click(object sender, EventArgs e)
        {
            showExerciseForm();
        }

        private void loginTrayOption_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }


        private void logoutTrayOption_Click(object sender, EventArgs e)
        {
            //TODO uitloggen
            Properties.Settings.Default.jwt = "";
            Properties.Settings.Default.email = "";
            Properties.Settings.Default.Save();
            hideAll();
            refreshButton.Visible = true;
            MessageBox.Show(this,"U bent nu uitgelogd.", "Kom in Beweging - Uitgelogd");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            doneWithExercise.Select();

        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            var item = comboBox1.SelectedIndex;
            Console.WriteLine("ITEM: " + item);
            hideExerciseForm();
        }

        /**
        * --------------   SERVER CALLS    ----------------
        **/
        private async System.Threading.Tasks.Task getExerciseAsync()
        {
            hideAll();
            var client = new RestClient(Globals.HOST_ADDRESS_CALLS);
            var request = new RestRequest("treatment/exercise-now", Method.GET);

            //TODO implement auth jwt        
            request.AddHeader("authorization", Properties.Settings.Default.jwt);
            request.Timeout = 2000;

            // execute the request
            var response = await client.ExecuteTaskAsync(request);
            var content = response.Content; // raw content as string  

            // Check if response is not null
            try
            {
                if (!content.Equals(null))
                {
                    if (content != "[]")
                    {
                        setExercise(content);
                        showAll();
                    }
                    else
                    {
                        refreshButton.Visible = true;

                        // Notify user, there is no exercise
                        MessageBox.Show("Kan geen oefening vinden, neem contact op met de systeembeheerder.", "Kom in Beweging - Fout",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine("ERROR: empty content: " + content);
                    }
                }
            }
            catch (Exception e)
            {
                refreshButton.Visible = true;

                // Notify user, can't get from the server
                MessageBox.Show("Kan geen gegevens ophalen van de server, neem contact op met de systeembeheerder.", "Kom in Beweging - Fout",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("ERROR: " + e.Message);
            }

        }

        private async System.Threading.Tasks.Task doneExerciseAsync(string done)
        {

            var client = new RestClient(Globals.HOST_ADDRESS_CALLS);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("treatment/exercise-done", Method.PUT);
            request.AddParameter("done", done); // adds to POST or URL querystring based on Method
            request.AddParameter("treatment_exercise_id", Globals.TreatmentExerciseID); 

            //TODO implement auth jwt        
            request.AddHeader("authorization", Properties.Settings.Default.jwt);
            request.Timeout = 2000;

            // execute the request
            var response = await client.ExecuteTaskAsync(request);
            var content = response.Content; // raw content as string  


            // Check if response is not null
            try
            {
                if (!content.Equals(null))
                {
                    setExercise(content);
                }
            }
            catch (Exception e)
            {
                // Notify user, can't get from the server
                MessageBox.Show("Kan geen gegevens sturen naar de server, neem contact op met de systeembeheerder.", "Kom in Beweging - Fout",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("ERROR: " + e.Message);
            }

        }

        private async System.Threading.Tasks.Task rateExerciseAsync(string rating)
        {

            var client = new RestClient(Globals.HOST_ADDRESS_CALLS);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("exercise/rate", Method.PUT);
            request.AddParameter("rating", rating); // adds to POST or URL querystring based on Method
            request.AddParameter("treatment_exercise_id", Globals.TreatmentExerciseID);

            //TODO implement auth jwt        
            request.AddHeader("authorization", Properties.Settings.Default.jwt);
            request.Timeout = 2000;

            // execute the request
            var response = await client.ExecuteTaskAsync(request);
            var content = response.Content; // raw content as string  

            // Check if response is not null
            try
            {
                if (!content.Equals(null))
                {
                    setExercise(content);
                }
            }
            catch (Exception e)
            {
                // Notify user, can't get from the server
                MessageBox.Show("Kan geen gegevens sturen naar de server, neem contact op met de systeembeheerder.", "Fout",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("ERROR: " + e.Message);
            }

        }


        /**
        * --------------   CONTENT FILLERS    ----------------
         **/
        private void setExercise(string JSONResponse)
        {

                dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(JSONResponse);
                for (var i = 0; i < data.Count; i++)
                {
                    dynamic item = data[i];
                    Console.WriteLine((string)item.repetitions, (string)item.name);
                    exerciseName.Text = (string)item.name;
                    exerciseRepetitions.Text = (string)item.repetitions;

                // Set buttons
                if ((string)item.done == "1") {
                    buttonDone.BackColor = Color.FromArgb(191, 229, 191);
                }
                else if ((string)item.done == "-1") {
                    buttonNotDone.BackColor = Color.FromArgb(249, 156, 156);
                }

                if ((string)item.rating_user == "1")
                {
                    buttonLike.BackColor = Color.FromArgb(191, 229, 191);
                }
                else if ((string)item.rating_user == "-1")
                {
                    buttonDislike.BackColor = Color.FromArgb(249, 156, 156);
                }

                // Save treatment_exercise_id for later use
                Globals.TreatmentExerciseID = (string)item.treatment_exercise_id;

                    // Fill webbrowser for video, insert correct video url
                exerciseVideoBrowser.DocumentText = "<!DOCTYPE html>" +
                        "<html lang = 'en' xmlns = 'http://www.w3.org/1999/xhtml'>" +
                        "<head >" +
                        "<meta http-equiv = 'X-UA-Compatible' content = 'IE=edge' />" +
                        "</head >" +
                        "<body oncontextmenu='return false;' style='user-select: none;-ms-user-select:none; -moz-user-select: none;  -webkit-user-select: none;background-color:#efeaea;top:0; left:0; margin:0; border:none;height:349px; width:620px' >" +
                        "<div style = 'background-color:#efeaea;overflow:hidden;height:100%; width:100%;' >" +
                        "<iframe allowfullscreen='allowfullscreen' style ='background-color:#efeaea;overflow:hidden;top:0; left:0; margin:0; border:none;height:100%; width:100%;' src =" +
                        "'" + (string)item.media_url + "?autoplay=0&showinfo=0&controls=1&rel=0' allowfullscreen>" +
                        "</iframe >" +
                        "</div>" +
                        "</body>" +
                        "</html> ";

                // TODO image extension in database

                    // Fill webbrowser for video, insert correct image url
                    exerciseImageBrowser.DocumentText = "<!DOCTYPE html>" +
                        "<html lang = 'en' xmlns = 'http://www.w3.org/1999/xhtml'>" +
                        "<head>" +
                        "<meta http-equiv = 'X-UA-Compatible' content = 'IE=edge' />" +
                        "</head>" +
                        "<body oncontextmenu='return false;' style='pointer-events: none;user-select: none;-ms-user-select:none; -moz-user-select: none;  -webkit-user-select: none;background-color:#efeaea;top:0; left:0; margin:0; border:none;height:208px; width:258px'>" +
                        "<div style = 'background-color:#efeaea;overflow:hidden;height:208px; width:258px;'>" +
                        "<img style = 'box-shadow: 0px 0px 16px #888888;display:block;background-color:#efeaea;overflow:hidden;top:0; left:0; margin:8px; border:none;height:auto;width:auto;max-height:192px; max-width:243px;' src=" +
                        "'" + Globals.HOST_ADDRESS_IMAGES + (string)item.image_url + ".jpg" + "'>" +
                        "</div>" +
                        "</body>" +
                        "</html> ";

                    // Fill webbrowser for description
                    exerciseDescriptionBrowser.DocumentText = "<!DOCTYPE html>" +
                        "<html lang = 'en' xmlns = 'http://www.w3.org/1999/xhtml'>" +
                        "<head>" +
                        "<meta http-equiv = 'X-UA-Compatible' content = 'IE=edge' />" +
                        "</head>" +
                        "<body oncontextmenu='return false;' style='cursor:default;user-select: none;-ms-user-select:none; -moz-user-select: none;  -webkit-user-select: none;  background-color:#efeaea;top:0; left:0; margin:0; border:none;height:100%; width:100%' >" +
                        "<div style = 'font-size: 14px; overflow-y:auto; font-family:Helvetica,Arial,sans-serif; font-weight:bold; color: rgba(0, 0, 0, 0.65); background-color:#efeaea;height:100%; width:100%;' >" +
                        (string)item.description +
                        "</div>" +
                        "</body>" +
                        "</html> ";

                }
            
        }


        /**
        * --------------   UTILITIES    ----------------
         **/

        private void hideAll() {
            exerciseName.Visible = false;
            exerciseRepetitions.Visible = false;
            exerciseDescriptionBrowser.Visible = false;
            exerciseImageBrowser.Visible = false;
            exerciseVideoBrowser.Visible = false;
            label2.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            buttonDone.Visible = false;
            buttonNotDone.Visible = false;
            buttonLike.Visible = false;
            buttonDislike.Visible = false;
            doneWithExercise.Visible = false;
            comboBox1.Visible = false;
            button1.Visible = false;

            refreshButton.Visible = false;
        }

        private void showAll()
        {
            exerciseName.Visible = true;
            exerciseRepetitions.Visible = true;
            exerciseDescriptionBrowser.Visible = true;
            exerciseImageBrowser.Visible = true;
            exerciseVideoBrowser.Visible = true;
            label2.Visible = true;
            pictureBox2.Visible = true;
            pictureBox3.Visible = true;
            buttonDone.Visible = true;
            buttonNotDone.Visible = true;
            buttonLike.Visible = true;
            buttonDislike.Visible = true;
            doneWithExercise.Visible = true;
            comboBox1.Visible = true;
            button1.Visible = true;

        }

        private void showExerciseForm()
        {
            WindowState = FormWindowState.Normal;
            Show();
        }

        private void hideExerciseForm()
        {
            exerciseVideoBrowser.Refresh();

            Hide();
            WindowState = FormWindowState.Minimized;
        }

        private void exitApplication()
        {
            notifyIcon1.Visible = false;
            notifyIcon1.Icon.Dispose();
            Environment.Exit(0);

        }

        private bool authorized()
        {

            try
            {
                if (!Properties.Settings.Default.jwt.Equals(null))
                {
                    if (!Properties.Settings.Default.jwt.Equals(""))
                    {
                        Console.WriteLine("NO ERROR: empty content: " + Properties.Settings.Default.jwt);
                        return true;

                    }
                    else
                    {
                        Console.WriteLine("FERROR: empty content: " + Properties.Settings.Default.jwt);
                        return false;

                    }
                }
                else {
                    return false;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("AERROR: " + e.Message);
                return false;
            }
        }
        private class Item
        {
            public string Name;
            public int Value;
            public Item(string name, int value)
            {
                Name = name; Value = value;
            }
            public override string ToString()
            {
                // Generates the text shown in the combo box
                return Name;
            }
        }
    }
}
