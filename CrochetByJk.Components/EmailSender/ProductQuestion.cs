namespace CrochetByJk.Components.EmailSender
{
    public class ProductQuestion : IEmailMessage
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}