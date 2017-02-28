using System.Threading.Tasks;

namespace CrochetByJk.Components.EmailSender
{
    public interface IEmailSender
    {
        void Send(IEmailMessage message);
        Task SendAsync(IEmailMessage message);
    }
}