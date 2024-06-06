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
        private HttpClient client;
        private string token;
        private WaveOutEvent waveOut;
        private MediaFoundationReader mediaReader;

        public Form1()
        {
            InitializeComponent();
            client = new HttpClient();
            waveOut = new WaveOutEvent();
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
            if (string.IsNullOrEmpty(token))
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
            }
        }

        private async void listBoxSongs_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedSong = listBoxSongs.SelectedItem as Song;
            if (selectedSong != null)
            {
                try
                {
                    var audioPlayer = new AudioPlayer();
                    await audioPlayer.PlayAudio(selectedSong.name, token);
                    lblMessage.Text = "Playing song...";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = $"Error: {ex.Message}";
                }
            }
        }

        public class TokenResponse
        {
            [JsonProperty("token")]
            public string Token { get; set; }
        }

        public class Song
        {
            public string name { get; set; }
        }

        public class AudioPlayer
        {
            private static readonly HttpClient client = new HttpClient();

            public async Task PlayAudio(string blobName, string token)
            {
                var playUrl = $"http://localhost:3000/api/audio/stream/{blobName}";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                try
                {
                    var _response = await client.GetAsync(playUrl);
                    if (_response.IsSuccessStatusCode)
                    {
                        var responseBody = await _response.Content.ReadAsStringAsync();
                        var responseJson = JObject.Parse(responseBody);
                        var sasUrl = responseJson["url"].ToString();
                        byte[] audioData = await client.GetByteArrayAsync(sasUrl);
                        Play(audioData);
                        MessageBox.Show("Playing song...");
                    }
                    else
                    {
                        MessageBox.Show($"Failed to get song URL: {_response.ReasonPhrase}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            private void Play(byte[] audioData)
            {
                // Use C# libraries to play audio (e.g., NAudio)
                // This is a simple example using System.Media.SoundPlayer
                using (var ms = new MemoryStream(audioData))
                using (var player = new SoundPlayer(ms))
                {
                    player.PlaySync();
                }
            }
        }
    }
}
