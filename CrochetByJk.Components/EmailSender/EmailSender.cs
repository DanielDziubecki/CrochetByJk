
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CrochetByJk.Components.EmailSender
{
    public class EmailSender : IEmailSender
    {
        private readonly IMailMessageFactory mailMessageFactory;

        public EmailSender(IMailMessageFactory mailMessageFactory)
        {
            this.mailMessageFactory = mailMessageFactory;
        }

        public void Send(IEmailMessage emailMessage)
        {
            var messages = mailMessageFactory.GetMessages(emailMessage);
            using (var smtpClient = GetSmtpClient())
            {
                foreach (var mailMessage in messages)
                {
                    smtpClient.Send(mailMessage);
                }
            }
        }

        public async Task SendAsync(IEmailMessage emailMessage)
        {
            var messages = mailMessageFactory.GetMessages(emailMessage);
            using (var smtpClient = GetSmtpClient())
            {
                foreach (var mailMessage in messages)
                {
                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
        }

        private SmtpClient GetSmtpClient()
        {
            return new SmtpClient
            {
                Host = "smtp.webio.pl",
                EnableSsl = true,
                Credentials = new NetworkCredential("kontakt@crochetbyjk.pl", Environment.GetEnvironmentVariable("smtp_pass")
            };
        }
    }
}
