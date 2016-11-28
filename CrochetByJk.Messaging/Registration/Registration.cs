using System.Reflection;
using Autofac;
using CrochetByJk.Messaging.Bus;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Model.Contexts;

namespace CrochetByJk.Messaging.Registration
{
    public class Registration
    {
        public static void Register(ContainerBuilder containerBuilder)
        {
            var assembly = Assembly.GetExecutingAssembly();

            containerBuilder.RegisterType<CqrsBus>().As<ICqrsBus>();
            containerBuilder.RegisterType<CrochetByJkDbContext>().AsSelf();

            containerBuilder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>));

            containerBuilder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IQueryHandler<,>));
        }
    }
}