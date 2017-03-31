using System.Linq;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Messaging.Queries;
using CrochetByJk.Model.Contexts;
using CrochetByJk.Model.Model;

namespace CrochetByJk.Messaging.QueryHandlers
{
    public class GetNewletterClientsQueryHandler : IQueryHandler<GetNewletterClientsQuery,NewsletterClient[]>
    {
        private readonly CrochetByJkDbContext context;

        public GetNewletterClientsQueryHandler(CrochetByJkDbContext context)
        {
            this.context = context;
        }
        public NewsletterClient[] Handle(GetNewletterClientsQuery query)
        {
            return context.NewsletterClients.ToArray();
        }
    }
}