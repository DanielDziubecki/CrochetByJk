using System;
using System.Net;
using System.Web.Mvc;
using CrochetByJk.ErrorHandlers;
using CrochetByJk.Messaging.Commands;
using CrochetByJk.Messaging.Core;
using NLog;

namespace CrochetByJk.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("newsletter")]
    [NlogHandleError(View = "Error")]
    public class NewsletterController : Controller
    {
        private readonly ICqrsBus bus;
        private readonly ILogger logger;

        public NewsletterController(ICqrsBus bus)
        {
            this.bus = bus;
            logger = LogManager.GetLogger("crochetDbLogger");
        }

        [Route("potwierdz/{clientId}")]
        public ActionResult ConfirmDeletion(string clientId)
        {
            return View("DeleteFromNewsletter", (object)clientId);
        }

        [Route("usun")]
        [HttpPost]
        public ActionResult Delete(string clientId)
        {
            var referer = Request.UrlReferrer;
            if (referer == null)
                return RedirectToAction("Error", "Home");

            try
            {
                bus.ExecuteCommand(new DeleteClientFromNewsletterCommand {Id = clientId});
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Cannot delete newletter client with id = {clientId}");
            }
            return Json(HttpStatusCode.Accepted,JsonRequestBehavior.AllowGet);
        }
    }
}