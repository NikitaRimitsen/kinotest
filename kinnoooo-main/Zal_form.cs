using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kinoteatr_bilet
{
    public partial class Zal_form : Form
    {



        public Zal_form()
        {
            this.Icon = Properties.Resources.ekologicno;
            this.Text = "Veterok";

            this.ClientSize = new System.Drawing.Size(400, 350);

            Button mal_btn = new Button
            {
                Text = "Väike saal",
                Location = new System.Drawing.Point(130, 100),//Point(x,y)
                Height = 50,
                Width = 150,
                BackColor = Color.LightYellow
            };
            mal_btn.Click += Mal_btn_Click;

            Button sred_btn = new Button
            {
                Text = "Keskmine saal",
                Location = new System.Drawing.Point(130, 170),//Point(x,y)
                Height = 50,
                Width = 150,
                BackColor = Color.LightYellow
            };
            sred_btn.Click += Sred_btn_Click;

            Button bol_btn = new Button
            {
                Text = "Suur saal",
                Location = new System.Drawing.Point(130, 240),//Point(x,y)
                Height = 50,
                Width = 150,
                BackColor = Color.LightYellow
            };
            bol_btn.Click += Bol_btn_Click;

            Label lbl_zal = new Label
            {
                Text = "Valige saal",
                Size = new System.Drawing.Size(180, 50),
                Location = new System.Drawing.Point(135, 25),
                Font = new Font("Oswald", 16, FontStyle.Bold)
            };

            this.Controls.Add(lbl_zal);
            this.Controls.Add(mal_btn);
            this.Controls.Add(sred_btn);
            this.Controls.Add(bol_btn);
            this.BackColor = Color.LightSalmon;
        }

        private void Bol_btn_Click(object sender, EventArgs e)
        {
            Zal_vaata uus_aken = new Zal_vaata(9, 9);//запускает пустую форму
            uus_aken.StartPosition = FormStartPosition.CenterScreen;
            uus_aken.ShowDialog();
        }

        private void Sred_btn_Click(object sender, EventArgs e)
        {
            Zal_vaata uus_aken = new Zal_vaata(7, 7);//запускает пустую форму
            uus_aken.StartPosition = FormStartPosition.CenterScreen;
            uus_aken.ShowDialog();

        }

        private void Mal_btn_Click(object sender, EventArgs e)
        {
            Zal_vaata uus_aken = new Zal_vaata(5, 5);//запускает пустую форму
            uus_aken.StartPosition = FormStartPosition.CenterScreen;
            uus_aken.ShowDialog();
        }


        private void MyFotm_Click(object sender, EventArgs e)
        {

        }
    }
}
