using Autofac;
using Autofac.Integration.Mvc;
using NLog;

namespace CrochetByJk.Registration
{
    public class Registration
    {
        public static void Register(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterControllers(typeof(MvcApplication).Assembly);
            containerBuilder.RegisterInstance(AutoMapperConfig.GetMapper());
            containerBuilder.RegisterInstance(LogManager.GetLogger("crochetDbLogger")).As<ILogger>().SingleInstance();
            Messaging.Registration.Registration.Register(containerBuilder);
            Components.Registration.Registration.Register(containerBuilder);
            Model.Registration.Registration.Register(containerBuilder);
        }
    }
}