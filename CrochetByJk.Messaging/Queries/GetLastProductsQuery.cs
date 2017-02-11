using CrochetByJk.Messaging.Core;

namespace CrochetByJk.Messaging.Queries
{
    public class GetLastProductsQuery : IQuery
    {
        public int Amount { get; set; }
    }
}