using System.Data.Entity;
using System.Linq;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Messaging.Queries;
using CrochetByJk.Model.Contexts;
using CrochetByJk.Model.Model;

namespace CrochetByJk.Messaging.QueryHandlers
{
    public class GetProductByNameInCategoryQueryHandler : IQueryHandler<GetProductByNameInCategoryQuery, Product>
    {
        private readonly CrochetByJkDbContext context;

        public GetProductByNameInCategoryQueryHandler(CrochetByJkDbContext context)
        {
            this.context = context;
        }

        public Product Handle(GetProductByNameInCategoryQuery query)
        {
            var products = context.Products.Include(x => x.ProductGallery).Include(x=>x.Category).ToList();
            return products.SingleOrDefault(x => x.Category.IdCategory == query.CategoryId && x.UrlFriendlyName == query.ProductName);
        }
    }
}