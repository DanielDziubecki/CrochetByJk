using System.Web.Mvc;
using AutoMapper;
using CrochetByJk.Components.ProductGalleryProvider;
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
        private readonly IPictureResizer pictureResizer;

        public HomeController(ICqrsBus bus, IMapper mapper, IPictureResizer pictureResizer)
        {
            this.bus = bus;
            this.mapper = mapper;
            this.pictureResizer = pictureResizer;
        }

        public ActionResult Index()
        {
            var products = bus.RunQuery<Product[]>(new GetLastProductsQuery {Amount = 3});
            var viewModel = mapper.Map<ProductTileViewModel[]>(products);
            foreach (var productTileViewModel in viewModel)
                pictureResizer.Resize(productTileViewModel);
            return View("Index", viewModel);
        }
    }
}