namespace client_app
{
    partial class MusicPlayerForm
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
            this.btnPlaySong = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnResume = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnLoadSongs = new System.Windows.Forms.Button();
            this.songsListBox = new System.Windows.Forms.ListBox();
            this.textBoxDebug = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnPlaySong
            // 
            this.btnPlaySong.Location = new System.Drawing.Point(226, 399);
            this.btnPlaySong.Name = "btnPlaySong";
            this.btnPlaySong.Size = new System.Drawing.Size(75, 23);
            this.btnPlaySong.TabIndex = 0;
            this.btnPlaySong.Text = "Play";
            this.btnPlaySong.UseVisualStyleBackColor = true;
            this.btnPlaySong.Click += new System.EventHandler(this.btnPlaySong_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(307, 399);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 0;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnResume
            // 
            this.btnResume.Location = new System.Drawing.Point(388, 399);
            this.btnResume.Name = "btnResume";
            this.btnResume.Size = new System.Drawing.Size(75, 23);
            this.btnResume.TabIndex = 0;
            this.btnResume.Text = "Resume";
            this.btnResume.UseVisualStyleBackColor = true;
            this.btnResume.Click += new System.EventHandler(this.btnResume_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(469, 399);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 0;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnLoadSongs
            // 
            this.btnLoadSongs.Location = new System.Drawing.Point(587, 51);
            this.btnLoadSongs.Name = "btnLoadSongs";
            this.btnLoadSongs.Size = new System.Drawing.Size(134, 29);
            this.btnLoadSongs.TabIndex = 1;
            this.btnLoadSongs.Text = "Load songs";
            this.btnLoadSongs.UseVisualStyleBackColor = true;
            this.btnLoadSongs.Click += new System.EventHandler(this.btnLoadSongs_Click);
            // 
            // songsListBox
            // 
            this.songsListBox.FormattingEnabled = true;
            this.songsListBox.ItemHeight = 16;
            this.songsListBox.Location = new System.Drawing.Point(226, 51);
            this.songsListBox.Name = "songsListBox";
            this.songsListBox.Size = new System.Drawing.Size(318, 308);
            this.songsListBox.TabIndex = 2;
            // 
            // textBoxDebug
            // 
            this.textBoxDebug.Location = new System.Drawing.Point(31, 51);
            this.textBoxDebug.Multiline = true;
            this.textBoxDebug.Name = "textBoxDebug";
            this.textBoxDebug.Size = new System.Drawing.Size(156, 308);
            this.textBoxDebug.TabIndex = 3;
            // 
            // MusicPlayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBoxDebug);
            this.Controls.Add(this.songsListBox);
            this.Controls.Add(this.btnLoadSongs);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnResume);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnPlaySong);
            this.Name = "MusicPlayerForm";
            this.Text = "MusicPlayerForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPlaySong;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnResume;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnLoadSongs;
        private System.Windows.Forms.ListBox songsListBox;
        private System.Windows.Forms.TextBox textBoxDebug;
    }
}