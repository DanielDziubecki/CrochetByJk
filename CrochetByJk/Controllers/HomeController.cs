using System.Web.Mvc;
using CrochetByJk.Messaging.Core;


namespace CrochetByJk.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ICqrsBus bus;

        public HomeController(ICqrsBus bus)
        {
            this.bus = bus;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}