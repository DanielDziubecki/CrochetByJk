using CrochetByJk.Messaging.Core;

namespace CrochetByJk.Messaging.Commands
{
    public class DeleteClientFromNewsletterCommand : ICommand
    {
        public string Id { get; set; }
    }
}