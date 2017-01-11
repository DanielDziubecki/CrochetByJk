using System.Web.Mvc;
using AutoMapper;
using CrochetByJk.Common.Categories;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Messaging.Queries;
using CrochetByJk.Model.Model;
using CrochetByJk.ViewModel;

namespace CrochetByJk.Controllers
{
    [RoutePrefix("produkty")]
    public class ProductController : Controller
    {
        private readonly ICqrsBus bus;
        private readonly IMapper mapper;

        public ProductController(ICqrsBus bus, IMapper mapper)
        {
            this.bus = bus;
            this.mapper = mapper;
        }

        [Route("sukienki")]
        public ActionResult Dresses()
        {
            var products = bus.RunQuery<Product[]>(new GetProductsFromCategoryQuery {CategoryId = Categories.Dresses});
            var viewModels = mapper.Map<ProductViewModel[]>(products);
            return View(viewModels);
        }

        [Route("dladzieci")]
        public ActionResult ForChildren()
        {
            var products = bus.RunQuery<Product[]>(new GetProductsFromCategoryQuery { CategoryId = Categories.ForChildren });
            var viewModels = mapper.Map<ProductViewModel[]>(products);
            return View(viewModels);
        }

        [Route("swetry")]
        public ActionResult Sweaters()
        {
            var products = bus.RunQuery<Product[]>(new GetProductsFromCategoryQuery { CategoryId = Categories.Sweaters });
            var viewModels = mapper.Map<ProductViewModel[]>(products);
            return View(viewModels);
        }

        [Route("torby")]
        public ActionResult Bags()
        {
            var products = bus.RunQuery<Product[]>(new GetProductsFromCategoryQuery { CategoryId = Categories.Bags });
            var viewModels = mapper.Map<ProductViewModel[]>(products);
            return View(viewModels);
        }

        [Route("dekoracje")]
        public ActionResult Decor()
        {
            var products = bus.RunQuery<Product[]>(new GetProductsFromCategoryQuery { CategoryId = Categories.Decor });
            var viewModels = mapper.Map<ProductViewModel[]>(products);
            return View(viewModels);
        }
    }
}