using Autofac;

namespace CrochetByJk.Components.Registration
{
    public class Registration
    {
        public static void Register(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<ProductGalleryProvider>().As<IProductGalleryProvider>();
        }
    }
}