using System.Web.Mvc;
using System.Web.UI;
using CrochetByJk.Common.Roles;
using CrochetByJk.Components.ProductGalleryProvider;
using CrochetByJk.Components.Validators;
using CrochetByJk.ErrorHandlers;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Messaging.Queries;
using CrochetByJk.Model.Model;
using DevTrends.MvcDonutCaching;

namespace CrochetByJk.Controllers
{
    [Authorize(Roles = ApplicationRoles.Administrator)]
    [NlogHandleError(View = "Error")]
    [DonutOutputCache(Duration = 60 * 600, Location = OutputCacheLocation.ServerAndClient)]
    public class AdminController : Controller
    {
        private readonly ICqrsBus bus;

        public AdminController(ICqrsBus bus, IValidator<Product> validator, IProductGalleryProvider galleryProvider)
        {
            this.bus = bus;
        }

        [Route("admin")]
        public ActionResult Panel()
        {
            return View();
        }

        [Route("categories")]
        public JsonResult GetCategories()
        {
            var categories = bus.RunQuery(new GetAllCategoriesQuery());
            return Json(categories, JsonRequestBehavior.AllowGet);
        }
    }
}