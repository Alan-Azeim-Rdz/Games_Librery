using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Games_Librery
{
    public partial class ApiFrom : Form
    {

        private string clientId = "s87stzlrar3716fqqtfyao1wtaf0b6";         
        private string accessToken;     
        private string clientSecret = "t1klkmgshqsv1ct8mk9x1jm5heoer0";
        private DateTime tokenExpiration;

        public ApiFrom()
        {
            InitializeComponent();
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                MessageBox.Show("Ingresa un nombre para buscar.");
                return;
            }

            try
            {
                // Aquí llamas a ObtenerTokenAsync y asignas el token
                accessToken = await ObtenerTokenAsync();

                // Ya con el token válido haces la búsqueda
                var juegos = await BuscarJuegosAsync(txtBuscar.Text);

                lstJuegos.Items.Clear();
                picPortada.Image = null;

                foreach (var juego in juegos)
                {
                    var item = new ListViewItem(juego.name);
                    item.SubItems.Add(juego.release_date);
                    item.Tag = juego;
                    lstJuegos.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }


        }

        private async Task<string> ObtenerTokenAsync()
        {
            // Si el token es válido y no expiró, regresarlo
            if (!string.IsNullOrEmpty(accessToken) && DateTime.Now < tokenExpiration)
            {
                return accessToken;
            }

            using (var client = new HttpClient())
            {
                var url = $"https://id.twitch.tv/oauth2/token?client_id={clientId}&client_secret={clientSecret}&grant_type=client_credentials";

                var response = await client.PostAsync(url, null);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("No se pudo obtener el token de acceso.");
                }

                var json = await response.Content.ReadAsStringAsync();
                var data = JObject.Parse(json);

                accessToken = data["access_token"].ToString();

                // Guardamos la expiración sumando los segundos que dura el token
                int expiresIn = data["expires_in"].ToObject<int>();
                tokenExpiration = DateTime.Now.AddSeconds(expiresIn - 60); // -60 para renovar 1 minuto antes

                return accessToken;
            }
        }

        public async Task<List<Game>> BuscarJuegosAsync(string nombre)
        {
            var juegos = new List<Game>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Client-ID", clientId);
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                string query = $@"fields name, first_release_date, genres.name, platforms.name, involved_companies.company.name, themes.name, cover.url; search ""{nombre}"";limit 10;";

                var content = new StringContent(query, Encoding.UTF8, "text/plain");
                var response = await client.PostAsync("https://api.igdb.com/v4/games", content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error en la API IGDB: {response.StatusCode}");
                }

                string json = await response.Content.ReadAsStringAsync();
                var data = JArray.Parse(json);

                foreach (var item in data)
                {
                    juegos.Add(new Game
                    {
                        name = item["name"]?.ToString(),
                        release_date = item["first_release_date"] != null
                            ? UnixTimeStampToDateTime((long)item["first_release_date"]).ToShortDateString()
                            : "Desconocida",
                        cover_url = item["cover"]?["url"]?.ToString()?.Replace("t_thumb", "t_cover_big")
                    });
                }
            }

            return juegos;
        }

        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        private void lstJuegos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstJuegos.SelectedItems.Count > 0)
            {
                var juego = (Game)lstJuegos.SelectedItems[0].Tag;
                if (!string.IsNullOrEmpty(juego.cover_url))
                {
                    try
                    {
                        picPortada.LoadAsync("https:" + juego.cover_url);
                    }
                    catch
                    {
                        picPortada.Image = null;
                    }
                }
                else
                {
                    picPortada.Image = null;
                }
            }
        }

        public class Game
        {
            public string name { get; set; }
            public string release_date { get; set; }
            public string cover_url { get; set; }
        }

    }
}
