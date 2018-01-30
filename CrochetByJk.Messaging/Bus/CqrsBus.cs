using System;
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

        public void ExecuteCommand(MSMQMessage command)
        {
            var type = Type.GetType(command.IncomingMessageNamespace)
            try
            {
                var handlerType = typeof(ICommandHandler<>).MakeGenericType(type);
                dynamic handler = context.Resolve(handlerType);
                if (handler == null)
                    throw new NotImplementedHandlerException($"Cannot find handler for {command.GetType()}");
                handler.Handle((dynamic) command);

            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        public TResult RunQuery<TResult>(IQuery<TResult> query)
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
    }
}
