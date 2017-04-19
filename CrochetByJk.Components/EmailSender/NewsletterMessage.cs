using System;
using CrochetByJk.Model.Model;

namespace CrochetByJk.Components.EmailSender
{
    public class NewsletterMessage : IEmailMessage
    {
        public string From { get; set; }
        public string[] To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string ProductUrl { get; set; }
        public Guid ProductId { get; set; }
        public NewsletterPicture NewsLetterPicture { get; set; }
        public NewsletterClient[] NewsletterClients { get; set; }
    }
}