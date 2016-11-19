using CrochetByJk.Messaging.Core;
namespace CrochetByJk.Messaging.HandleResult
{
    public class HandleResult : IHandleResult
    {
        public HandleResult(bool succeed)
        {
            Succeed = succeed;
        }
        public bool Succeed { get; }
    }
}