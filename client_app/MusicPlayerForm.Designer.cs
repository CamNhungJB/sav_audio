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
            this.listViewSongs = new System.Windows.Forms.ListView();
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Title = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Album = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Artist = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Length = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // btnPlaySong
            // 
            this.btnPlaySong.Location = new System.Drawing.Point(470, 393);
            this.btnPlaySong.Name = "btnPlaySong";
            this.btnPlaySong.Size = new System.Drawing.Size(75, 23);
            this.btnPlaySong.TabIndex = 0;
            this.btnPlaySong.Text = "Play";
            this.btnPlaySong.UseVisualStyleBackColor = true;
            this.btnPlaySong.Click += new System.EventHandler(this.btnPlaySong_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(551, 393);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 0;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnResume
            // 
            this.btnResume.Location = new System.Drawing.Point(632, 393);
            this.btnResume.Name = "btnResume";
            this.btnResume.Size = new System.Drawing.Size(75, 23);
            this.btnResume.TabIndex = 0;
            this.btnResume.Text = "Resume";
            this.btnResume.UseVisualStyleBackColor = true;
            this.btnResume.Click += new System.EventHandler(this.btnResume_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(713, 393);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 0;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnLoadSongs
            // 
            this.btnLoadSongs.Location = new System.Drawing.Point(12, 380);
            this.btnLoadSongs.Name = "btnLoadSongs";
            this.btnLoadSongs.Size = new System.Drawing.Size(97, 48);
            this.btnLoadSongs.TabIndex = 1;
            this.btnLoadSongs.Text = "Load songs";
            this.btnLoadSongs.UseVisualStyleBackColor = true;
            this.btnLoadSongs.Click += new System.EventHandler(this.btnLoadSongs_Click);
            // 
            // listViewSongs
            // 
            this.listViewSongs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.Title,
            this.Album,
            this.Artist,
            this.Length});
            this.listViewSongs.GridLines = true;
            this.listViewSongs.HideSelection = false;
            this.listViewSongs.Location = new System.Drawing.Point(12, 12);
            this.listViewSongs.Name = "listViewSongs";
            this.listViewSongs.Size = new System.Drawing.Size(776, 342);
            this.listViewSongs.TabIndex = 4;
            this.listViewSongs.UseCompatibleStateImageBehavior = false;
            this.listViewSongs.View = System.Windows.Forms.View.Details;
            // 
            // ID
            // 
            this.ID.Text = "ID";
            this.ID.Width = 32;
            // 
            // Title
            // 
            this.Title.Text = "Title";
            this.Title.Width = 226;
            // 
            // Album
            // 
            this.Album.Text = "Album";
            this.Album.Width = 115;
            // 
            // Artist
            // 
            this.Artist.Text = "Artist";
            this.Artist.Width = 110;
            // 
            // Length
            // 
            this.Length.Text = "Length";
            this.Length.Width = 54;
            // 
            // MusicPlayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listViewSongs);
            this.Controls.Add(this.btnLoadSongs);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnResume);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnPlaySong);
            this.Name = "MusicPlayerForm";
            this.Text = "MusicPlayerForm";
            this.Load += new System.EventHandler(this.MusicPlayerForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPlaySong;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnResume;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnLoadSongs;
        private System.Windows.Forms.ListView listViewSongs;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader Title;
        private System.Windows.Forms.ColumnHeader Album;
        private System.Windows.Forms.ColumnHeader Artist;
        private System.Windows.Forms.ColumnHeader Length;
    }
}