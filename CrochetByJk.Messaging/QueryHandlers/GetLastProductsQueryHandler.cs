using System.Data.Entity;
using System.Linq;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Messaging.Queries;
using CrochetByJk.Model.Contexts;
using CrochetByJk.Model.Model;

namespace CrochetByJk.Messaging.QueryHandlers
{
    public class GetLastProductsQueryHandler : IQueryHandler<GetLastProductsQuery, Product[]>
    {
        private readonly CrochetByJkDbContext context;

        public GetLastProductsQueryHandler(CrochetByJkDbContext context)
        {
            this.context = context;
        }

        public Product[] Handle(GetLastProductsQuery query)
        {
            return context.Products.
                           OrderByDescending(x => x.InsertDate).
                           Include(x => x.ProductGallery).Take(query.Amount).ToArray();
        }
    }
}