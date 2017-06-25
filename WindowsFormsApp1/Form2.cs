using RestSharp;
using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    /**
    * Form2, login form
    **/
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            initializeForm();
            
        }

        /**
        * Initialize form on creation
        * Add enter handlers to input fields
        * Check if email is available in properties
        **/
        private void initializeForm()
        {
            this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            inputEmail.KeyPress += new KeyPressEventHandler(CheckEnter);
            inputPassword.KeyPress += new KeyPressEventHandler(CheckEnter);


            if (Properties.Settings.Default.email != "")
            {
                inputEmail.Text = Properties.Settings.Default.email;
                this.ActiveControl = inputPassword;
            }
        }


        /**
        * --------------   USER INTERACTION    ----------------
        **/


        /**
        * If loginbutton is clicked, go to check before attempt
        **/
        private void loginButton_MouseClick(object sender, MouseEventArgs e)
        {
            CheckBeforeLogin();
        }

        /**
        * If the user closes this form, hide it instead of closing
        **/
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            e.Cancel = true;
            hideLoginForm();

        }

        /**
         * Handler for the enter key, login on enter
         **/
        private void CheckEnter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                // Enter key pressed
                CheckBeforeLogin();
            }
        }


        /**
        * --------------   SERVER CALLS    ----------------
        **/


        /**
        * POST request with user credentials, set jwt and email to properties after succesfull login
        * email
        * password
        **/
        private async Task loginAsync(string email, string password)
        {

            var client = new RestClient(Form1.Globals.HOST_ADDRESS_CALLS);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("user/login", Method.POST);
            request.AddParameter("email", email); // adds to POST or URL querystring based on Method
            request.AddParameter("password", password);

            request.Timeout = 2000;

            // execute the request
            var response = await client.ExecuteTaskAsync(request);

            HttpStatusCode statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;
            //Console.WriteLine("STATUS " + numericStatusCode);
            var content = response.Content; // raw content as string  

            // Check if response is not null
            try
            {
                if (!content.Equals(null))
                {
                        
                    // If timeout
                    if (numericStatusCode == 0)
                    {
                        errorMessageText.Text = "Geen verbinding met de server, neem contact op met de systeembeheerder";

                    // If statuscode != 200 or no content, show error
                    }
                    else if (content == "" || numericStatusCode != 200)
                    {
                        errorMessageText.Text = "Onjuist wachtwoord of email";
                    
                    }
                    else
                    {
                        // Login
                        dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                        //Console.WriteLine((string)item.token);
                        Properties.Settings.Default.jwt = ((string)item.token);
                        Properties.Settings.Default.email = email;
                        Properties.Settings.Default.Save(); // Saves settings in application configuration file

                        Form1 form1 = Application.OpenForms.OfType<Form1>().First();
                        form1.getExerciseAsync();

                        form1.WindowState = FormWindowState.Normal;
                        form1.Show();

                        inputPassword.Text = "";
                        this.Hide();
                    }
                }
            }
            catch (Exception e)
            {
                // Notify user, can't get from the server
                MessageBox.Show("Kan geen gegevens sturen naar de server, neem contact op met de systeembeheerder.", "Kom in Beweging - Fout",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /**
        * --------------   UTILITIES    ----------------
         **/


        /**
        * Hide this form
        **/
        private void hideLoginForm()
        {
            Hide();
            WindowState = FormWindowState.Minimized;
        }

        /**
        * Check if all fields are filled before login attempt, notify user if not
        **/
        private void CheckBeforeLogin() {

            // Reset label changes from old errors
            emailLabel.ForeColor = Color.Black;
            errorMessageText.Text = "";
            passwordLabel.ForeColor = Color.Black;

            // Change labels for potential errors
            bool cancel = false;

            if (inputEmail.Text == "")
            {
                cancel = true;
                emailLabel.ForeColor = Color.Red;
                errorMessageText.Text = "Voer alle velden in";

            }
            if (inputPassword.Text == "")
            {
                cancel = true;
                passwordLabel.ForeColor = Color.Red;
                errorMessageText.Text = "Voer alle velden in";
            }

            if (!cancel)
            {
                loginAsync(inputEmail.Text, inputPassword.Text);
            }
        }
    }
}
