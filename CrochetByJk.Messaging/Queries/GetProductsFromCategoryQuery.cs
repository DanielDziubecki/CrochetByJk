using System;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Model.Model;

namespace CrochetByJk.Messaging.Queries
{
    public class GetProductsFromCategoryQuery : IQuery<Product[]>
    {
        public Guid CategoryId { get; set; }
    }
}
