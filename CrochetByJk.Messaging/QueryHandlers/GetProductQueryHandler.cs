using System.Data.Entity;
using System.Linq;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Messaging.Queries;
using CrochetByJk.Model.Contexts;
using CrochetByJk.Model.Model;

namespace CrochetByJk.Messaging.QueryHandlers
{
    public class GetProductQueryHandler : IQueryHandler<GetProductQuery, Product>
    {
        private readonly CrochetByJkDbContext context;

        public GetProductQueryHandler(CrochetByJkDbContext context)
        {
            this.context = context;
        }

        public Product Handle(GetProductQuery query)
        {
            var products = context.Products.Include(x => x.ProductGallery).Include(x=>x.Category).ToList();
            return products.SingleOrDefault(x => x.Category.IdCategory == query.Id && x.UrlFriendlyName == query.ProductName);
        }
    }
}