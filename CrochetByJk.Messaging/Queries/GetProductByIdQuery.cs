using System;
using CrochetByJk.Messaging.Core;

namespace CrochetByJk.Messaging.Queries
{
    public class GetProductByIdQuery : IQuery
    {
        public Guid ProductId { get; set; }
    }
}