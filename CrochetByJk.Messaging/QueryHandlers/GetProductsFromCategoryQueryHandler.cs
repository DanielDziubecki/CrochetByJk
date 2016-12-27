using System.Data.Entity;
using System.Linq;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Messaging.Queries;
using CrochetByJk.Model.Contexts;
using CrochetByJk.Model.Model;

namespace CrochetByJk.Messaging.QueryHandlers
{
    public class GetProductsFromCategoryQueryHandler : IQueryHandler<GetProductsFromCategoryQuery, Product[]>
    {
        private readonly CrochetByJkDbContext context;

        public GetProductsFromCategoryQueryHandler(CrochetByJkDbContext context)
        {
            this.context = context;
        }

        public Product[] Handle(GetProductsFromCategoryQuery query)
        {
            return context.Products.Where(x => x.IdCategory == query.CategoryId)
                .Include(x => x.ProductGallery)
                .ToArray();
        }
    }
}