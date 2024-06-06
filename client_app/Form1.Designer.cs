namespace client_app
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
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnOpenRegister = new System.Windows.Forms.Button();
            this.txtToken = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnGetSongs = new System.Windows.Forms.Button();
            this.songsListBox = new System.Windows.Forms.ListBox();
            this.txtCurrentSong = new System.Windows.Forms.TextBox();
            this.PlaySongButton = new System.Windows.Forms.Button();
            this.PauseButton = new System.Windows.Forms.Button();
            this.ResumeButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(105, 137);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(219, 22);
            this.txtPassword.TabIndex = 0;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(105, 92);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(219, 22);
            this.txtUsername.TabIndex = 0;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(105, 209);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 35);
            this.btnLogin.TabIndex = 1;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(18, 27);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 16);
            this.lblMessage.TabIndex = 2;
            // 
            // btnOpenRegister
            // 
            this.btnOpenRegister.Location = new System.Drawing.Point(249, 209);
            this.btnOpenRegister.Name = "btnOpenRegister";
            this.btnOpenRegister.Size = new System.Drawing.Size(75, 35);
            this.btnOpenRegister.TabIndex = 8;
            this.btnOpenRegister.Text = "Register";
            this.btnOpenRegister.UseVisualStyleBackColor = true;
            this.btnOpenRegister.Click += new System.EventHandler(this.btnOpenRegister_Click);
            // 
            // txtToken
            // 
            this.txtToken.Location = new System.Drawing.Point(802, 12);
            this.txtToken.Name = "txtToken";
            this.txtToken.Size = new System.Drawing.Size(298, 22);
            this.txtToken.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(741, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Token";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(465, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Songs";
            // 
            // btnGetSongs
            // 
            this.btnGetSongs.Location = new System.Drawing.Point(146, 289);
            this.btnGetSongs.Name = "btnGetSongs";
            this.btnGetSongs.Size = new System.Drawing.Size(131, 31);
            this.btnGetSongs.TabIndex = 12;
            this.btnGetSongs.Text = "Get songs";
            this.btnGetSongs.UseVisualStyleBackColor = true;
            this.btnGetSongs.Click += new System.EventHandler(this.btnGetSongs_Click);
            // 
            // songsListBox
            // 
            this.songsListBox.FormattingEnabled = true;
            this.songsListBox.ItemHeight = 16;
            this.songsListBox.Location = new System.Drawing.Point(459, 71);
            this.songsListBox.Name = "songsListBox";
            this.songsListBox.Size = new System.Drawing.Size(338, 260);
            this.songsListBox.TabIndex = 13;
            // 
            // txtCurrentSong
            // 
            this.txtCurrentSong.Location = new System.Drawing.Point(762, 40);
            this.txtCurrentSong.Name = "txtCurrentSong";
            this.txtCurrentSong.Size = new System.Drawing.Size(338, 22);
            this.txtCurrentSong.TabIndex = 14;
            // 
            // PlaySongButton
            // 
            this.PlaySongButton.Location = new System.Drawing.Point(441, 356);
            this.PlaySongButton.Name = "PlaySongButton";
            this.PlaySongButton.Size = new System.Drawing.Size(75, 23);
            this.PlaySongButton.TabIndex = 15;
            this.PlaySongButton.Text = "Play";
            this.PlaySongButton.UseVisualStyleBackColor = true;
            this.PlaySongButton.Click += new System.EventHandler(this.PlaySongButton_Click);
            // 
            // PauseButton
            // 
            this.PauseButton.Location = new System.Drawing.Point(536, 356);
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Size = new System.Drawing.Size(75, 23);
            this.PauseButton.TabIndex = 16;
            this.PauseButton.Text = "Pause";
            this.PauseButton.UseVisualStyleBackColor = true;
            this.PauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // ResumeButton
            // 
            this.ResumeButton.Location = new System.Drawing.Point(630, 356);
            this.ResumeButton.Name = "ResumeButton";
            this.ResumeButton.Size = new System.Drawing.Size(75, 23);
            this.ResumeButton.TabIndex = 17;
            this.ResumeButton.Text = "Resume";
            this.ResumeButton.UseVisualStyleBackColor = true;
            this.ResumeButton.Click += new System.EventHandler(this.ResumeButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(722, 356);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 23);
            this.StopButton.TabIndex = 17;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 403);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.ResumeButton);
            this.Controls.Add(this.PauseButton);
            this.Controls.Add(this.PlaySongButton);
            this.Controls.Add(this.txtCurrentSong);
            this.Controls.Add(this.songsListBox);
            this.Controls.Add(this.btnGetSongs);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtToken);
            this.Controls.Add(this.btnOpenRegister);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtPassword);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button btnOpenRegister;
        private System.Windows.Forms.TextBox txtToken;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnGetSongs;
        private System.Windows.Forms.ListBox songsListBox;
        private System.Windows.Forms.TextBox txtCurrentSong;
        private System.Windows.Forms.Button PlaySongButton;
        private System.Windows.Forms.Button PauseButton;
        private System.Windows.Forms.Button ResumeButton;
        private System.Windows.Forms.Button StopButton;
    }
}

