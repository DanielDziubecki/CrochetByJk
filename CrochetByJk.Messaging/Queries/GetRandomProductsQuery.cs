using CrochetByJk.Messaging.Core;

namespace CrochetByJk.Messaging.Queries
{
    public class GetRandomProductsQuery : IQuery
    {
        public int Amount { get; set; }
    }
}