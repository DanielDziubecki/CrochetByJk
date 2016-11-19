using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using CrochetByJk.Controllers;

namespace CrochetByJk
{
    public class AutofacConfig
    {
        public static void RegisterComponents()
        {
            var builder = new ContainerBuilder();
            Registration.Registration.Register(builder);
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}