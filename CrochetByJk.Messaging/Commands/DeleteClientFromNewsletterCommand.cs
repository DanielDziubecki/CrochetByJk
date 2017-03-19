using CrochetByJk.Messaging.Core;

namespace CrochetByJk.Messaging.Commands
{
    public class DeleteClientFromNewsletterCommand : ICommand
    {
        public string Email { get; set; }
    }
}