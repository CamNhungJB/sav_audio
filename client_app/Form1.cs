using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using NAudio.Wave;
using System.IO;
using System.Media;

namespace client_app
{
    public partial class Form1 : Form
    {
        public class TokenResponse
        {
            [JsonProperty("token")]
            public string Token { get; set; }
        }

        public class Song
        {
            public string name { get; set; }
        }

        private HttpClient client = new HttpClient();
        private string token;
        private IWavePlayer waveOutDevice;
        private AudioFileReader audioFileReader;
        private List<string> songNames = new List<string>();
        private string currentFilePath;

        public Form1()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var username = txtUsername.Text;
            var password = txtPassword.Text;
            var loginUrl = "http://localhost:3000/api/auth/login";

            var loginData = new
            {
                username = username,
                password = password
            };

            var content = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(loginUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var tokenData = JsonConvert.DeserializeObject<TokenResponse>(responseBody);
                    token = tokenData.Token;
                    txtToken.Text = token;
                    lblMessage.Text = "Login successful!";
                }
                else
                {
                    lblMessage.Text = "Login failed. Invalid credentials.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error: {ex.Message}";
            }
        }

        private void btnOpenRegister_Click(object sender, EventArgs e)
        {
            Form2 registerForm = new Form2();
            registerForm.ShowDialog();
        }

        private async void btnGetSongs_Click(object sender, EventArgs e)
        {
            await LoadSongsAsync();
            /* if (string.IsNullOrEmpty(token))
             {
                 lblMessage.Text = "You must log in first.";
                 return;
             }

             var songsUrl = "http://localhost:3000/api/audio/songs";
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

             try
             {
                 var response = await client.GetAsync(songsUrl);
                 if (response.IsSuccessStatusCode)
                 {
                     var responseBody = await response.Content.ReadAsStringAsync();
                     var songs = JsonConvert.DeserializeObject<List<Song>>(responseBody);
                     listBoxSongs.DataSource = songs;
                     listBoxSongs.DisplayMember = "name";
                     lblMessage.Text = "Songs loaded successfully.";
                 }
                 else
                 {
                     lblMessage.Text = $"Failed to get songs: {response.ReasonPhrase}";
                 }
             }
             catch (Exception ex)
             {
                 lblMessage.Text = $"Error: {ex.Message}";
             }*/
        }

        private async void PlaySongButton_Click(object sender, EventArgs e)
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

        private async Task LoadSongsAsync()
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
        }

        private async Task<string> GetSongStreamUrlAsync(string songName)
        {
            try
            {
                string url = $"http://localhost:3000/api/audio/stream/{songName}"; // Adjust this URL if needed
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
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
            Console.WriteLine(tempFilePath);
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    using (var fs = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        await response.Content.CopyToAsync(fs);
                    }

                    // Stop any currently playing music
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

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            StopMusic();
            base.OnFormClosing(e);
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            waveOutDevice?.Pause();
        }

        private void ResumeButton_Click(object sender, EventArgs e)
        {
            waveOutDevice?.Play();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            StopMusic();
        }

        private void StopMusic()
        {
            waveOutDevice?.Stop();
            audioFileReader?.Dispose();
            waveOutDevice?.Dispose();
            currentFilePath = null;
        }

        /*        private async void listBoxSongs_SelectedIndexChanged(object sender, EventArgs e)
                {
                    var selectedSong = listBoxSongs.SelectedItem as Song;
                    if (selectedSong != null)
                    {
                        var playUrl = $"http://localhost:3000/api/audio/stream/{selectedSong.name}";
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                        try
                        {
                            var response = await client.GetAsync(playUrl);
                            if (response.IsSuccessStatusCode)
                            {
                                var responseBody = await response.Content.ReadAsStringAsync();
                                var responseJson = JObject.Parse(responseBody);
                                var sasUrl = responseJson["url"].ToString();
                                txtCurrentSong.Text = sasUrl;
                                PlaySong(sasUrl);
                                lblMessage.Text = "Playing song...";
                            }
                            else
                            {
                                lblMessage.Text = $"Failed to get song URL: {response.ReasonPhrase}";
                            }
                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = $"Error: {ex.Message}";
                        }
                    }
                }

                private async void PlaySong(string url)
                {
                    *//*if (mediaReader != null)
                    {
                        mediaReader.Dispose();
                    }

                    try
                    {
                        mediaReader = new MediaFoundationReader(url);
                        waveOut.Init(mediaReader);
                        waveOut.Play();
                        MessageBox.Show("Enjoying your songs");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error playing song: {ex.Message}");
                    }*//*
                    byte[] audioData = await client.GetByteArrayAsync(url);
                    MemoryStream audioStream = new MemoryStream(audioData);
                    SoundPlayer player = new SoundPlayer(audioStream);
                    player.PlaySync();
                }*/
    }
}
