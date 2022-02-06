using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Policy;
using System.Data.SqlClient;
using System.Data;

namespace Kinoteatr_bilet
{

    public partial class Zal_vaata : Form
    {

        Label message = new Label();
        Button[] btn = new Button[4];//создали массив(список) btn - название массива
        string[] texts = new string[4];//создали массив(список) texts - название массива
        TableLayoutPanel tlp = new TableLayoutPanel();
        Button btn_tabel;
        static List<Pilet> piletid;
        int k, r;
        static string[] read_kohad;
        static string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nikit\source\repos\kinotest\kinnoooo-main\AppData\Kino_DB.mdf;Integrated Security=True";
        /*Надо менять            ↑ ↑ ↑ ↑ ↑ ↑ ↑  вот это, если ты пересел за другой комп!!!!!!!!!*/
        SqlConnection connect_to_DB = new SqlConnection(conn);

        SqlCommand command;
        SqlDataAdapter adapter;

        public Zal_vaata()//пустая форма
        { }


        public string[] Ostu_piletid()
        {
            try
            {
                /*StreamReader f = new StreamReader(@"..\..\Piletid\piletid.txt");
                read_kohad = f.ReadToEnd().Split(';');
                //int kogus = read_kohad.Length;
                f.Close();*/
                connect_to_DB.Open();
                adapter = new SqlDataAdapter("SELECT * FROM [dbo].[Piletid]", connect_to_DB);
                DataTable tabel = new DataTable();
                adapter.Fill(tabel);
                read_kohad = new string[tabel.Rows.Count];
                var index = 0;
                foreach (DataRow row in tabel.Rows)
                {
                    var rida = row["Rida"];
                    var koht = row["Koht"];
                    read_kohad[index++] = $"{rida}{koht}";
                }
                connect_to_DB.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }
            return read_kohad;

        }






        public Zal_vaata(int read, int kohad)
        {
            this.tlp.ColumnCount = kohad;
            this.tlp.RowCount = read;
            this.tlp.ColumnStyles.Clear();
            this.tlp.RowStyles.Clear();
            int i, j;
            read_kohad = Ostu_piletid();
            piletid = new List<Pilet> { };


            for (i = 0; i < read; i++)
            {
                this.tlp.RowStyles.Add(new RowStyle(SizeType.Percent/*, 100 / read*/));
                this.tlp.RowStyles[i].Height = 100 / read;
            }

            for (j = 0; j < kohad; j++)
            {
                this.tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent/*, 100 / kohad*/));
                this.tlp.ColumnStyles[j].Width = 100 / kohad;
            }

            this.Size = new System.Drawing.Size(kohad * 100, read * 100);
            for (int r = 0; r < read; r++)
            {
                for (int k = 0; k < kohad; k++)
                {
                    //Button btn_tabel = Uusnupp((sender, e) => Pileti_zapis(sender, e));
                    btn_tabel = new Button
                    {
                        Text = string.Format("rida {0},koht {1}", r + 1, k + 1),
                        Name = string.Format("{1}{0}", k + 1, r + 1),
                        Dock = DockStyle.Fill,
                        BackColor = Color.Green
                    };
                    
                     foreach (var item in read_kohad)
                     {

                         if (item.ToString() == btn_tabel.Name)
                         {
                             //добавить чтобы не мог купить ещё
                             btn_tabel.BackColor = Color.Red;
                             btn_tabel.Enabled = false;

                            //MessageBox.Show(item, btn_tabel.BackColor.ToString());
                        }
                    }
                    btn_tabel.Click += new EventHandler(Pileti_zapis);
                    this.tlp.Controls.Add(btn_tabel, k, r);

                  }

            }
            //делаем чтобы кнопки были нормальные
            
            //btn_w = (int)(100 / kohad);
            //btn_h = (int)(100 / read);
            this.tlp.Dock = DockStyle.Fill;
            this.Controls.Add(tlp);
        }

        string pocta = "";
        private void Saada_piletid(List<Pilet> piletid)
        {
            pocta = Interaction.InputBox("Email", "Email");

            if (pocta !="")
            {
                connect_to_DB.Open();
                var filmivaata = File.ReadLines(@"..\..\zapisfilma\Film.txt").Last();

                string text = "Kinoteatr: „Veterok“\nFilmi on: " + filmivaata;
                foreach (var item in piletid)
                {

                    text += "\n" + "Rida: " + item.Rida + " Koht: " + item.Koht;
                    command = new SqlCommand("INSERT INTO Piletid(Rida,Koht,Film) VALUES(@rida,@koht,@film)", connect_to_DB);
                    command.Parameters.AddWithValue("@rida", item.Rida);
                    command.Parameters.AddWithValue("@koht", item.Koht);
                    command.Parameters.AddWithValue("@film", 1);
                    command.ExecuteNonQuery();
                }
                connect_to_DB.Close();

                text += "\n\nTäname, et valisite meid!\nHead vaatamist!\nRimitsen Nikita";
                MailMessage message = new MailMessage();
                if (pocta.EndsWith("@gmail.com") || pocta.EndsWith("@mail.ru") || pocta.EndsWith("@bk.ru") || pocta.EndsWith("@list.ru") || pocta.EndsWith("@tthk.ee"))
                {
                    message.To.Add(new MailAddress(pocta));
                    message.From = new MailAddress(pocta);
                    message.Subject = "Ostetud piletid";
                    message.Body = text;
                    string email = "programmeeriminetthk@gmail.com";
                    string password = "2.kuursus tarpv20";
                    SmtpClient client = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential(email, password),
                        EnableSsl = true,
                    };
                    try
                    {
                        client.Send(message);
                        Environment.Exit(0);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());

                    }
                    
                }
                else
                {
                    if (MessageBox.Show("E-post on valesti sisestatud.\nKas soovite korrata?", "Viga", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        Saada_piletid(piletid);
                    }
                    else
                    {
                        Environment.Exit(0);
                    }

                }
            }
            else
            {
                if (MessageBox.Show("E-post on valesti sisestatud.\nKas soovite korrata?", "Viga", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    Saada_piletid(piletid);
                }
                else
                    {
                        Environment.Exit(0);
                    }
                
            }

            

        }

        private void Pileti_zapis(object sender, EventArgs e)
        {
            Button btn_click = (Button)sender;
            btn_click.BackColor = Color.Yellow;
            MessageBox.Show(btn_click.Name.ToString());
            var rida = int.Parse(btn_click.Name[0].ToString());
            var koht = int.Parse(btn_click.Name[1].ToString());

            var vas = MessageBox.Show("Sinu pilet on: Rida: " + rida+ " Koht: " +koht, "Kas ostad?", MessageBoxButtons.YesNo);
            if (vas == DialogResult.Yes)
            {
                btn_click.BackColor = Color.Red;
                btn_click.Enabled = false;
                try
                {
                    Pilet pilet = new Pilet(rida, koht);
                    piletid.Add(pilet);
                    StreamWriter ost = new StreamWriter(@"..\..\Piletid\piletid.txt", true);
                    ost.Write(btn_click.Name.ToString() + ';');
                    ost.Close();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (vas == DialogResult.No)
            {
                btn_click.BackColor = Color.Green;
            };
            if (piletid.Count() > 0)
            {
                if (MessageBox.Show("Sul on ostetud: " + piletid.Count() + " piletid", "Kas tahad saada neid e-mailile?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    Saada_piletid(piletid);
                }
            }
            
        }






    }
}
