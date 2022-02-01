using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kinoteatr_bilet
{
    public partial class Afisha : Form
    {

        static string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\opilane\source\repos\link_TARpv21\ProverkaZnPoMat\Kinoteatr_bilet\AppData\Kino_DB.mdf;Integrated Security=True";
        SqlConnection connect_to_DB = new SqlConnection(conn);

        SqlCommand command;
        SqlDataAdapter adapter;

        PictureBox kartinka1;
        PictureBox kartinka2;
        PictureBox kartinka3;
        public Afisha()
        {
            this.Icon = Properties.Resources.ekologicno;
            this.Text = "Veterok";

            this.ClientSize = new System.Drawing.Size(720, 500);

            kartinka1 = new PictureBox();//создали PictureBox
            kartinka1.Size = new Size(220, 400);
            kartinka1.Location = new Point(500, 20);
            kartinka1.SizeMode = PictureBoxSizeMode.StretchImage;
            kartinka1.ImageLocation = (@"..\..\image\avatar.jpg");
            kartinka1.Click += kartinka1_Click;

            kartinka2 = new PictureBox();//создали PictureBox
            kartinka2.Size = new Size(220, 400);
            kartinka2.Location = new Point(250, 20);
            kartinka2.SizeMode = PictureBoxSizeMode.StretchImage;
            kartinka2.ImageLocation = (@"..\..\image\mortal.jpg");
            kartinka2.Click += kartinka2_Click;

            kartinka3 = new PictureBox();//создали PictureBox
            kartinka3.Size = new Size(220, 400);
            kartinka3.Location = new Point(1, 20);
            kartinka3.SizeMode = PictureBoxSizeMode.StretchImage;
            kartinka3.ImageLocation = (@"..\..\image\dzeltemene.jpg");
            kartinka3.Click += kartinka3_Click;


            this.BackColor = Color.LightSalmon;
            this.Controls.Add(kartinka1);
            this.Controls.Add(kartinka2);
            this.Controls.Add(kartinka3);

        }

        private void kartinka1_Click(object sender, EventArgs e)
        {
            Zal_form uus_aken = new Zal_form();//запускает пустую форму
            uus_aken.StartPosition = FormStartPosition.CenterScreen;
            uus_aken.Show();
            string avatar = "Avatar";
            using (StreamWriter srb = new StreamWriter(@"..\..\zapisfilma\Film.txt", true))
            {
                srb.WriteLine(avatar);
            }
            this.Hide();

        }

        private void kartinka2_Click(object sender, EventArgs e)
        {
            Zal_form uus_aken = new Zal_form();//запускает пустую форму
            uus_aken.StartPosition = FormStartPosition.CenterScreen;
            uus_aken.Show();
            string mc = "Mortal Kombat";
            using (StreamWriter srb = new StreamWriter(@"..\..\zapisfilma\Film.txt", true))
            {
                srb.WriteLine(mc);
            }
            this.Hide();
        }

        private void kartinka3_Click(object sender, EventArgs e)
        {
            Zal_form uus_aken = new Zal_form();//запускает пустую форму
            uus_aken.StartPosition = FormStartPosition.CenterScreen;
            uus_aken.Show();
            string dz = "Dzeltemene udachi";
            using (StreamWriter srb = new StreamWriter(@"..\..\zapisfilma\Film.txt", true))
            {
                srb.WriteLine(dz);
            }
            this.Hide();
        }
    }
}
