using System.Threading.Tasks;

namespace CrochetByJk.Messaging.Core
{
    public interface ICqrsBus
    {
        IHandleResult ExecuteCommand(ICommand command);

        TResult RunQuery<TResult>(IQuery query);
        Task<TResult> RunQueryAsync<TResult>(IQuery query);
    }
}