using System.Linq;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Messaging.Queries;
using CrochetByJk.Model.Contexts;
using CrochetByJk.Model.Model;

namespace CrochetByJk.Messaging.QueryHandlers
{
    public class GetAllCategoriesQueryHandler : IQueryHandler<GetAllCategoriesQuery, Category[]>
    {
        private readonly CrochetByJkDbContext context;

        public GetAllCategoriesQueryHandler(CrochetByJkDbContext context)
        {
            this.context = context;
        }
        public Category[] Handle(GetAllCategoriesQuery query)
        {
            return context.Categories.ToArray();
        }

        public void Dispose()
        {
        }
    }
}