namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.quitTrayOption = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.loginTrayOption = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutTrayOption = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.openTrayOption = new System.Windows.Forms.ToolStripMenuItem();
            this.exerciseName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.exerciseRepetitions = new System.Windows.Forms.Label();
            this.exerciseVideoBrowser = new System.Windows.Forms.WebBrowser();
            this.exerciseDescriptionBrowser = new System.Windows.Forms.WebBrowser();
            this.exerciseImageBrowser = new System.Windows.Forms.WebBrowser();
            this.refreshButton = new System.Windows.Forms.Button();
            this.doneWithExercise = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.delayButton = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.buttonDislike = new System.Windows.Forms.Button();
            this.buttonLike = new System.Windows.Forms.Button();
            this.buttonNotDone = new System.Windows.Forms.Button();
            this.buttonDone = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.delayTimer = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "KomInBeweging";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.BalloonTipClicked += new System.EventHandler(this.notifyIcon1_BalloonTipClicked);
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quitTrayOption,
            this.toolStripSeparator2,
            this.loginTrayOption,
            this.logoutTrayOption,
            this.toolStripSeparator1,
            this.openTrayOption});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(127, 104);
            // 
            // quitTrayOption
            // 
            this.quitTrayOption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.quitTrayOption.Name = "quitTrayOption";
            this.quitTrayOption.Size = new System.Drawing.Size(126, 22);
            this.quitTrayOption.Text = "Afsluiten";
            this.quitTrayOption.Click += new System.EventHandler(this.quitTrayOption_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(123, 6);
            // 
            // loginTrayOption
            // 
            this.loginTrayOption.Name = "loginTrayOption";
            this.loginTrayOption.Size = new System.Drawing.Size(126, 22);
            this.loginTrayOption.Text = "Inloggen";
            this.loginTrayOption.Click += new System.EventHandler(this.loginTrayOption_Click);
            // 
            // logoutTrayOption
            // 
            this.logoutTrayOption.Name = "logoutTrayOption";
            this.logoutTrayOption.Size = new System.Drawing.Size(126, 22);
            this.logoutTrayOption.Text = "Uitloggen";
            this.logoutTrayOption.Click += new System.EventHandler(this.logoutTrayOption_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(123, 6);
            // 
            // openTrayOption
            // 
            this.openTrayOption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.openTrayOption.Name = "openTrayOption";
            this.openTrayOption.Size = new System.Drawing.Size(126, 22);
            this.openTrayOption.Text = "Openen";
            this.openTrayOption.Click += new System.EventHandler(this.openTrayOption_Click);
            // 
            // exerciseName
            // 
            this.exerciseName.AutoSize = true;
            this.exerciseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exerciseName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(163)))), ((int)(((byte)(231)))));
            this.exerciseName.Location = new System.Drawing.Point(60, 46);
            this.exerciseName.MaximumSize = new System.Drawing.Size(600, 67);
            this.exerciseName.Name = "exerciseName";
            this.exerciseName.Size = new System.Drawing.Size(205, 55);
            this.exerciseName.TabIndex = 2;
            this.exerciseName.Text = "Planken";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(163)))), ((int)(((byte)(231)))));
            this.label2.Location = new System.Drawing.Point(688, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 31);
            this.label2.TabIndex = 3;
            this.label2.Text = "Herhalingen";
            // 
            // exerciseRepetitions
            // 
            this.exerciseRepetitions.AutoSize = true;
            this.exerciseRepetitions.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exerciseRepetitions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(163)))), ((int)(((byte)(231)))));
            this.exerciseRepetitions.Location = new System.Drawing.Point(744, 80);
            this.exerciseRepetitions.Name = "exerciseRepetitions";
            this.exerciseRepetitions.Size = new System.Drawing.Size(52, 55);
            this.exerciseRepetitions.TabIndex = 4;
            this.exerciseRepetitions.Text = "5";
            // 
            // exerciseVideoBrowser
            // 
            this.exerciseVideoBrowser.Location = new System.Drawing.Point(128, 392);
            this.exerciseVideoBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.exerciseVideoBrowser.Name = "exerciseVideoBrowser";
            this.exerciseVideoBrowser.Size = new System.Drawing.Size(620, 349);
            this.exerciseVideoBrowser.TabIndex = 6;
            // 
            // exerciseDescriptionBrowser
            // 
            this.exerciseDescriptionBrowser.AllowNavigation = false;
            this.exerciseDescriptionBrowser.AllowWebBrowserDrop = false;
            this.exerciseDescriptionBrowser.IsWebBrowserContextMenuEnabled = false;
            this.exerciseDescriptionBrowser.Location = new System.Drawing.Point(336, 162);
            this.exerciseDescriptionBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.exerciseDescriptionBrowser.Name = "exerciseDescriptionBrowser";
            this.exerciseDescriptionBrowser.Size = new System.Drawing.Size(414, 208);
            this.exerciseDescriptionBrowser.TabIndex = 7;
            // 
            // exerciseImageBrowser
            // 
            this.exerciseImageBrowser.AllowNavigation = false;
            this.exerciseImageBrowser.AllowWebBrowserDrop = false;
            this.exerciseImageBrowser.IsWebBrowserContextMenuEnabled = false;
            this.exerciseImageBrowser.Location = new System.Drawing.Point(66, 154);
            this.exerciseImageBrowser.Name = "exerciseImageBrowser";
            this.exerciseImageBrowser.ScrollBarsEnabled = false;
            this.exerciseImageBrowser.Size = new System.Drawing.Size(258, 208);
            this.exerciseImageBrowser.TabIndex = 8;
            // 
            // refreshButton
            // 
            this.refreshButton.BackColor = System.Drawing.Color.Transparent;
            this.refreshButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.refreshButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.refreshButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.refreshButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.refreshButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.refreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshButton.ForeColor = System.Drawing.Color.Black;
            this.refreshButton.Location = new System.Drawing.Point(340, 368);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(200, 50);
            this.refreshButton.TabIndex = 15;
            this.refreshButton.Text = "Klik hier om opnieuw te proberen";
            this.refreshButton.UseVisualStyleBackColor = false;
            this.refreshButton.Visible = false;
            this.refreshButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.refreshButton_MouseClick);
            // 
            // doneWithExercise
            // 
            this.doneWithExercise.BackColor = System.Drawing.Color.White;
            this.doneWithExercise.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.doneWithExercise.Cursor = System.Windows.Forms.Cursors.Hand;
            this.doneWithExercise.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.doneWithExercise.FlatAppearance.BorderSize = 0;
            this.doneWithExercise.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.doneWithExercise.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(214)))), ((int)(((byte)(214)))));
            this.doneWithExercise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.doneWithExercise.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.doneWithExercise.ForeColor = System.Drawing.Color.Black;
            this.doneWithExercise.Location = new System.Drawing.Point(752, 768);
            this.doneWithExercise.Name = "doneWithExercise";
            this.doneWithExercise.Size = new System.Drawing.Size(100, 50);
            this.doneWithExercise.TabIndex = 16;
            this.doneWithExercise.Text = "Klaar";
            this.doneWithExercise.UseVisualStyleBackColor = false;
            this.doneWithExercise.MouseClick += new System.Windows.Forms.MouseEventHandler(this.doneWithExercise_MouseClick);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.Location = new System.Drawing.Point(24, 768);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(100, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // delayButton
            // 
            this.delayButton.BackColor = System.Drawing.Color.White;
            this.delayButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.delayButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.delayButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.delayButton.FlatAppearance.BorderSize = 0;
            this.delayButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.delayButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(214)))), ((int)(((byte)(214)))));
            this.delayButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.delayButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.delayButton.ForeColor = System.Drawing.Color.Black;
            this.delayButton.Location = new System.Drawing.Point(24, 789);
            this.delayButton.Name = "delayButton";
            this.delayButton.Size = new System.Drawing.Size(100, 29);
            this.delayButton.TabIndex = 17;
            this.delayButton.Text = "Uitstellen";
            this.delayButton.UseVisualStyleBackColor = false;
            this.delayButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.delayButton_MouseClick);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.pictureBox3.Image = global::WindowsFormsApp1.Properties.Resources.half_circle_right;
            this.pictureBox3.Location = new System.Drawing.Point(296, 768);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(50, 50);
            this.pictureBox3.TabIndex = 14;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.pictureBox2.Image = global::WindowsFormsApp1.Properties.Resources.half_circle_left;
            this.pictureBox2.Location = new System.Drawing.Point(536, 768);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(25, 50);
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
            // 
            // buttonDislike
            // 
            this.buttonDislike.BackColor = System.Drawing.Color.White;
            this.buttonDislike.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.dislike1;
            this.buttonDislike.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonDislike.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonDislike.FlatAppearance.BorderSize = 0;
            this.buttonDislike.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDislike.ForeColor = System.Drawing.Color.White;
            this.buttonDislike.Location = new System.Drawing.Point(488, 768);
            this.buttonDislike.Name = "buttonDislike";
            this.buttonDislike.Size = new System.Drawing.Size(50, 50);
            this.buttonDislike.TabIndex = 12;
            this.buttonDislike.UseVisualStyleBackColor = false;
            this.buttonDislike.Click += new System.EventHandler(this.button4_Click);
            // 
            // buttonLike
            // 
            this.buttonLike.BackColor = System.Drawing.Color.White;
            this.buttonLike.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.like1;
            this.buttonLike.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonLike.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonLike.FlatAppearance.BorderSize = 0;
            this.buttonLike.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLike.ForeColor = System.Drawing.Color.White;
            this.buttonLike.Location = new System.Drawing.Point(440, 768);
            this.buttonLike.Name = "buttonLike";
            this.buttonLike.Size = new System.Drawing.Size(50, 50);
            this.buttonLike.TabIndex = 11;
            this.buttonLike.UseVisualStyleBackColor = false;
            this.buttonLike.Click += new System.EventHandler(this.button3_Click);
            // 
            // buttonNotDone
            // 
            this.buttonNotDone.BackColor = System.Drawing.Color.White;
            this.buttonNotDone.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.close1;
            this.buttonNotDone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonNotDone.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonNotDone.FlatAppearance.BorderSize = 0;
            this.buttonNotDone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNotDone.ForeColor = System.Drawing.Color.White;
            this.buttonNotDone.Location = new System.Drawing.Point(392, 768);
            this.buttonNotDone.Name = "buttonNotDone";
            this.buttonNotDone.Size = new System.Drawing.Size(50, 50);
            this.buttonNotDone.TabIndex = 10;
            this.buttonNotDone.UseVisualStyleBackColor = false;
            this.buttonNotDone.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonDone
            // 
            this.buttonDone.BackColor = System.Drawing.Color.White;
            this.buttonDone.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.checkmark1;
            this.buttonDone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonDone.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonDone.FlatAppearance.BorderSize = 0;
            this.buttonDone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDone.ForeColor = System.Drawing.Color.White;
            this.buttonDone.Location = new System.Drawing.Point(344, 768);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(50, 50);
            this.buttonDone.TabIndex = 9;
            this.buttonDone.UseVisualStyleBackColor = false;
            this.buttonDone.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // delayTimer
            // 
            this.delayTimer.Tick += new System.EventHandler(this.delayTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.ClientSize = new System.Drawing.Size(884, 844);
            this.Controls.Add(this.delayButton);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.doneWithExercise);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.buttonDislike);
            this.Controls.Add(this.buttonLike);
            this.Controls.Add(this.buttonNotDone);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.exerciseDescriptionBrowser);
            this.Controls.Add(this.exerciseVideoBrowser);
            this.Controls.Add(this.exerciseRepetitions);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.exerciseName);
            this.Controls.Add(this.exerciseImageBrowser);
            this.Controls.Add(this.refreshButton);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kom in Beweging";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label exerciseName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label exerciseRepetitions;
        private System.Windows.Forms.WebBrowser exerciseVideoBrowser;
        private System.Windows.Forms.WebBrowser exerciseDescriptionBrowser;
        private System.Windows.Forms.WebBrowser exerciseImageBrowser;
        private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.Button buttonNotDone;
        private System.Windows.Forms.Button buttonLike;
        private System.Windows.Forms.Button buttonDislike;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Button doneWithExercise;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem quitTrayOption;
        private System.Windows.Forms.ToolStripMenuItem openTrayOption;
        private System.Windows.Forms.ToolStripMenuItem loginTrayOption;
        private System.Windows.Forms.ToolStripMenuItem logoutTrayOption;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button delayButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer delayTimer;
    }
}

