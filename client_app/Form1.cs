using System;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace client_app
{
    public partial class Form1 : Form
    {
        public class TokenResponse
        {
            [JsonProperty("token")]
            public string Token { get; set; }
        }

        private HttpClient client = new HttpClient();
        public string Token { get; private set; }

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
                    Token = tokenData.Token;
                    MessageBox.Show("Login successful!");

                    this.Hide();
                    MusicPlayerForm musicPlayerForm = new MusicPlayerForm(Token);
                    musicPlayerForm.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Login failed. Invalid credentials.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnOpenRegister_Click(object sender, EventArgs e)
        {
            Form2 registerForm = new Form2();
            registerForm.ShowDialog();
        }
    }
}
