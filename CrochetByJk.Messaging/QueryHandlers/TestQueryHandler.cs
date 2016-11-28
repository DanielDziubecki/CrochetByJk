using CrochetByJk.Messaging.Core;
using CrochetByJk.Messaging.Queries;

namespace CrochetByJk.Messaging.QueryHandlers
{
    public class TestQueryHandler : IQueryHandler<TestQuery, string>
    {
        public string Handle(TestQuery query)
        {
            return query.TestString + "done";
        }

        public void Dispose()
        {
        }
    }
}