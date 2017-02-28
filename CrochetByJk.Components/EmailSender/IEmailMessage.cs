namespace CrochetByJk.Components.EmailSender
{
    public interface IEmailMessage
    {
        string From { get; }
        string To { get; }
        string Subject { get; }
        string Body { get; }
    }
}