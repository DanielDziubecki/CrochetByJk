using System.Web.Mvc;
using NLog;

namespace CrochetByJk.ErrorHandlers
{
    public class NlogHandleError : HandleErrorAttribute
    {
        private readonly ILogger logger;
        public NlogHandleError()
        {
            logger = LogManager.GetLogger("crochetDbLogger");
        }
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            logger.Error(filterContext.Exception, filterContext.Exception.Message, filterContext.Exception);
        }
    }
}