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
    public partial class Login : Form
    {


        TextBox login = new TextBox
        {
            Location = new System.Drawing.Point(200, 100),//Point(x,y)
            Height = 80,
            Width = 150
        };
        TextBox password = new TextBox
        {
            Location = new System.Drawing.Point(200, 160),//Point(x,y)
            Height = 80,
            Width = 150,
            UseSystemPasswordChar = true

    };
        static string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nikit\source\repos\kinotesta\kinnoooo-main\AppData\Kino_DB.mdf;Integrated Security=True";
        /*Надо менять            ↑ ↑ ↑ ↑ ↑ ↑ ↑  вот это, если ты пересел за другой комп!!!!!!!!!*/
        SqlConnection connect_to_DB = new SqlConnection(conn);

        SqlCommand command;

        public Login()
        {
            



            Label nimilabel = new Label
            {
                Location = new System.Drawing.Point(200, 30),//Point(x,y)
                Height = 50,
                Width = 170,
                Text = "Login vorm",
                Font = new Font("Oswald", 16, FontStyle.Bold)
            };

            Label loginnimi = new Label
            {
                Location = new System.Drawing.Point(50, 100),//Point(x,y)
                Height = 50,
                Width = 150,
                Text = "Login:",
                Font = new Font("Oswald", 16, FontStyle.Bold)
            };

            Label passwordnimi = new Label
            {
                Location = new System.Drawing.Point(50, 160),//Point(x,y)
                Height = 50,
                Width = 150,
                Text = "Password:",
                Font = new Font("Oswald", 16, FontStyle.Bold)
            };

            Button loginbutton = new Button
            {
                Text = "Login",
                Location = new System.Drawing.Point(100, 300),//Point(x,y)
                Height = 50,
                Width = 120,
                BackColor = Color.LightYellow
            };
            loginbutton.Click += Loginbutton_Click;

            Button lobu = new Button
            {
                Text = "Loobu",
                Location = new System.Drawing.Point(280, 300),//Point(x,y)
                Height = 50,
                Width = 120,
                BackColor = Color.LightYellow
            };
            lobu.Click += Lobu_Click;

            this.Controls.Add(nimilabel);
            this.Controls.Add(loginnimi);
            this.Controls.Add(passwordnimi);
            this.Controls.Add(login);
            this.Controls.Add(password);
            this.Controls.Add(loginbutton);
            this.Controls.Add(lobu);
            this.ClientSize = new System.Drawing.Size(500, 400);
        }

        private void Lobu_Click(object sender, EventArgs e)
        {
            Menu uus_aken = new Menu();//запускает пустую форму
            uus_aken.StartPosition = FormStartPosition.CenterScreen;
            uus_aken.Show();
            this.Hide();
        }

        private void Loginbutton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nikit\source\repos\kinotesta\kinnoooo-main\AppData\Kino_DB.mdf;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter("SELECT Count(*) FROM Login WHERE Username='" + login.Text + "' and Password ='" + password.Text +"'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                Administrator uus_aken = new Administrator();//запускает пустую форму
                uus_aken.StartPosition = FormStartPosition.CenterScreen;
                uus_aken.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Vale parool või sisselogimine");
            }

            
        }
    }
}
