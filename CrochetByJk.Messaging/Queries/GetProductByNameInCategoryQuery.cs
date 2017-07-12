using System;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Model.Model;

namespace CrochetByJk.Messaging.Queries
{
    public class GetProductByNameInCategoryQuery : IQuery<Product>
    {
        public Guid CategoryId { get; set; }
        public string ProductName { get; set; }
    }
}