using System;

namespace CrochetByJk.Messaging.Core
{
    public interface IQueryHandler<in TQuery, out TResult> where TQuery : IQuery
    {
        TResult Handle(TQuery query);
    }
}