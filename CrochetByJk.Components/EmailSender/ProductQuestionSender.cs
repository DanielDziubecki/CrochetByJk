using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CrochetByJk.Components.EmailSender
{
    public class ProductQuestionSender : IEmailSender
    {
        public void Send(IEmailMessage emailMessage)
        {
            var message = GetMessage(emailMessage);
            using (var smtpClient = GetSmtpClient())
            {
                smtpClient.Send(message);
            }
        }

        public async Task SendAsync(IEmailMessage emailMessage)
        {
            var message = GetMessage(emailMessage);
            using (var smtpClient = GetSmtpClient())
            {
                await smtpClient.SendMailAsync(message);
            }
        }

        private MailMessage GetMessage(IEmailMessage emailMessage)
        {
            var message = new MailMessage(emailMessage.From, emailMessage.To, emailMessage.Subject, emailMessage.Body);
            message.To.Add(emailMessage.To);
            message.Subject = emailMessage.Subject;
            message.Body = emailMessage.Body;

            return message;
        }

        private SmtpClient GetSmtpClient()
        {
            return new SmtpClient
            {
                Host = "smtp.webio.pl",
                EnableSsl = true,
                Credentials = new NetworkCredential("kontakt@crochetbyjk.pl", "Daniel!991")
            };
        }
    }
}