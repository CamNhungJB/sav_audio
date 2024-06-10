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
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection;

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

        public class Song
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("album")]
            public string Album { get; set; }

            [JsonProperty("artist")]
            public string Artist { get; set; }

            [JsonProperty("length")]
            public string Length { get; set; }

        }


        public MusicPlayerForm(string token)
        {
            InitializeComponent();
            this.token = token;

            vlcControl = new VlcControl();
            vlcControl.Dock = DockStyle.Fill;
            this.Controls.Add(vlcControl);
        }

        private async void btnLoadSongs_Click(object sender, EventArgs e)
        {
            var songsUrl = "http://localhost:3000/api/audio/songs";

            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync(songsUrl);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var songs = JsonConvert.DeserializeObject<List<Song>>(responseBody);

                    listViewSongs.Items.Clear();

                    foreach (var song in songs)
                    {
                        var item = new ListViewItem(new[] { song.Id.ToString(), song.Title, song.Album, song.Artist, song.Length });
                        listViewSongs.Items.Add(item);
                    }
                }
                else
                {
                    Console.WriteLine(response.Content);
                    MessageBox.Show("Failed to load songs.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        private async void btnPlaySong_Click(object sender, EventArgs e)
        {
            if (listViewSongs.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a song to play.");
                return;
            }

            var selectedSong = listViewSongs.SelectedItems[0];
            var songTitle = selectedSong.SubItems[1].Text; // Assuming the title is in the second column
            var streamUrl = $"http://localhost:3000/api/audio/songs/stream/{Uri.EscapeDataString(songTitle)}";

            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync(streamUrl);

                if (response.IsSuccessStatusCode)
                {
                    var sasUrl = await response.Content.ReadAsStringAsync();

                    // Log the URL for debugging
                    Console.WriteLine($"SAS URL: {sasUrl}");

                    // Ensure the SAS URL is properly trimmed and valid
                    sasUrl = sasUrl.Trim('"');

                    // Validate the SAS URL
                    if (Uri.TryCreate(sasUrl, UriKind.Absolute, out Uri validUri))
                    {
                        // Log the valid URI
                        Console.WriteLine($"Valid URI: {validUri}");

                        // Use the VLC control to play the valid URI
                        vlcControl.Play(validUri);
                        Console.WriteLine("Playing song..." + vlcControl.Length);
                    }
                    else
                    {
                        MessageBox.Show("The SAS URL is invalid.");
                        Console.WriteLine($"Invalid URI: {sasUrl}");
                    }
                }
                else
                {
                    MessageBox.Show("Failed to stream song.");
                    Console.WriteLine($"Response status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                Console.WriteLine($"Exception: {ex}");
            }
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
        }
    }
}
  