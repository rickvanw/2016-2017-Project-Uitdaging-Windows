using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using RestSharp;
using System.Runtime.InteropServices;
using System.IO;
using IWshRuntimeLibrary;
using System.Threading;
using System.Timers;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1
{

    /**
    * Form1, exercise form
    **/
    public partial class Form1 : Form
    {
        public static class Globals
        {
            // Host address for REST Calls
            public static String HOST_ADDRESS_CALLS = "http://localhost:8000/";

            // Host address for exercise images folder
            public static String HOST_ADDRESS_IMAGES = "http://localhost:63342/2016-2017-Project-Uitdaging-EHI2Va15-Web/kom_in_beweging/img/";

            /**
            * Global variable for the current exercise / treatment combination
            **/
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

        /**
        * Form onload, also create form2
        **/
        private void Form1_Load(object sender, EventArgs e)
        {
            initializeForm();
            Form form2 = new Form2();
            form2.Show();
            Form form3 = new Form3();
            form3.Show();

            System.Timers.Timer alignTimeTimer;
            alignTimeTimer = new System.Timers.Timer();
            var timeToAlign = 0;
            if (DateTime.Now.Minute < 30)
            {
                // Timer will be set to go until first 30 min
                timeToAlign = 30 - DateTime.Now.Minute;
                Console.WriteLine("timeToAlign 1: " + timeToAlign);
            }
            else {
                // Timer will be set to go until first 30 min
                timeToAlign = 60 - DateTime.Now.Minute;
                timeToAlign += 30;
                Console.WriteLine("timeToAlign 2: " + timeToAlign);

            }

            Console.WriteLine("timeToAlign 3: " + timeToAlign);

            // Change to seconds for debug
            alignTimeTimer.Interval = timeToAlign * 60 * 1000;
            //alignTimeTimer.Interval = timeToAlign * 1000;

            alignTimeTimer.Elapsed += OnAlignTimedEvent;

            alignTimeTimer.AutoReset = false;
            alignTimeTimer.Enabled = true;


            if (authorized())
            {
                getExerciseAsync();
            }
            else {
                hideAll();
                refreshButton.Visible = true;
                showLogin();
            }
        }

        /**
        * Initialize the form
        **/
        private void initializeForm()
        {
            // DEBUG CLEAR SETTINGS
            //Properties.Settings.Default.Reset();
            comboBox1.Items.Add(new Item("5 Minuten",5));
            comboBox1.Items.Add(new Item("15 Minuten", 15));
            comboBox1.Items.Add(new Item("30 Minuten", 30));
            comboBox1.Items.Add(new Item("45 Minuten", 45));
            comboBox1.SelectedIndex = 0;

            doneWithExercise.Select();

            this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            //enableAutoStartup();
        }

        /**
         * --------------   USER INTERACTION    ----------------
         **/

        /**
        * When the user clicks on the balloontip, show exercise
        **/
        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            Console.WriteLine("balloon");
            clearAll();
            getExerciseAsync();
            showExerciseForm();
        }

        /**
        * When the user clicks on the trayicon, show exercise
        **/
        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            // Only if left click
            if (e.Button == MouseButtons.Left)
            {
                showExerciseForm();
                //Console.WriteLine("notify");
            }
        }

        /**
        * Done button, to mark the exercise as done
        **/
        private void button1_Click(object sender, EventArgs e)
        {
            doneExerciseAsync("1");
            buttonDone.BackColor = Color.FromArgb(191, 229, 191);
            buttonNotDone.BackColor = Color.White;
        }

        /**
        * Not done button, to mark the exercise as not done
        **/
        private void button2_Click(object sender, EventArgs e)
        {
            doneExerciseAsync("-1");
            buttonDone.BackColor = Color.White;
            buttonNotDone.BackColor = Color.FromArgb(249, 156, 156);
        }

        /**
        * Like button, to rate the exercise
        **/
        private void button3_Click(object sender, EventArgs e)
        {
            rateExerciseAsync("1");
            buttonLike.BackColor = Color.FromArgb(191, 229, 191);
            buttonDislike.BackColor = Color.White;
        }

        /**
        * Dislike button, to rate the exercise
        **/
        private void button4_Click(object sender, EventArgs e)
        {
            rateExerciseAsync("-1");
            buttonLike.BackColor = Color.White;
            buttonDislike.BackColor = Color.FromArgb(249, 156, 156);
        }

        /**
        * If the user closes the form (with x button), hide it instead
        **/
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            e.Cancel = true;
            hideExerciseForm();

        }

        /**
        * Refreshbutton to try to get the exercise again, only shown when no exercise in form
        **/
        private void refreshButton_MouseClick(object sender, MouseEventArgs e)
        {
            getExerciseAsync();
        }

        /**
        * Hide the form if user is done with exercise
        **/
        private void doneWithExercise_MouseClick(object sender, MouseEventArgs e)
        {
            hideExerciseForm();
        }


        /**
        * Select something else after chosing delay time. 
        * This prevents combobox from being selected after chosing time (else it will stay blue)
        **/
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            doneWithExercise.Select();
        }

        /**
        * Button for delaying the exercise, takes the chosen time from the combobox
        **/
        private void delayButton_MouseClick(object sender, MouseEventArgs e)
        {
            delayTimer(comboBox1.Text);
            disableDelayExercise();
            disableDelayNotification();
        }

        // TRAY OPTIONS

        /**
        * Tray icon right click open option will show the current exercise
        **/
        private void quitTrayOption_Click(object sender, EventArgs e)
        {
            exitApplication();
        }

        /**
        * Tray icon right click open option will show the current exercise
        **/
        private void openTrayOption_Click(object sender, EventArgs e)
        {
            showExerciseForm();
        }

        /**
        * Tray icon right click login option will show the login form
        **/
        private void loginTrayOption_Click(object sender, EventArgs e)
        {
            showLogin();
        }

        /**
        * Tray icon right click logout option will clear user properties, hide current exercise
        **/
        private void logoutTrayOption_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.jwt = "";
            Properties.Settings.Default.email = "";
            Properties.Settings.Default.Save();
            clearAll();
            hideAll();
            refreshButton.Visible = true;
            Form form2 = Application.OpenForms["Form2"];
            MessageBox.Show(this, "U bent nu uitgelogd.", "Kom in Beweging - Uitgelogd");
        }


        /**
        * --------------   SERVER CALLS    ----------------
        **/


        /**
         * GET call to get all exercise items, fills exercise after getting
         **/
        public async System.Threading.Tasks.Task getExerciseAsync()
        {
            hideAll();
            var client = new RestClient(Globals.HOST_ADDRESS_CALLS);
            var request = new RestRequest("treatment/exercise-now", Method.GET);

            request.AddHeader("authorization", Properties.Settings.Default.jwt);
            request.Timeout = 2000;

            // execute the request
            var response = await client.ExecuteTaskAsync(request);

            HttpStatusCode statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var content = response.Content; // raw content as string  

            // Check if response is not null
            try
            {
                if (!content.Equals(null) && numericStatusCode == 200)
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
                        //MessageBox.Show("Kan geen oefening vinden, neem contact op met de systeembeheerder.", "Kom in Beweging - Fout",
                        //MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //Console.WriteLine("ERROR: empty content: " + response.ErrorMessage);
                    }
                }
                else {
                    refreshButton.Visible = true;
                }
            }
            catch (Exception e)
            {
                refreshButton.Visible = true;

                // Notify user, can't get from the server
                MessageBox.Show("Kan geen gegevens ophalen van de server, neem contact op met de systeembeheerder.", "Kom in Beweging - Fout",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Console.WriteLine("ERROR: " + e.Message);
            }

        }

        /**
        * PUT call to set the current exercise to done / not done
        * done - not done(-1) / done(1)
        **/
        private async System.Threading.Tasks.Task doneExerciseAsync(string done)
        {

            var client = new RestClient(Globals.HOST_ADDRESS_CALLS);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("treatment/exercise-done", Method.PUT);
            request.AddParameter("done", done); // adds to POST or URL querystring based on Method
            request.AddParameter("treatment_exercise_id", Globals.TreatmentExerciseID); 

            request.AddHeader("authorization", Properties.Settings.Default.jwt);
            request.Timeout = 2000;

            // execute the request
            var response = await client.ExecuteTaskAsync(request);

            HttpStatusCode statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var content = response.Content; // raw content as string  


            // Check if response is not null
            try
            {
                if (!content.Equals(null) && numericStatusCode == 200)
                {
                    setExercise(content);
                }
                else {
                    // Notify user, can't get from the server
                    MessageBox.Show("Kan geen gegevens sturen naar de server, neem contact op met de systeembeheerder.", "Kom in Beweging - Fout",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Console.WriteLine("ERROR: " + response.ErrorMessage);
                }
            }
            catch (Exception e)
            {
                // Notify user, can't get from the server
                MessageBox.Show("Kan geen gegevens sturen naar de server, neem contact op met de systeembeheerder.", "Kom in Beweging - Fout",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Console.WriteLine("ERROR: " + e.Message);
            }
        }

        /**
        * PUT call to set the user rating of current exercise
        * rating - user rating (-1 or 1)
        **/
        private async System.Threading.Tasks.Task rateExerciseAsync(string rating)
        {

            var client = new RestClient(Globals.HOST_ADDRESS_CALLS);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("exercise/rate", Method.PUT);
            request.AddParameter("rating", rating); // adds to POST or URL querystring based on Method
            request.AddParameter("treatment_exercise_id", Globals.TreatmentExerciseID);

            request.AddHeader("authorization", Properties.Settings.Default.jwt);
            request.Timeout = 2000;

            // execute the request
            var response = await client.ExecuteTaskAsync(request);

            HttpStatusCode statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var content = response.Content; // raw content as string  

            // Check if response is not null
            try
            {
                if (!content.Equals(null) && numericStatusCode == 200)
                {
                    setExercise(content);
                }
                else {
                    // Notify user, can't get from the server
                    MessageBox.Show("Kan geen gegevens sturen naar de server, neem contact op met de systeembeheerder.", "Fout",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Console.WriteLine("ERROR: " + response.ErrorMessage);
                }
            }
            catch (Exception e)
            {
                // Notify user, can't get from the server
                MessageBox.Show("Kan geen gegevens sturen naar de server, neem contact op met de systeembeheerder.", "Fout",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Console.WriteLine("ERROR: " + e.Message);
            }

        }


        /**
        * --------------   CONTENT FILLERS    ----------------
         **/


        /**
        * Fill all items in the form
        **/
        private void setExercise(string JSONResponse)
        {
                // Parse data from JSON
                dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(JSONResponse);
                for (var i = 0; i < data.Count; i++)
                {
                    dynamic item = data[i];
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

                //Console.WriteLine(JSONResponse);

                // Fill webbrowser for video, insert correct video url
                exerciseVideoBrowser.DocumentText = "<!DOCTYPE html>" +
                        "<html lang = 'en' xmlns = 'http://www.w3.org/1999/xhtml'>" +
                        "<head >" +
                        "<meta http-equiv = 'X-UA-Compatible' content = 'IE=edge' />" +
                        "</head >" +
                        "<body oncontextmenu='return false;' style='user-select: none;-ms-user-select:none; -moz-user-select: none;  -webkit-user-select: none;background-color:#efeaea;top:0; left:0; margin:0; border:none;height:349px; width:620px' >" +
                        "<div style = 'background-color:#efeaea;overflow:hidden;height:100%; width:100%;' >" +
                        "<iframe allowfullscreen='allowfullscreen' style ='background-color:#efeaea;overflow:hidden;top:0; left:0; margin:0; border:none;height:100%; width:100%;' src =" +
                        "'" + getEmbedUrl((string)item.media_url) + "?autoplay=0&showinfo=0&controls=0&rel=0' allowfullscreen>" +
                        "</iframe >" +
                        "</div>" +
                        "</body>" +
                        "</html> ";

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


        /**
        * Hide all items in the form, show refresh button
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
            delayButton.Visible = false;

            refreshButton.Visible = false;
        }

        /**
        * Show all items in the form
        **/
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
            delayButton.Visible = true;

        }

        /**
        * Clear all items in this form, so that they can be repopulated
        **/
        private void clearAll()
        {
            exerciseName.Text = "";
            exerciseRepetitions.Text = "";
            exerciseDescriptionBrowser.Navigate("about:blank");
            exerciseImageBrowser.Navigate("about:blank");
            exerciseVideoBrowser.Navigate("about:blank");
            buttonDone.BackColor = Color.White;
            buttonNotDone.BackColor = Color.White;
            buttonLike.BackColor = Color.White;
            buttonDislike.BackColor = Color.White;
            comboBox1.SelectedIndex = 0;
        }

        /**
        * Show this form
        **/
        private void showExerciseForm()
        {
            WindowState = FormWindowState.Normal;
            Show();
            BringToFront();

        }

        /**
        * Hide this form, refresh youtube video to make sure it stops playing
        **/
        private void hideExerciseForm()
        {
            exerciseVideoBrowser.Refresh();

            Hide();
            WindowState = FormWindowState.Minimized;
        }

        /**
        * Exit the application
        **/
        private void exitApplication()
        {
            notifyIcon1.Visible = false;
            notifyIcon1.Icon.Dispose();
            Environment.Exit(0);
        }

        /**
        * Returns true if user is authorized (jwt is found in properties)
        **/
        private bool authorized()
        {
            try
            {
                if (!Properties.Settings.Default.jwt.Equals(null))
                {
                    if (!Properties.Settings.Default.jwt.Equals(""))
                    {
                        return true;
                    }
                    else
                    {
                        //Console.WriteLine("ERROR: empty content: " + Properties.Settings.Default.jwt);
                        return false;
                    }
                }
                else {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /**
        * Item to put into the combobox for the delaymenu
        **/
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

        /**
        * Show the login form
        **/
        private void showLogin() {
            Form form2 = Application.OpenForms["Form2"];
            form2.Show();
            form2.WindowState = FormWindowState.Normal;
            form2.BringToFront();
        }

        /**
        * Show the notification form
        **/
        private void showNotification()
        {
            Form form3 = Application.OpenForms["Form3"];

            form3.Invoke((MethodInvoker)delegate ()
            {
                form3.Show();
                form3.WindowState = FormWindowState.Normal;
            });
        }

        /**
        * Disable delay functionality for the notification box
        **/
        private void disableDelayNotification() {

            Form3 form3 = (Form3)Application.OpenForms["Form3"];
            form3.Invoke((MethodInvoker)delegate ()
            {
                form3.disableDelayNotification();
            });
        }

        /**
        * Disable delay functionality for the exercise form (this form)
        **/
        public void disableDelayExercise()
        {
            delayButton.Enabled = false;
            comboBox1.Enabled = false;
        }

        /**
        * Enable delay functionality for the notification box
        **/
        private void enableDelayNotification()
        {

            Form3 form3 = (Form3)Application.OpenForms["Form3"];
            form3.Invoke((MethodInvoker)delegate ()
            {
                form3.enableDelayNotification();
            });
        }

        /**
        * Enable delay functionality for the exercise form (this form)
        **/
        private void enableDelayExercise()
        {
            Form1 form1 = (Form1)Application.OpenForms["Form1"];
            form1.Invoke((MethodInvoker)delegate ()
            {
                delayButton.Enabled = true;
                comboBox1.Enabled = true;
            });           
        }
       
        /**
        * Create embed url for normal "watch" youtube link
        **/
        public string getEmbedUrl(string url)
        {
            var YoutubeVideoRegex = new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)");
            Match youtubeMatch = YoutubeVideoRegex.Match(url);
            return youtubeMatch.Success ? "http://www.youtube.com/embed/" + youtubeMatch.Groups[1].Value : string.Empty;
        }

        /**
        * Put shortcut in startup folder to launch at startup
        **/
        private void enableAutoStartup()
        {
            var startupFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            WshShell shell = new WshShell();
            string shortcutAddress = startupFolder + @"\KomInBeweging.lnk";
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
            shortcut.Description = "Snelkoppeling om de 'Kom in Beweging' applicatie bij het inloggen te starten.";
            shortcut.WorkingDirectory = Application.StartupPath; /* working directory */
            shortcut.TargetPath = Application.ExecutablePath; /* path of the executable */
            shortcut.Save(); // save the shortcut 
        }

        /**
        * When align timer is finished (first half hour is reached), notify user with exercise, set hourly timer until shutdown
        **/
        private void OnAlignTimedEvent(Object source, ElapsedEventArgs e)
        {
            showNotification();
            enableDelayExercise();
            enableDelayNotification();

            System.Timers.Timer hourTimer;
            hourTimer = new System.Timers.Timer();

            // Set to seconds for debug
            hourTimer.Interval = 60 * 60 * 1000;
            //hourTimer.Interval = 60 * 1000;

            // Hook up the Elapsed event for the timer. 
            hourTimer.Elapsed += OnHourTimedEvent;

            // Have the timer fire repeated events (true is the default)
            hourTimer.AutoReset = true;

            // Start the timer
            hourTimer.Enabled = true;

        }

        /**
        * Every hour show notification, this means new exercise so delay is enabled again.
        **/
        private void OnHourTimedEvent(Object source, ElapsedEventArgs e)
        {
            showNotification();
            enableDelayExercise();
            enableDelayNotification();
            
        }

        /**
        * If delay is done, notify user
        **/
        private void OnDelayTimedEvent(Object source, ElapsedEventArgs e)
        {
            showNotification();
        }

        /**
        * Setup the delaytimer
        **/
        public void delayTimer(string timeText) {
            String minutes = timeText;
            //Console.WriteLine(minutes);
            string[] comboboxvalue = minutes.Split(' ');
            int minute = Int32.Parse(comboboxvalue[0]);
            //Console.WriteLine(minute);
            System.Timers.Timer delayTimer;
            delayTimer = new System.Timers.Timer();

            // Set to seconds for debug
            delayTimer.Interval = minute * 60 * 1000;
            //delayTimer.Interval = minute * 1000;

            // Hook up the Elapsed event for the timer. 
            delayTimer.Elapsed += OnDelayTimedEvent;

            // Have the timer fire repeated events (true is the default)
            delayTimer.AutoReset = false;

            // Start the timer
            delayTimer.Enabled = true;
            var item = comboBox1.SelectedIndex;
            hideExerciseForm();
        }      
    }
}
