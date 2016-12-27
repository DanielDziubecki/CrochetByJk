using System;
using CrochetByJk.Messaging.Commands;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Model.Contexts;

namespace CrochetByJk.Messaging.CommandHandlers
{
    public class SaveProductCommandHandler : ICommandHandler<SaveProductCommand>
    {
        private readonly CrochetByJkDbContext context;

        public SaveProductCommandHandler(CrochetByJkDbContext context)
        {
            this.context = context;
        }

        public void Handle(SaveProductCommand command)
        {
            context.Products.Add(command.Product);
            context.Pictures.AddRange(command.Product.ProductGallery);
            context.SaveChanges();
        }
    }
}