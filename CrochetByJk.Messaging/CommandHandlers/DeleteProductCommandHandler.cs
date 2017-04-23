using System.Linq;
using CrochetByJk.Messaging.Commands;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Model.Contexts;

namespace CrochetByJk.Messaging.CommandHandlers
{
    public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
    {
        private readonly CrochetByJkDbContext context;

        public DeleteProductCommandHandler(CrochetByJkDbContext context)
        {
            this.context = context;
        }

        public void Handle(DeleteProductCommand command)
        {
            var product = context.Products.SingleOrDefault(x => x.IdProduct == command.IdProduct);
            context.Pictures.RemoveRange(context.Pictures.Where(x => x.IdProduct == command.IdProduct));
            if(product!=null)
            context.Products.Remove(product);

            context.SaveChanges();
        }
    }
}