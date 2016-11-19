using System.Threading.Tasks;
using System.Web.Mvc;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Messaging.Queries;

namespace CrochetByJk.Controllers
{
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