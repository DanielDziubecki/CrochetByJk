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
        public async  Task<ActionResult> Index()
        {
            var result = await bus.RunQueryAsync<string>(new TestQuery{TestString = "test"});
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}