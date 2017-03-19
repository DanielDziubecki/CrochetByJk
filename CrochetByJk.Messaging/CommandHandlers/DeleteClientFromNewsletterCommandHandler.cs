using System.Linq;
using CrochetByJk.Messaging.Commands;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Model.Contexts;

namespace CrochetByJk.Messaging.CommandHandlers
{
    public class DeleteClientFromNewsletterCommandHandler : ICommandHandler<DeleteClientFromNewsletterCommand>
    {
        private readonly CrochetByJkDbContext context;

        public DeleteClientFromNewsletterCommandHandler(CrochetByJkDbContext context)
        {
            this.context = context;
        }
        public void Handle(DeleteClientFromNewsletterCommand command)
        {
            var client = context.NewsletterClients.SingleOrDefault(x => x.Email == command.Email.Trim().ToLowerInvariant());
            if (client == null) return;
            context.NewsletterClients.Remove(client);
            context.SaveChanges();
        }
    }
}