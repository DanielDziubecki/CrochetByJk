using System.Threading.Tasks;
using Autofac;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Messaging.Exceptions;

namespace CrochetByJk.Messaging.Bus
{
    public class CqrsBus : ICqrsBus
    {
        private readonly IComponentContext context;

        public CqrsBus(IComponentContext context)
        {
            this.context = context;
        }

        public async Task<IHandleResult> ExecuteCommandAsync(ICommand command)
        {
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            dynamic handler = context.Resolve(handlerType);
            if (handler == null)
                throw new NotImplementedHandlerException($"Cannot find handler for {command.GetType()}");
            await (Task) handler.HandleAsync((dynamic) command);
            return new HandleResult.HandleResult(true);
        }

        public async Task<TResult> RunQueryAsync<TResult>(IQuery query)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            dynamic handler = context.Resolve(handlerType);
            if (handler == null)
                throw new NotImplementedHandlerException($"Cannot find handler for {query.GetType()}");
            return await (Task<TResult>) handler.HandleAsync((dynamic) query);
        }
    }
}