using System;

namespace CrochetByJk.Messaging.Exceptions
{
    public class NotImplementedHandlerException : Exception
    {
        private readonly string message;

        public NotImplementedHandlerException(string message) : base(message)
        {
            this.message = message;
        }
    }
}