using System;
using CrochetByJk.Messaging.Core;

namespace CrochetByJk.Messaging.Commands
{
    public class DeleteProductCommand : ICommand
    {
        public Guid IdProduct { get; set; }
    }
}