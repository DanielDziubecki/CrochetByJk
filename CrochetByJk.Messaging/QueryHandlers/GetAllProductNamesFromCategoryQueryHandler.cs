using System.Collections.Generic;
using System.Linq;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Messaging.Queries;
using CrochetByJk.Model.Contexts;

namespace CrochetByJk.Messaging.QueryHandlers
{
    public class GetAllProductNamesFromCategoryQueryHandler : IQueryHandler<GetAllProductNamesFromCategoryQuery, IEnumerable<string>>
    {
        private readonly CrochetByJkDbContext context;

        public GetAllProductNamesFromCategoryQueryHandler(CrochetByJkDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<string> Handle(GetAllProductNamesFromCategoryQuery query)
        {
            return context.Products.Where(x=>x.IdCategory==query.CategoryId).Select(x => x.UrlFriendlyName);
        }
    }
}