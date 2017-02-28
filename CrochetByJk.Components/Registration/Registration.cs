using System.Collections.Generic;
using Autofac;
using CrochetByJk.Components.EmailSender;
using CrochetByJk.Components.ProductGalleryProvider;
using CrochetByJk.Components.Validators;
using CrochetByJk.Model.Model;

namespace CrochetByJk.Components.Registration
{
    public class Registration
    {
        //todo: rejestracja generyków
        public static void Register(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<ProductGalleryProvider.ProductGalleryProvider>()
                .As<IProductGalleryProvider>();

            containerBuilder.RegisterType<ProductValidator>()
                .As<IValidator<Product>>();

            containerBuilder.RegisterType<PictureValidator>()
                .As<IValidator<IEnumerable<Picture>>>();

            containerBuilder.RegisterType<PictureResizer>()
               .As<IPictureResizer>();

            containerBuilder.RegisterType<EmailSender.EmailSender>()
               .As<IEmailSender>();
        }
    }
}