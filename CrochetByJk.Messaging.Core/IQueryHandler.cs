namespace CrochetByJk.Messaging.Core
{
    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery
    {
        TResult Handle(TQuery query);
    }
}