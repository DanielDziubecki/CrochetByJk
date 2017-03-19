using System.Collections.Concurrent;
using System.Net.Mail;
using System.Threading.Tasks;
using CrochetByJk.Components.Exceptions;

namespace CrochetByJk.Components.EmailSender
{
    public class MailMessageFactory : IMailMessageFactory
    {
        public MailMessage[] GetMessages(IEmailMessage emailMessage)
        {
            if (emailMessage is ProductQuestionMessage)
                return GetProductQuestionTypeofMessages(emailMessage);

            if (emailMessage is NewsletterMessage)
                return GetNewsletterTypeofMessages(emailMessage);

            throw new UnknownEmailMessageType(emailMessage.GetType());
        }

        private MailMessage[] GetNewsletterTypeofMessages(IEmailMessage emailMessage)
        {
            var message = (NewsletterMessage) emailMessage;
            var mailMessages = new ConcurrentBag<MailMessage>();
            var body = "<html><body><div style=\"text-align=center;\"><p>Witaj, dodałam nowy produkt. Być może Cię zainteresuje."
                       + "<img src=\"" + message.MainImageUrl + "\"/>" +
                       "Znajduje się on pod tym linkiem: " + message.ProductUrl + "\r\n" +
                       "Jeśli chcesz zrezygnować z newslettera kliknij w ten link: <a href=\"www.crochetbyjk.pl/newsletter/usun/>\"</p></div></body></html>";

            Parallel.ForEach(emailMessage.To, email =>
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(emailMessage.From),
                    Subject = emailMessage.Subject,
                    Body = body + email,
                    To = {email}
                };
                mailMessages.Add(mailMessage);
            });
            return mailMessages.ToArray();
        }

        private MailMessage[] GetProductQuestionTypeofMessages(IEmailMessage emailMessage)
        {
            var body = "Pytanie od użytkownika: " + emailMessage.From + "\r\n" + emailMessage.Body;
            var target = "daniel.dziubecki@wp.pl";
            var mailMessages = new ConcurrentBag<MailMessage>();
            Parallel.ForEach(emailMessage.To, email =>
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(email),
                    Subject = emailMessage.Subject,
                    Body = body,
                    To = {target}
                };
                mailMessages.Add(mailMessage);
            });
            return mailMessages.ToArray();
        }
    }
}