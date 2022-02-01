using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kinoteatr_bilet
{
    class Menu: System.Windows.Forms.Form
    {
        PictureBox afisha;


        public Menu() 
        {

            MainMenu menu = new MainMenu();
            MenuItem menuFile = new MenuItem("Seaded");
            menuFile.MenuItems.Add("Admin", new EventHandler(menuAdmin)).Shortcut = Shortcut.CtrlS;
            menu.MenuItems.Add(menuFile);

            this.Menu = menu;

            this.Icon = Properties.Resources.ekologicno;
            this.Text = "Veterok";
            Button Kinozal_btn = new Button
            {
                Text = "Osta pilet",
                Location = new System.Drawing.Point(220, 100),//Point(x,y)
                Height = 50,
                Width = 120,
                BackColor = Color.LightYellow
            };
            Kinozal_btn.Click += Kinozal_btn_Click;

            afisha = new PictureBox();//создали PictureBox
            afisha.Size = new Size(220, 400);
            afisha.Location = new Point(500, 20);
            afisha.SizeMode = PictureBoxSizeMode.StretchImage;
            afisha.ImageLocation = (@"..\..\image\avatar.jpg");
            afisha.DoubleClick += Afisha_DoubleClick;

            Button Listat_btn = new Button
            {
                Text = "=>",
                Location = new System.Drawing.Point(580, 425),//Point(x,y)
                Height = 30,
                Width = 60,
                BackColor = Color.LightYellow
            };
            Button Info_btn = new Button
            {
                Text = "Info",
                Location = new System.Drawing.Point(220, 170),//Point(x,y)
                Height = 50,
                Width = 120,
                BackColor = Color.LightYellow
            };
            Info_btn.Click += Info_btn_Click;

            Button Pravil_btn = new Button
            {
                Text = "Reegel",
                Location = new System.Drawing.Point(220, 240),//Point(x,y)
                Height = 50,
                Width = 120,
                BackColor = Color.LightYellow
            };
            Pravil_btn.Click += Pravil_btn_Click;



            Label lbl = new Label
            {
                Text = "Kinoteatr „Veterok“",
                Size = new System.Drawing.Size(250, 60),
                Location = new System.Drawing.Point(180, 25),
                Font = new Font("Oswald", 16, FontStyle.Bold)

            };


            this.Controls.Add(Kinozal_btn);
            this.Controls.Add(Info_btn);
            this.Controls.Add(Pravil_btn);
            this.Controls.Add(lbl);

            this.BackColor = Color.LightSalmon;


            Listat_btn.Click += Listat_btn_Click;
            this.Controls.Add(Listat_btn);
            this.Controls.Add(afisha);
            this.Controls.Add(Kinozal_btn);
            this.Height = 600;//свойство высота формы
            this.Width = 800;
        }


        int scetcikafi = 0;
        private void Listat_btn_Click(object sender, EventArgs e)
        {
            scetcikafi++; //тут я увеличиваю значения счетчика на 1
            if (scetcikafi == 1)
            {

                afisha.ImageLocation = (@"..\..\image\mortal.jpg");

            }
            else if (scetcikafi == 2)
            {

                afisha.ImageLocation = (@"..\..\image\dovod.jpg");
            }
            else if (scetcikafi == 3)
            {
                afisha.ImageLocation = (@"..\..\image\dzeltemene.jpg");
            }
            else if (scetcikafi == 4)
            {
                afisha.ImageLocation = (@"..\..\image\ligamonstrov.jpg");
            }
            else if (scetcikafi == 5)
            {

                scetcikafi = 0; //сбрасывает счетччик, что бы начать все заново
                afisha.ImageLocation = (@"..\..\image\avatar.jpg");
            }
        }

        private void Pravil_btn_Click(object sender, EventArgs e)
        {
            var info = File.ReadAllText(@"..\..\texte\Pravila_zal.txt");
            var information = MessageBox.Show(info, "Info");
        }

        private void Info_btn_Click(object sender, EventArgs e)
        {
            var info = File.ReadAllText(@"..\..\texte\Info_text_zal.txt");
            var information = MessageBox.Show(info, "Info");
        }
        public static void Count(object obj)
        {
            int x = (int)obj;
            for (int i = 1; i < 9; i++, x++)
            {
                Console.WriteLine($"{x * i}");
            }
        }


        private void Kinozal_btn_Click(object sender, EventArgs e)
        {
            Afisha uus_aken = new Afisha();//запускает пустую форму
            uus_aken.StartPosition = FormStartPosition.CenterScreen;
            uus_aken.Show();
            this.Hide();
        }

        private void menuAdmin(object sender, EventArgs e)
        {
            Administrator uus_okno = new Administrator();//запускает пустую форму
            uus_okno.StartPosition = FormStartPosition.CenterScreen;
            uus_okno.Show();
            this.Hide();
        }

        private void Afisha_DoubleClick(object sender, EventArgs e)
        {
            /*scetcikafi++; //тут я увеличиваю значения счетчика на 1
            if (scetcikafi == 1)
            {

                afisha.ImageLocation = (@"..\..\image\avatar.jpg");

            }
            else if (scetcikafi == 2)
            {

                afisha.ImageLocation = (@"..\..\image\dovod.jpg");
            }
            else if (scetcikafi == 3)
            {
                afisha.ImageLocation = (@"..\..\image\dzeltemene.jpg");
            }
            else if (scetcikafi == 4)
            {

                scetcikafi = 0; //сбрасывает счетччик, что бы начать все заново
                afisha.ImageLocation = (@"..\..\image\mortal.jpg");
            }*/


        }
        /*private void Afisha_DoubleClick(object sender, EventArgs e)
        {
            Button btn_click = (Button)sender;
            btn_Click.BackColor = Color.Yellow;
            string[] rida_koht = btn_tabel.Name.Split('_');
            pilet = new Pilet(int.Parse(rida_koht[1]), int.Parse(rida_koht[0]));
            if (MessageBox.Show("Sinu pilet on: Rida" + (rida_koht[1]) + " Koht: " + (rida_koht[0]), "Kas ostad?", MessageBox.Yes / No) == DialogResult.Yes)
            {
                btn_click.BackColor = Color.Red;
            }
            else
            {
                btn_click.BackColor = Color.Green;
            }
        }*/
    }
}
