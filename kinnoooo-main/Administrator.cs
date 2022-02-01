using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kinoteatr_bilet
{
    public partial class Administrator : Form
    {

        static string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\opilane\source\repos\link_TARpv21\ProverkaZnPoMat\Kino_\AppData\Kino_DB.mdf;Integrated Security=True";
        /*Надо менять            ↑ ↑ ↑ ↑ ↑ ↑ ↑  вот это, если ты пересел за другой комп!!!!!!!!!*/
        SqlConnection connect_to_DB = new SqlConnection(conn);

        SqlCommand command;
        SqlDataAdapter adapter;


        int Id_film;
        public Administrator()
        {
            this.ClientSize = new System.Drawing.Size(720, 500);


            Button naita = new Button
            {
                Text = "Naita",
                Location = new System.Drawing.Point(500, 25),//Point(x,y)
                Height = 40,
                Width = 100,
                BackColor = Color.LightYellow
            };
            naita.Click += Film_naita_Click;


            Button film_uuenda = new Button
            {
                Text = "Naita",
                Location = new System.Drawing.Point(250, 50),//Point(x,y)
                Height = 40,
                Width = 100,
                BackColor = Color.LightYellow
            };
            film_uuenda.Click += Film_uuenda_Click;

            this.Controls.Add(naita);
            /*TextBox nimi = new TextBox
            {
                Location = new System.Drawing.Point(50, 40),//Point(x,y)
                Height = 80,
                Width = 150,
            };

            TextBox film = new TextBox
            {
                Location = new System.Drawing.Point(50, 80),//Point(x,y)
                Height = 80,
                Width = 150,
            };
            this.Controls.Add(nimi);
            this.Controls.Add(film);*/
        }

        TextBox film_txt, aasta_txt, poster_txt;
        PictureBox poster;
        DataGridView dataGridView;
        private void Film_uuenda_Click(object sender, EventArgs e)
        {
            if (film_txt.Text != "" && aasta_txt.Text != "" && poster_txt.Text != "" && poster.Image != null)
            {
                connect_to_DB.Open();
                command = new SqlCommand("UPDATE Film  SET Nimi=@nimi,Aasta=@aasta, Pilt=@pilt WHERE Id_film=@id", connect_to_DB);
                
                command.Parameters.AddWithValue("@id", Id_film);
                command.Parameters.AddWithValue("@nimi", film_txt.Text);
                command.Parameters.AddWithValue("@aasta", aasta_txt.Text);
                command.Parameters.AddWithValue("@pilt", poster_txt.Text);
                //string file_pilt = poster_txt.Text + ".jpg";
                //command.Parameters.AddWithValue("@pilt", file_pilt);
                command.ExecuteNonQuery();
                connect_to_DB.Close();
                //ClearData();
                //Data();
                MessageBox.Show("Andmed uuendatud");
            }
            else
            {
                MessageBox.Show("Viga");
            }
            
        }

        private void Film_naita_Click(object sender, EventArgs e)
        {
            film_txt = new TextBox
            {
                Location = new System.Drawing.Point(450, 75),
            };
            aasta_txt = new TextBox
            {
                Location = new System.Drawing.Point(450, 100),
            };
            poster_txt = new TextBox
            {
                Location = new System.Drawing.Point(450, 125),
            };
            poster = new PictureBox
            {
                Size = new System.Drawing.Size(180, 250),
                Location=new System.Drawing.Point(450,150),
                SizeMode = PictureBoxSizeMode.StretchImage
            };


            /*connect_to_DB.Open();
            DataTable tabel = new DataTable();
            dataGridView = new DataGridView();
            dataGridView.RowHeaderMouseClick += DataGridView_RowHeaderMouseClick;
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Id_film,Nimi,Aasta,Pilt FROM [dbo].[Film]", connect_to_DB);
            adapter.Fill(tabel);
            dataGridView.DataSource = tabel;
            dataGridView.Location = new System.Drawing.Point(10, 75);
            dataGridView.Size = new System.Drawing.Size(400, 200);*/
            Data();
            this.Controls.Add(dataGridView);
            this.Controls.Add(film_txt);
            this.Controls.Add(aasta_txt);
            this.Controls.Add(poster_txt);
            this.Controls.Add(poster);
            //connect_to_DB.Close();
        }
        public void Data()
        {
            connect_to_DB.Open();
            DataTable tabel = new DataTable();
            dataGridView = new DataGridView();
            dataGridView.RowHeaderMouseClick += DataGridView_RowHeaderMouseClick;
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Id_film,Nimi,Aasta,Pilt FROM [dbo].[Film]", connect_to_DB);
            adapter.Fill(tabel);
            dataGridView.DataSource = tabel;
            dataGridView.Location = new System.Drawing.Point(10, 75);
            dataGridView.Size = new System.Drawing.Size(400, 200);
            connect_to_DB.Close();
        }
        private void DataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Id_film = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
            film_txt.Text = dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            aasta_txt.Text = dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
            poster_txt.Text = dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
            poster.Image = Image.FromFile(@"C:..\..\Posterid\" + dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString());
            //string v = dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
            //comboBox1.SelectedIndex = Int32.Parse(v) - 1;
        }
    }
}
