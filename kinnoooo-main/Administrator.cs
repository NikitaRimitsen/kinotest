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

        static string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nikit\source\repos\kinnoooo\AppData\Kino_DB.mdf;Integrated Security=True";
        /*Надо менять            ↑ ↑ ↑ ↑ ↑ ↑ ↑  вот это, если ты пересел за другой комп!!!!!!!!!*/
        SqlConnection connect_to_DB = new SqlConnection(conn);

        SqlCommand command;
        SqlDataAdapter adapter;

        Button film_uuenda;
        Button film_kustuta;
        Button film_insert;
        int Id_film;
        public Administrator()
        {
            this.ClientSize = new System.Drawing.Size(720, 500);


            Button naita = new Button
            {
                Text = "Naita",
                Location = new System.Drawing.Point(40, 25),//Point(x,y)
                Height = 40,
                Width = 100,
                BackColor = Color.LightYellow
            };
            naita.Click += Film_naita_Click;


            Button piletinaita = new Button
            {
                Text = "PiletiNaita",
                Location = new System.Drawing.Point(160, 25),//Point(x,y)
                Height = 40,
                Width = 100,
                BackColor = Color.LightYellow
            };
            piletinaita.Click += Piletinaita_Click;

            film_uuenda = new Button
            {
                Location = new System.Drawing.Point(600, 75),
                Size = new System.Drawing.Size(80, 25),
                Text = "Uuendamine",
                Visible = false
            };
            film_uuenda.Click += Film_uuenda_Click;

            film_kustuta = new Button
            {
                Location = new System.Drawing.Point(600, 100),
                Size = new System.Drawing.Size(80, 25),
                Text = "Kustuta",
                Visible = false
            };
            film_kustuta.Click += Film_kustuta_Click;


            film_insert = new Button
            {
                Location = new System.Drawing.Point(600, 125),
                Size = new System.Drawing.Size(80, 25),
                Text = "Lisa uus",
                Visible = false
            };
            film_insert.Click += Film_insert_Click;


            this.Controls.Add(piletinaita);

            this.Controls.Add(film_insert);
            this.Controls.Add(film_uuenda);
            this.Controls.Add(film_kustuta);
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

        private void Piletinaita_Click(object sender, EventArgs e)
        {
            connect_to_DB.Open();
            DataTable tabel_p = new DataTable();
            DataGridView dataGridView_p = new DataGridView();
            DataSet dataset_p = new DataSet();
            SqlDataAdapter adapter_p = new SqlDataAdapter("SELECT Rida,Koht,Film FROM [dbo].[Piletid]; SELECT Nimi FROM [dbo].[Film]", connect_to_DB);

            //adapter_p.TableMappings.Add("Piletid", "Rida");
            //adapter_p.TableMappings.Add("Filmid", "Filmi_nimetus");
            //adapter_p.Fill(dataset_p);
            adapter_p.Fill(tabel_p);
            dataGridView_p.DataSource = tabel_p;
            dataGridView_p.Location = new System.Drawing.Point(10, 75);
            dataGridView_p.Size = new System.Drawing.Size(400, 200);


            SqlDataAdapter adapter_f = new SqlDataAdapter("SELECT Nimi FROM [dbo].[Film]", connect_to_DB);
            DataTable tabel_f = new DataTable();
            DataSet dataset_f = new DataSet();
            adapter_f.Fill(tabel_f);
            /*fkc = new ForeignKeyConstraint(tabel_f.Columns["Id"], tabel_p.Columns["Film_Id"]);
            tabel_p.Constraints.Add(fkc);*/
            //poster.Image = Image.FromFile("../../Posterid/ezik.jpg");

            DataGridViewComboBoxCell cbc = new DataGridViewComboBoxCell();
            ComboBox com_f = new ComboBox();
            foreach (DataRow row in tabel_f.Rows)
            {
                com_f.Items.Add(row["Nimi"]);
                cbc.Items.Add(row["Nimi"]);
            }
            cbc.Value = com_f;
            connect_to_DB.Close();
            this.Controls.Add(dataGridView_p);
            this.Controls.Add(com_f);
        }

        private void Film_insert_Click(object sender, EventArgs e)
        {
            if (film_txt.Text != "" && aasta_txt.Text != "" && poster_txt.Text != "")
            {
                try
                {
                    command = new SqlCommand("INSERT INTO Film(Nimi,Aasta,Pilt) VALUES(@nimi,@aasta,@pilt)", connect_to_DB);
                    connect_to_DB.Open();
                    command.Parameters.AddWithValue("@nimi", film_txt.Text);
                    command.Parameters.AddWithValue("@aasta", aasta_txt.Text);
                    string file_pilt = film_txt.Text + ".jpg";
                    command.Parameters.AddWithValue("@pilt", file_pilt);

                    command.ExecuteNonQuery();
                    connect_to_DB.Close();
                    ClearData();
                    DisplayData();
                    MessageBox.Show("Andmed on lisatud");
                }
                catch (Exception)
                {

                    MessageBox.Show("Viga lisamisega");
                }
            }
            else
            {
                MessageBox.Show("Viga else");
            }
        }

        private void Film_kustuta_Click(object sender, EventArgs e)
        {
            if (Id_film != 0)
            {
                command = new SqlCommand("DELETE Film WHERE Id_film=@id", connect_to_DB);
                connect_to_DB.Open();
                command.Parameters.AddWithValue("@id", Id_film);
                command.ExecuteNonQuery();
                connect_to_DB.Close();
                ClearData();
                DisplayData();
                MessageBox.Show("Andmed on kustutatud");
            }
            else
            {
                MessageBox.Show("Viga kustutamisega");
            }
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
                ClearData();
                Data();
                MessageBox.Show("Andmed uuendatud");

            }
            else
            {
                MessageBox.Show("Viga");
            }

        }
        int chet = 0;
        private void Film_naita_Click(object sender, EventArgs e)
        {
            if (chet==0)
            {
                film_insert.Visible = true;
                film_uuenda.Visible = true;
                film_kustuta.Visible = true;
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
                    Location = new System.Drawing.Point(450, 150),
                    SizeMode = PictureBoxSizeMode.StretchImage
                };
                Data();
                chet++;
            }
            else if (chet == 1)
            {
                film_insert.Visible = false;
                film_uuenda.Visible = false;
                film_kustuta.Visible = false;

                film_txt.Visible = false;
                aasta_txt.Visible = false;
                poster_txt.Visible = false;
                poster.Visible = false;


                chet = 0;
            }


            /*connect_to_DB.Open();
            DataTable tabel = new DataTable();
            dataGridView = new DataGridView();
            dataGridView.RowHeaderMouseClick += DataGridView_RowHeaderMouseClick;
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Id_film,Nimi,Aasta,Pilt FROM [dbo].[Film]", connect_to_DB);
            adapter.Fill(tabel);
            dataGridView.DataSource = tabel;
            dataGridView.Location = new System.Drawing.Point(10, 75);
            dataGridView.Size = new System.Drawing.Size(400, 200);*/
            
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
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM [dbo].[Film]", connect_to_DB);
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

        private void DisplayData()
        {
            connect_to_DB.Open();
            DataTable tabel = new DataTable();
            adapter = new SqlDataAdapter("SELECT * FROM Film", connect_to_DB);//, Kategooria WHERE Toodetable.Kategooria_Id=Kategooria.Id
            adapter.Fill(tabel);
            dataGridView.DataSource = tabel;
            connect_to_DB.Close();

        }

        private void ClearData()
        {
            //Id = 0;
            film_txt.Text = "";
            aasta_txt.Text = "";
            poster_txt.Text = "";
            //save.FileName = "";
            poster.Image = Image.FromFile("../../Posterid/ezik.jpg");

        }



    }
}
