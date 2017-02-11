using System;
using CrochetByJk.Messaging.Core;

namespace CrochetByJk.Messaging.Queries
{
    public class GetProductQuery : IQuery
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
    }
}