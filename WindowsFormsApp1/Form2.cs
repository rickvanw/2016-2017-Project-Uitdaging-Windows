using RestSharp;
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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            initializeForm();
            
        }

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

        private void loginButton_MouseClick(object sender, MouseEventArgs e)
        {
            CheckBeforeLogin();
        }

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
            var content = response.Content; // raw content as string  


            // Check if response is not null
            try
            {
                if (!content.Equals(null))
                {

                    if (content == "")
                    {
                        errorMessageText.Text = "Onjuist wachtwoord of email";
                    }
                    else
                    {
                        // Login
                        Console.WriteLine("JWT: " + content);

                        dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                        Console.WriteLine((string)item.token);
                        Properties.Settings.Default.jwt = ((string)item.token);
                        Properties.Settings.Default.Save(); // Saves settings in application configuration file
                        Properties.Settings.Default.email = email;
                        Properties.Settings.Default.Save(); // Saves settings in application configuration file

                        Form fc = Application.OpenForms["Form1"];

                        fc.Show();

                        this.Close();
                    }
                    
                }
            }
            catch (Exception e)
            {
                // Notify user, can't get from the server
                MessageBox.Show("Kan geen gegevens sturen naar de server, neem contact op met de systeembeheerder.", "Kom in Beweging - Fout",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("TERROR: " + e.Message);
            }

        }
        private void CheckEnter(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                Console.WriteLine("ENTER");
                // Enter key pressed
                CheckBeforeLogin();
            }
        }

        private void CheckBeforeLogin() {
            emailLabel.ForeColor = Color.Black;
            errorMessageText.Text = "";
            passwordLabel.ForeColor = Color.Black;

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
