namespace CrochetByJk.Components.EmailSender
{
    public class NewsletterMessage : IEmailMessage
    {
        public string From { get; set; }
        public string[] To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string ProductUrl { get; set; }
        public string MainImageUrl { get; set; }
    }
}