using System;

namespace CrochetByJk.Components.Exceptions
{
    [Serializable]
    public class UnknownEmailMessageType : Exception
    {
        public readonly Type MessageType;

        public UnknownEmailMessageType(Type messageType)
        {
            MessageType = messageType;
        }
    }
}