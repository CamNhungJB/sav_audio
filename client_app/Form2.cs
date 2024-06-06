using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client_app
{
    public partial class Form2 : Form
    {
        private static readonly HttpClient client = new HttpClient();

        public Form2()
        {
            InitializeComponent();
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            var username = txtUsername.Text;
            var password = txtPassword.Text;
            var email = txtEmail.Text;
            var registerUrl = "http://localhost:3000/api/auth/register";

            var registerData = new
            {
                username = username,
                password = password,
                email = email
            };

            var content = new StringContent(JsonConvert.SerializeObject(registerData), Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(registerUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    lblMessage.Text = "Registration successful!";
                }
                else
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    lblMessage.Text = $"Registration failed: {responseBody}";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error: {ex.Message}";
            }
        }
    }
}
