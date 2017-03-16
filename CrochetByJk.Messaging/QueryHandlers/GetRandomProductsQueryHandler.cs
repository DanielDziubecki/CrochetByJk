using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Messaging.Queries;
using CrochetByJk.Model.Contexts;
using CrochetByJk.Model.Model;

namespace CrochetByJk.Messaging.QueryHandlers
{
    public class GetRandomProductsQueryHandler : IQueryHandler<GetRandomProductsQuery, Product[]>
    {
        private readonly CrochetByJkDbContext context;


        public GetRandomProductsQueryHandler(CrochetByJkDbContext context)
        {
            this.context = context;
        }

        public Product[] Handle(GetRandomProductsQuery query)
        {
            var rndProducts = new List<Product>();
            var availableCategories = context.Products.Select(x => x.Category).Distinct().ToList();
            var amount = query.Amount;
            var rndCategories = availableCategories.OrderBy(x => Guid.NewGuid()).Take(amount).ToList();
            var numberOfCategories = rndCategories.Count();
            var productPerCategory = 1;
            if (numberOfCategories < amount)
                productPerCategory = (int)Math.Ceiling((double)amount/numberOfCategories);
            
            foreach (var category in rndCategories)
            {
                var rndInCategory = context.Products
                    .Where(x => x.Category.IdCategory == category.IdCategory)
                    .OrderBy(x => Guid.NewGuid())
                    .Include(x => x.ProductGallery)
                    .Take(productPerCategory).ToList();

                    rndProducts.AddRange(rndInCategory);
            }
            return rndProducts.Take(amount).ToArray();
        }
    }
}