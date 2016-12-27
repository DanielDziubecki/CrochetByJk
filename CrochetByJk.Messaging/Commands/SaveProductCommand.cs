using CrochetByJk.Messaging.Core;
using CrochetByJk.Model.Model;

namespace CrochetByJk.Messaging.Commands
{
    public class SaveProductCommand : ICommand
    {
        public readonly Product Product;

        public SaveProductCommand(Product product)
        {
            Product = product;
        }
    }
}
