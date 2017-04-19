using System.Linq;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Messaging.Queries;
using CrochetByJk.Model.Contexts;
using CrochetByJk.Model.Model;

namespace CrochetByJk.Messaging.QueryHandlers
{
    public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery,Product>
    {
        private readonly CrochetByJkDbContext context;

        public GetProductByIdQueryHandler(CrochetByJkDbContext context)
        {
            this.context = context;
        }

        public Product Handle(GetProductByIdQuery query)
        {
            return context.Products.SingleOrDefault(x => x.IdProduct == query.ProductId);
        }
    }
}