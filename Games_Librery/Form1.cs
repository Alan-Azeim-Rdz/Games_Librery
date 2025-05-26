namespace Games_Librery
{
    using Microsoft.Data.SqlClient;
    using System;
    using System.Data;
    using static System.Windows.Forms.VisualStyles.VisualStyleElement;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            load_data();
        }
        //conection to database Games_Library
        string conectionDb = @"Data Source=192.168.1.5\MSSQLserver02,1433;Database=Game_Library;User Id=Lux;Password=1234567890;TrustServerCertificate=True";


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
                        pictureBoxLogo.Load(url); // Carga la imagen desde la URL
                    }
                    catch
                    {
                        pictureBoxLogo.Image = null; // O carga una imagen por defecto si quieres
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

    }
}
