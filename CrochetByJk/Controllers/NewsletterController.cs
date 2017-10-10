using System;
using System.Net;
using System.Web.Mvc;
using System.Web.UI;
using CrochetByJk.Common.ShortGuid;
using CrochetByJk.ErrorHandlers;
using CrochetByJk.Messaging.Commands;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Messaging.Queries;
using DevTrends.MvcDonutCaching;
using NLog;

namespace CrochetByJk.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("newsletter")]
    [NlogHandleError(View = "Error")]
    [DonutOutputCache(Duration = 60 * 600, Location = OutputCacheLocation.ServerAndClient)]
    public class NewsletterController : Controller
    {
        private readonly ICqrsBus bus;
        private readonly ILogger logger;

        public NewsletterController(ICqrsBus bus,ILogger logger)
        {
            this.bus = bus;
            this.logger = logger;
        }

        [Route("{productId}")]
        public ActionResult RedirectToProduct(string productId)
        {
            var encodedId = ShortGuid.Decode(productId);
            var product = bus.RunQuery(new GetProductByIdQuery {ProductId = encodedId});
            if (product == null)
                return View("Error");

            return Redirect(product.ProductUrl);
        }

        [Route("potwierdz/{clientId}")]
        public ActionResult ConfirmDeletion(string clientId)
        {
            return View("DeleteFromNewsletter", (object) clientId);
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
            return Json(HttpStatusCode.Accepted, JsonRequestBehavior.AllowGet);
        }
    }
}