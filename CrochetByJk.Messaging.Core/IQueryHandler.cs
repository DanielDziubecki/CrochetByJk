using System;
using System.Threading.Tasks;

namespace CrochetByJk.Messaging.Core
{
    public interface IQueryHandler<in TQuery, TResult> : IDisposable
    {
         Task<TResult> HandleAsync(TQuery query);
    }
}