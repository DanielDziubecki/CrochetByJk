using System;
using CrochetByJk.Messaging.Core;

namespace CrochetByJk.Messaging.Queries
{
    public class GetProductByNameInCategoryQuery : IQuery
    {
        public Guid CategoryId { get; set; }
        public string ProductName { get; set; }
    }
}