using System;
using CrochetByJk.Messaging.Core;

namespace CrochetByJk.Messaging.Queries
{
    public class GetProductsFromCategoryQuery : IQuery
    {
        public Guid CategoryId { get; set; }
    }
}
