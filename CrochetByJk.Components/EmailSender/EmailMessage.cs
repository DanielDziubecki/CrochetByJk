namespace CrochetByJk.Components.EmailSender
{
    public class EmailMessage : IEmailMessage
    {
        public string From { get; }
        public string To { get; }
        public string Subject { get; }
        public string Body { get; }

        public EmailMessage(string from, string to, string subject, string body)
        {
            From = from;
            To = to;
            Subject = subject;
            Body = body;
        }
    }
}