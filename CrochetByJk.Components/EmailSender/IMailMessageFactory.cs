using System.Net.Mail;

namespace CrochetByJk.Components.EmailSender
{
    public interface IMailMessageFactory
    {
        MailMessage[] GetMessages(IEmailMessage emailMessage);
    }
}