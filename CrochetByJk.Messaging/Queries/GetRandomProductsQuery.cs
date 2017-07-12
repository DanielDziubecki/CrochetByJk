using CrochetByJk.Messaging.Core;
using CrochetByJk.Model.Model;

namespace CrochetByJk.Messaging.Queries
{
    public class GetRandomProductsQuery : IQuery<Product[]>
    {
        public int Amount { get; set; }
    }
}