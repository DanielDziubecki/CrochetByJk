using System;
using System.Threading.Tasks;
using Autofac;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Messaging.Exceptions;
using NLog;

namespace CrochetByJk.Messaging.Bus
{
    public class CqrsBus : ICqrsBus
    {
        private readonly IComponentContext context;
        private readonly ILogger logger;

        public CqrsBus(IComponentContext context)
        {
            this.context = context;
            logger = LogManager.GetLogger("crochetDbLogger");
        }

        public IHandleResult ExecuteCommand(ICommand command)
        {
            try
            {
                var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
                dynamic handler = context.Resolve(handlerType);
                if (handler == null)
                    throw new NotImplementedHandlerException($"Cannot find handler for {command.GetType()}");
                handler.Handle((dynamic) command);
                return new HandleResult.HandleResult(true);

            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            return new HandleResult.HandleResult(false);
        }

        public TResult RunQuery<TResult>(IQuery query)
        {
            try
            {
                var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
                dynamic handler = context.Resolve(handlerType);
                if (handler == null)
                    throw new NotImplementedHandlerException($"Cannot find handler for {query.GetType()}");
                return handler.Handle((dynamic)query);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
        }

        public Task<TResult> RunQueryAsync<TResult>(IQuery query)
        {
            return Task.Run(() => RunQuery<TResult>(query));
        }
    }
}