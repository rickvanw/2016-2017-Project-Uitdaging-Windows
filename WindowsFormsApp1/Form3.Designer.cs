namespace WindowsFormsApp1
{
    partial class Form3
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
            this.delayButtonNotification = new System.Windows.Forms.Button();
            this.comboBox1Notification = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // delayButtonNotification
            // 
            this.delayButtonNotification.BackColor = System.Drawing.Color.White;
            this.delayButtonNotification.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.delayButtonNotification.Cursor = System.Windows.Forms.Cursors.Hand;
            this.delayButtonNotification.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.delayButtonNotification.FlatAppearance.BorderSize = 0;
            this.delayButtonNotification.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.delayButtonNotification.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(214)))), ((int)(((byte)(214)))));
            this.delayButtonNotification.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.delayButtonNotification.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.delayButtonNotification.ForeColor = System.Drawing.Color.Black;
            this.delayButtonNotification.Location = new System.Drawing.Point(192, 77);
            this.delayButtonNotification.Name = "delayButtonNotification";
            this.delayButtonNotification.Size = new System.Drawing.Size(100, 29);
            this.delayButtonNotification.TabIndex = 19;
            this.delayButtonNotification.Text = "Uitstellen";
            this.delayButtonNotification.UseVisualStyleBackColor = false;
            this.delayButtonNotification.MouseClick += new System.Windows.Forms.MouseEventHandler(this.delayButtonNotification_MouseClick);
            // 
            // comboBox1Notification
            // 
            this.comboBox1Notification.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1Notification.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1Notification.Location = new System.Drawing.Point(192, 56);
            this.comboBox1Notification.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox1Notification.Name = "comboBox1Notification";
            this.comboBox1Notification.Size = new System.Drawing.Size(100, 21);
            this.comboBox1Notification.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 26);
            this.label1.TabIndex = 20;
            this.label1.Text = "Nieuwe oefening!";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 18);
            this.label2.TabIndex = 21;
            this.label2.Text = "Klik hier om te beginnen";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(144)))), ((int)(((byte)(226)))));
            this.ClientSize = new System.Drawing.Size(300, 120);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.delayButtonNotification);
            this.Controls.Add(this.comboBox1Notification);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form3";
            this.Opacity = 0.95D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form3";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.Form3_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form3_MouseClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button delayButtonNotification;
        private System.Windows.Forms.ComboBox comboBox1Notification;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}