namespace CrochetByJk.Components.EmailSender
{
    public class ProductQuestionMessage : IEmailMessage
    {
        public string From { get; set; }
        public string[] To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool AddToNewsletter { get; set; }
    }
}