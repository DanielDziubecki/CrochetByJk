using Autofac;
using Autofac.Integration.Mvc;

namespace CrochetByJk.Registration
{
    public class Registration
    {
        public static void Register(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterControllers(typeof(MvcApplication).Assembly);
            containerBuilder.RegisterInstance(AutoMapperConfig.GetMapper());
            Messaging.Registration.Registration.Register(containerBuilder);
            Components.Registration.Registration.Register(containerBuilder);
            Model.Registration.Registration.Register(containerBuilder);
        }
    }
}