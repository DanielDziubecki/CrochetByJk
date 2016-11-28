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

        public IHandleResult ExecuteCommand(ICommand command)
        {
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            dynamic handler = context.Resolve(handlerType);
            if (handler == null)
                throw new NotImplementedHandlerException($"Cannot find handler for {command.GetType()}");
            handler.Handle((dynamic) command);
            return new HandleResult.HandleResult(true);
        }

        public TResult RunQuery<TResult>(IQuery query)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            dynamic handler = context.Resolve(handlerType);
            if (handler == null)
                throw new NotImplementedHandlerException($"Cannot find handler for {query.GetType()}");
            return handler.Handle((dynamic) query);
        }
    }
}