using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;

namespace CrochetByJk
{
    public class AutofacConfig
    {
        public static void RegisterComponents()
        {
            var builder = new ContainerBuilder();
            Registration.Registration.Register(builder);
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}