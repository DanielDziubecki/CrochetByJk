using System;
using System.Linq;
using CrochetByJk.Common.ShortGuid;
using CrochetByJk.Messaging.Commands;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Model.Contexts;
using CrochetByJk.Model.Model;

namespace CrochetByJk.Messaging.CommandHandlers
{
    public class AddNewsletterClientCommandHandler : ICommandHandler<AddNewsletterClientCommand>
    {
        private readonly CrochetByJkDbContext context;

        public AddNewsletterClientCommandHandler(CrochetByJkDbContext context)
        {
            this.context = context;
        }

        public void Handle(AddNewsletterClientCommand command)
        {
            var clientEmail = command.ClientEmail.Trim().ToLowerInvariant();
            var isExistingClient =
                context.NewsletterClients.Any(x => x.Email == clientEmail);

            if (isExistingClient)
                return;

            context.NewsletterClients.Add(new NewsletterClient
            {
                Email = clientEmail,
                Id = ShortGuid.NewGuid(),
                InsertDate = DateTime.Now
            });
            context.SaveChanges();
        }
    }
}