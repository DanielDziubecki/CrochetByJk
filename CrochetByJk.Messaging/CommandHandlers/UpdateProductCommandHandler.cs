using System;
using System.Data.Entity;
using System.Linq;
using CrochetByJk.Messaging.Commands;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Model.Contexts;

namespace CrochetByJk.Messaging.CommandHandlers
{
    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand>
    {
        private readonly CrochetByJkDbContext context;

        public UpdateProductCommandHandler(CrochetByJkDbContext context)
        {
            this.context = context;
        }

        public void Handle(UpdateProductCommand command)
        {
            var productToUpdate =
                context.Products.Include(x => x.ProductGallery)
                    .SingleOrDefault(x => x.IdProduct == command.UpdatedProduct.IdProduct);

            if (productToUpdate == null)
                return;

            if (command.UpdatedProduct.ProductGallery != null && command.UpdatedProduct.IdMainPicture != Guid.Empty)
            {
                context.Pictures.RemoveRange(context.Pictures.Where(x => x.IdProduct == command.UpdatedProduct.IdProduct));
                context.Pictures.AddRange(command.UpdatedProduct.ProductGallery);
                productToUpdate.IdMainPicture = command.UpdatedProduct.IdMainPicture;
            }

            productToUpdate.Description = command.UpdatedProduct.Description;
            productToUpdate.IdCategory = command.UpdatedProduct.IdCategory;
            productToUpdate.InsertDate = command.UpdatedProduct.InsertDate;
            productToUpdate.Name = command.UpdatedProduct.Name;
            productToUpdate.ProductUrl = command.UpdatedProduct.ProductUrl;
            productToUpdate.UrlFriendlyName = command.UpdatedProduct.UrlFriendlyName;

            context.SaveChanges();
        }
    }
}