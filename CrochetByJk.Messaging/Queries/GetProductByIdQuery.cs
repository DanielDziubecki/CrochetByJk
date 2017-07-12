using System;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Model.Model;

namespace CrochetByJk.Messaging.Queries
{
    public class GetProductByIdQuery : IQuery<Product>
    {
        public Guid ProductId { get; set; }
    }
}