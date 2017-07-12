using System.Threading.Tasks;

namespace CrochetByJk.Messaging.Core
{
    public interface ICqrsBus
    {
        void ExecuteCommand(ICommand command);

        TResult RunQuery<TResult>(IQuery<TResult> query);
    }
}

