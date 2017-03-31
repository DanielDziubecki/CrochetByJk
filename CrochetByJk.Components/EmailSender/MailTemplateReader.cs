using System.IO;
using System.Web;
using CrochetByJk.Components.Exceptions;

namespace CrochetByJk.Components.EmailSender
{
    public class MailTemplateReader : IMailTemplateReader
    {
        private static readonly string ServerRoot = HttpContext.Current.Server.MapPath(WebSiteRoot);
        private const string WebSiteRoot = @"~\Content\MailTemplates\";

        private static readonly string NewsletterPath = Path.Combine(ServerRoot, "newsletter_template.html");
        private const string ProductQuestionPath = "";

        public string GetTemplate(MailTemplateType mailType)
        {
            switch (mailType)
            {
                case MailTemplateType.Newsletter:
                    return File.ReadAllText(NewsletterPath);

                case MailTemplateType.ProductQuestion:
                    return File.ReadAllText(ProductQuestionPath);

                default:
                    throw new UnknownEmailMessageType(mailType.GetType());
            }
        }
    }
}