using System.Linq;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Messaging.Queries;
using CrochetByJk.Model.Contexts;

namespace CrochetByJk.Messaging.QueryHandlers
{
    public class GetNewletterClientsEmailsQueryHandler : IQueryHandler<GetNewletterClientsEmailsQuery,string[]>
    {
        private readonly CrochetByJkDbContext context;

        public GetNewletterClientsEmailsQueryHandler(CrochetByJkDbContext context)
        {
            this.context = context;
        }
        public string[] Handle(GetNewletterClientsEmailsQuery query)
        {
           return context.NewsletterClients.Select(x => x.Email).ToArray();
        }
    }
}