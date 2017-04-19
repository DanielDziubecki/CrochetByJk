using CrochetByJk.Messaging.Core;
using CrochetByJk.Model.Model;

namespace CrochetByJk.Messaging.Commands
{
    public class UpdateProductCommand : ICommand
    {
        public Product UpdatedProduct { get; set; }
    }
}