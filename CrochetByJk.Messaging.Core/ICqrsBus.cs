using System.Threading.Tasks;

namespace CrochetByJk.Messaging.Core
{
    public interface ICqrsBus
    {
        Task<IHandleResult> ExecuteCommandAsync(ICommand command);
        Task<TResult> RunQueryAsync<TResult>(IQuery query);
    }
}