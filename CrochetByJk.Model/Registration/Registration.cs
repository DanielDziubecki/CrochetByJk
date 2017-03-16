using System.Data.Entity;
using Autofac;
using CrochetByJk.Model.Contexts;

namespace CrochetByJk.Model.Registration
{
    public class Registration
    {
        public static void Register(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .RegisterType<CrochetByJkDbContext>()
                .InstancePerRequest();

            containerBuilder
                .RegisterType<SecurityContext>()
                   .InstancePerRequest();

        }
    }
}