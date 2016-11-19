using Autofac;

namespace CrochetByJk.Registration
{
    public class Registration
    {
        public static void Register(ContainerBuilder containerBuilder)
        {
           Messaging.Registration.Registration.Register(containerBuilder);
        }
    }
}
