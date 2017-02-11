using System.Web.Mvc;
using AutoMapper;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Messaging.Queries;
using CrochetByJk.Model.Model;
using CrochetByJk.ViewModel;

namespace CrochetByJk.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ICqrsBus bus;
        private readonly IMapper mapper;

        public HomeController(ICqrsBus bus, IMapper mapper)
        {
            this.bus = bus;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var products = bus.RunQuery<Product[]>(new GetLastProductsQuery {Amount = 3});
            var viewModel = mapper.Map<ProductTileViewModel[]>(products);
            return View("Index", viewModel);
        }
    }
}