using CrochetByJk.Messaging.Core;

namespace CrochetByJk.Messaging.Commands
{
    public class AddNewsletterClientCommand : ICommand
    {
        public string ClientEmail { get; set; }
    }
}