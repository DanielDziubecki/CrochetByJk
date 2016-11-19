using System.Threading.Tasks;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Messaging.Queries;

namespace CrochetByJk.Messaging.QueryHandlers
{
    public class TestQueryHandler : IQueryHandler<TestQuery, string>
    {
        public async Task<string> HandleAsync(TestQuery query)
        {
            return await Task.Factory.StartNew(() => query.TestString + "done");
        }

        public void Dispose()
        {
        }
    }
}