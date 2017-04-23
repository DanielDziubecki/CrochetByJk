using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using CrochetByJk.Common.ShortGuid;
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
            var mailMessages = new List<MailMessage>();
            pictureResizer.Resize(message.NewsLetterPicture, true);

            var htmlTemplate = templateReader.GetTemplate(MailTemplateType.Newsletter);
            var baseNewsletter = GetBaseOfNewsletterMessage(htmlTemplate, message);

            foreach (var client in message.NewsletterClients)
            {
                baseNewsletter.Descendants("a").Single(x => x.Id == "cancelSubLink").Attributes.Remove("href");
                baseNewsletter.Descendants("a")
                    .Single(x => x.Id == "cancelSubLink")
                    .Attributes.Append("href", "https://" + $"www.crochetbyjk.pl/newsletter/potwierdz/{client.Id}");
                var htmlBody = baseNewsletter.InnerHtml;

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
            }
            return mailMessages.ToArray();
        }

        private static HtmlNode GetBaseOfNewsletterMessage(string template, NewsletterMessage message)
        {
            var html = new HtmlDocument();
            html.LoadHtml(template);
            var root = html.DocumentNode;
            root.Descendants("img")
                .Single(x => x.Id == "newProductImage")
                .Attributes.Append("src", $"cid:{message.NewsLetterPicture.LinkedResource.ContentId}");
            root.Descendants("a").Single(x => x.Id == "goToProduct").Attributes.Append("href", $"https://www.crochetbyjk.pl/newsletter/{ShortGuid.Encode(message.ProductId)}");
            return root;
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