using System.Web.Mvc;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Messaging.Queries;
using CrochetByJk.Model.Model;
using CrochetByJk.Roles;

namespace CrochetByJk.Controllers
{
    [Authorize(Roles = ApplicationRoles.Administrator)]
    public class AdminController : Controller
    {
        private readonly ICqrsBus bus;


        public AdminController(ICqrsBus bus)
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
            var categories = bus.RunQuery<Category[]>(new GetAllCategoriesQuery());
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        [Route("test")]
        public ActionResult TestView()
        {
            return View();
        }
    }
}