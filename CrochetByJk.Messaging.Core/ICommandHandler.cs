using System.Threading.Tasks;

namespace CrochetByJk.Messaging.Core
{
    public interface ICommandHandler<in TCommand> where TCommand : class , ICommand
    {
        Task HandleAsync(TCommand command);
        void Validate(TCommand command);
    }
}