using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BLL.Mail
{
    public class Mail
    {
        private static MailAddress from = new MailAddress("testado.net@gmail.com");
        private static MailAddress to;
        private static SmtpClient smtp = new SmtpClient("smtp.gmail.com");
        public Mail(string email)
        {
            to = new MailAddress(email);
            smtp.Credentials = new NetworkCredential("testado.net@gmail.com", "lkaeqypbqdlktosj");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        }


        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }




        public void SendMassage()
        {
            
            MailMessage m = new MailMessage(from, to)
            {
                Subject = this.Subject,
                Body = this.Body,
                IsBodyHtml = this.IsBodyHtml
            };
            smtp.Send(m);

        }

        public async Task SendMassageAsync()
        {
            MailMessage m = new MailMessage(from, to)
            {
                Subject = this.Subject,
                Body = this.Body,
                IsBodyHtml = this.IsBodyHtml
            };
           await smtp.SendMailAsync(m);
        }

    }
}