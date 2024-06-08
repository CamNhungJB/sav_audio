using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using NAudio.Wave;
using Vlc.DotNet.Core;
using Vlc.DotNet.Forms;

namespace client_app
{
    public partial class MusicPlayerForm : Form
    {
        private HttpClient client = new HttpClient();
        private string token;
        private IWavePlayer waveOutDevice;
        private AudioFileReader audioFileReader;
        private List<string> songNames = new List<string>();
        private string currentFilePath;
        private VlcControl vlcControl;

        public MusicPlayerForm(string token)
        {
            InitializeComponent();
            this.token = token;
            Console.WriteLine(token);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            vlcControl = new VlcControl();
            vlcControl.Dock = DockStyle.Fill;
            this.Controls.Add(vlcControl);
        }

        private async void btnLoadSongs_Click(object sender, EventArgs e)
        {
            await LoadSongsAsync();
        }

        private async void btnPlaySong_Click(object sender, EventArgs e)
        {
            ListBox songsListBox = this.Controls["songsListBox"] as ListBox;
            if (songsListBox.SelectedItem != null)
            {
                string songName = songsListBox.SelectedItem.ToString();
                string streamUrl = await GetSongStreamUrlAsync(songName);
                if (!string.IsNullOrEmpty(streamUrl))
                {
                    await PlayMusicAsync(streamUrl, songName);
                }
            }
            else
            {
                MessageBox.Show("Please select a song first.");
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            waveOutDevice?.Pause();
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            waveOutDevice?.Play();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopMusic();
        }

        /*private async void btnPlaySong_Click(object sender, EventArgs e)
        {
            ListBox songsListBox = this.Controls["songsListBox"] as ListBox;
            if (songsListBox.SelectedItem != null)
            {
                string songName = songsListBox.SelectedItem.ToString();
                string hlsUrl = await GetHLSStreamUrlAsync(songName);
                
                if (!string.IsNullOrEmpty(hlsUrl))
                {
                    PlayMedia(hlsUrl);
                }
            }
            else
            {
                MessageBox.Show("Please select a song first.");
            }
        }*/


        /* private async Task LoadSongsAsync()
         {
             try
             {
                 string url = "http://localhost:3000/api/audio/songs"; // Adjust this URL if needed
                 client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                 HttpResponseMessage response = await client.GetAsync(url);
                 response.EnsureSuccessStatusCode();

                 string responseBody = await response.Content.ReadAsStringAsync();
                 JArray songs = JArray.Parse(responseBody);

                 ListBox songsListBox = this.Controls["songsListBox"] as ListBox;
                 songsListBox.Items.Clear();
                 songNames.Clear();

                 foreach (var song in songs)
                 {
                     songsListBox.Items.Add(song["name"].ToString());
                     songNames.Add(song["name"].ToString());
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show("Error loading songs: " + ex.Message);
             }
         }*/




        /*private void PlayMedia(string url)
        {
            vlcControl.SetMedia(new Uri(url));
            vlcControl.Play();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            vlcControl.Pause();
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            vlcControl.Play();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            vlcControl.Stop();
        }*/

        private async Task LoadSongsAsync()
        {
            try
            {
                string url = "http://localhost:3000/api/audio/songs";
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                JArray songs = JArray.Parse(responseBody);

                ListBox songsListBox = this.Controls["songsListBox"] as ListBox;
                songsListBox.Items.Clear();
                songNames.Clear();

                foreach (var song in songs)
                {
                    songsListBox.Items.Add(song["name"].ToString());
                    songNames.Add(song["name"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading songs: " + ex.Message);
            }
        }

        private async Task<string> GetHLSStreamUrlAsync(string songName)
        {
            try
            {
                string url = $"http://localhost:3000/api/audio/play/{songName}";
                /*url = url.Replace(" ", "%20");
                textBoxDebug.Text = url;*/
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                Console.WriteLine("ok3");
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("ok4");
                JObject responseJson = JObject.Parse(responseBody);
                Console.WriteLine("ok5"); 
                return responseJson["url"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting HLS stream URL: " + ex.Message);
                return null;
            }
        }

        private async Task<string> GetSongStreamUrlAsync(string songName)
        {
            try
            {
                string url = $"http://localhost:3000/api/audio/stream/{songName}";
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                JObject responseJson = JObject.Parse(responseBody);

                return responseJson["url"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting song stream URL: " + ex.Message);
                return null;
            }
        }

        private async Task PlayMusicAsync(string url, string songName)
        {
            string extension = Path.GetExtension(songName);
            string tempFilePath = Path.Combine(Path.GetTempPath(), "downloadedMusic" + extension);

            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                Console.WriteLine(response);
                if (response.IsSuccessStatusCode)
                {
                    using (var fs = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        await response.Content.CopyToAsync(fs);
                    }

                    StopMusic();
                    currentFilePath = tempFilePath;
                    PlayMusic(tempFilePath);
                }
                else
                {
                    MessageBox.Show("Failed to download the song.");
                }
            }
        }

        private void PlayMusic(string filePath)
        {
            waveOutDevice = new WaveOut();
            audioFileReader = new AudioFileReader(filePath);
            waveOutDevice.Init(audioFileReader);
            waveOutDevice.Play();
        }

        private void StopMusic()
        {
            waveOutDevice?.Stop();
            audioFileReader?.Dispose();
            waveOutDevice?.Dispose();
            currentFilePath = null;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            StopMusic();
            base.OnFormClosing(e);
            /*vlcControl.Stop();
            vlcControl.Dispose();*/
        }
    }
}
