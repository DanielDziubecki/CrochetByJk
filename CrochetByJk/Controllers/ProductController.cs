using System;
using System.Web.Mvc;
using AutoMapper;
using CrochetByJk.Common.Constants;
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
            var products = bus.RunQuery<Product[]>(new GetProductsFromCategoryQuery {CategoryId = new Guid(CategoriesConstants.Dresses)});
            var dresses = mapper.Map<ProductViewModel[]>(products);
            return View("CategoryProducts",dresses);
        }

        [Route("dladzieci")]
        public ActionResult ForChildren()
        {
            var products = bus.RunQuery<Product[]>(new GetProductsFromCategoryQuery { CategoryId = new Guid(CategoriesConstants.ForChildren) });
            var forChildren = mapper.Map<ProductViewModel[]>(products);
            return View("CategoryProducts", forChildren);
        }

        [Route("swetry")]
        public ActionResult Sweaters()
        {
            var products = bus.RunQuery<Product[]>(new GetProductsFromCategoryQuery { CategoryId = new Guid(CategoriesConstants.Sweaters) });
            var sweaters = mapper.Map<ProductViewModel[]>(products);
            return View("CategoryProducts", sweaters);
        }

        [Route("torby")]
        public ActionResult Bags()
        {
            var products = bus.RunQuery<Product[]>(new GetProductsFromCategoryQuery { CategoryId = new Guid(CategoriesConstants.Bags) });
            var bags = mapper.Map<ProductViewModel[]>(products);
            return View("CategoryProducts", bags);
        }

        [Route("dekoracje")]
        public ActionResult Decor()
        {
            var products = bus.RunQuery<Product[]>(new GetProductsFromCategoryQuery { CategoryId = new Guid(CategoriesConstants.Decor) });
            var decors = mapper.Map<ProductViewModel[]>(products);
            return View("CategoryProducts", decors);
        }
    }
}