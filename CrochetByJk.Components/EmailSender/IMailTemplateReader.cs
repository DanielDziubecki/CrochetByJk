namespace CrochetByJk.Components.EmailSender
{
    public interface IMailTemplateReader
    {
        string GetTemplate(MailTemplateType mailType);
    }
}