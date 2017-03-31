using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using CrochetByJk.Components.Exceptions;
using CrochetByJk.Components.ProductGalleryProvider;
using HtmlAgilityPack;

namespace CrochetByJk.Components.EmailSender
{
    public class MailMessageFactory : IMailMessageFactory
    {
        private readonly IPictureResizer pictureResizer;
        private readonly IMailTemplateReader templateReader;

        public MailMessageFactory(IPictureResizer pictureResizer, IMailTemplateReader templateReader)
        {
            this.pictureResizer = pictureResizer;
            this.templateReader = templateReader;
        }

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
            pictureResizer.Resize(message.NewsLetterPicture);

            var template = templateReader.GetTemplate(MailTemplateType.Newsletter);
            var html = new HtmlDocument();
            html.LoadHtml(template);
            var root = html.DocumentNode;
            var img = root.Descendants("img").Single(x=>x.Id == "newProductImage").Attributes.Append("src",$"cid:{message.NewsLetterPicture.LinkedResource.ContentId}");
            var product = root.Descendants("a").Single(x => x.Id == "goToProduct").Attributes.Append("href", $"{message.ProductUrl}");
            Parallel.ForEach(message.NewsletterClients, client =>
            {
                var cancelSub = root.Descendants("a").Single(x => x.Id == "cancelSubLink").Attributes.Append("href", $"www.crochetbyjk.pl/newsletter/usun/{client.Id}");
                var htmlBody = root.InnerHtml;

                var avHtml = AlternateView.CreateAlternateViewFromString
                    (htmlBody, null, MediaTypeNames.Text.Html);

                avHtml.LinkedResources.Add(message.NewsLetterPicture.LinkedResource);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(emailMessage.From),
                    Subject = emailMessage.Subject,
                    Body = htmlBody,
                    To = {client.Email},
                    IsBodyHtml = true
                };
                mailMessage.AlternateViews.Add(avHtml);
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