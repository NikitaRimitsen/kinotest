using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;

namespace Kinoteatr_bilet
{
    class Emailotp
    {
        public void Saada_piletid()
        {
            string text = "Nikita Rimitsen \n Kinoteatr: Veterok \n Sinu ost on \n";
            /*foreach (var item in piletid)
            {
                text += "Pilet:\n" + "Rida: " + item.Rida + "Koht: " + item.Koht + "\n";
            }*/

            MailMessage message = new MailMessage();
            string email = "programmeeriminetthk@gmail.com";
            string password = "2.kuursus";
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.Credentials = new NetworkCredential(email, password);
            client.EnableSsl = true;

            client.UseDefaultCredentials = true;

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


    }
}
