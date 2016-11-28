using System;

namespace CrochetByJk.Messaging.Core
{
    public interface IQueryHandler<in TQuery, out TResult> : IDisposable
    {
         TResult Handle(TQuery query);
    }
}