namespace Games_Librery
{
    using Azure.Core;
    using Microsoft.Data.SqlClient;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Data;
    using static System.Windows.Forms.VisualStyles.VisualStyleElement;

    public partial class Form1 : Form
    {
        // Variable global para guardar el token
        private string accessToken = "";

        // Conexión a la base de datos
        string conectionDb = @"Data Source=192.168.1.5\MSSQLserver02,1433;Database=Game_Library;User Id=Lux;Password=1234567890;TrustServerCertificate=True";

        public Form1()
        {
            InitializeComponent();
            load_data();
        }


        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void load_data()
        {
            using (SqlConnection connection = new SqlConnection(conectionDb))
            {
                string consulta = "SELECT * FROM games";

                SqlDataAdapter adapter = new SqlDataAdapter(consulta, connection);
                DataTable dt = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(dt);  // Llenamos el DataTable con los datos
                    dataGridView1.DataSource = dt;  // Asignamos el DataTable como fuente de datos
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar datos: " + ex.Message);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Tomamos la primera fila seleccionada
                DataGridViewRow fila = dataGridView1.SelectedRows[0];

                // Obtenemos la URL de la columna "logo_url"
                string url = fila.Cells["logo_url"].Value?.ToString();

                if (!string.IsNullOrEmpty(url))
                {
                    try
                    {
                        pictureBoxLogo.Load(url);
                    }
                    catch
                    {
                        pictureBoxLogo.Image = null;
                    }
                }
                else
                {
                    pictureBoxLogo.Image = null;
                }
            }
            else
            {
                pictureBoxLogo.Image = null; // Si no hay fila seleccionada
            }
        }


        private void BtnApi_Click(object sender, EventArgs e)
        {
            ApiFrom apiForm = new ApiFrom();
            apiForm.ShowDialog();
        }

        //public async Task<string> ObtenerTokenAsync()
        //{
        //    string clientId = "s87stzlrar3716fqqtfyao1wtaf0b6";
        //    string clientSecret = "9ghh5i8ovl0ucnhbvxchhy0w793m22";
        //    string url = "https://id.twitch.tv/oauth2/token";

        //    using (var client = new HttpClient())
        //    {
        //        var parameters = new Dictionary<string, string>
        //        {
        //            { "client_id", clientId },
        //            { "client_secret", clientSecret },
        //            { "grant_type", "client_credentials" }
        //        };

        //        var content = new FormUrlEncodedContent(parameters);
        //        var response = await client.PostAsync(url, content);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var json = await response.Content.ReadAsStringAsync();
        //            var data = JObject.Parse(json);
        //            return data["access_token"].ToString();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Error al obtener el token: " + response.StatusCode);
        //            return null;
        //        }
        //    }
        //}
    }
}
