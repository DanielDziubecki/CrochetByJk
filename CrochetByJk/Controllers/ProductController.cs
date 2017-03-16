using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using CrochetByJk.Common.Constants;
using CrochetByJk.Components.EmailSender;
using CrochetByJk.Components.ProductGalleryProvider;
using CrochetByJk.ErrorHandlers;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Messaging.Queries;
using CrochetByJk.Model.Model;
using CrochetByJk.ViewModel;

namespace CrochetByJk.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("produkty")]
    [NlogHandleError(View = "Error")]
    public class ProductController : Controller
    {
        private readonly ICqrsBus bus;
        private readonly IMapper mapper;
        private readonly IPictureResizer pictureResizer;
        private readonly IEmailSender emailSender;

        public ProductController(ICqrsBus bus, IMapper mapper, IPictureResizer pictureResizer, IEmailSender emailSender)
        {
            this.bus = bus;
            this.mapper = mapper;
            this.pictureResizer = pictureResizer;
            this.emailSender = emailSender;
        }

        [Route("sukienki")]
        public ActionResult Dresses()
        {
            var products = bus.RunQuery<Product[]>(new GetProductsFromCategoryQuery
            {
                CategoryId = new Guid(CategoriesConstants.Dresses)
            });
            var dressesTiles = mapper.Map<ProductTileViewModel[]>(products);
            foreach (var productTileViewModel in dressesTiles)
                pictureResizer.Resize(productTileViewModel, Request.Browser.IsMobileDevice);
            return View("ProductsInCategory", dressesTiles);
        }

        [Route("sukienki/{name}")]
        public ActionResult Dresses(string name)
        {
            var selectedProduct = bus.RunQuery<Product>(new GetProductQuery
            {
                Id = new Guid(CategoriesConstants.Dresses),
                ProductName = name
            });
            var viewModel = mapper.Map<ProductViewModel>(selectedProduct);
            var seeAlsoProducts = bus.RunQuery<Product[]>(new GetRandomProductsQuery { Amount = 3});
            var seeAlsoVm = mapper.Map<ProductTileViewModel[]>(seeAlsoProducts);
            foreach (var productTileViewModel in seeAlsoVm)
            {
                pictureResizer.Resize(productTileViewModel, Request.Browser.IsMobileDevice);
            }
            viewModel.SeeAlsoProducts = seeAlsoVm;
            return View("ProductDetails", viewModel);
        }

        [Route("dladzieci")]
        public ActionResult ForChildren()
        {
            var products = bus.RunQuery<Product[]>(new GetProductsFromCategoryQuery
            {
                CategoryId = new Guid(CategoriesConstants.ForChildren)
            });
            var forChildren =
                mapper.Map<ProductTileViewModel[]>(products);
            foreach (var productTileViewModel in forChildren)
                pictureResizer.Resize(productTileViewModel, Request.Browser.IsMobileDevice);
            return View("ProductsInCategory", forChildren);
        }

        [Route("swetry")]
        public ActionResult Sweaters()
        {
            var products =
                bus.RunQuery<Product[]>(new GetProductsFromCategoryQuery
                {
                    CategoryId = new Guid(CategoriesConstants.Sweaters)
                });
            var sweaters = mapper.Map<ProductTileViewModel[]>(products);
            foreach (var productTileViewModel in sweaters)
                pictureResizer.Resize(productTileViewModel, Request.Browser.IsMobileDevice);
            return View("ProductsInCategory", sweaters);
        }

        [Route("torebki")]
        public ActionResult Bags()
        {
            var products =
                bus.RunQuery<Product[]>(new GetProductsFromCategoryQuery
                {
                    CategoryId = new Guid(CategoriesConstants.Bags)
                });
            var bags = mapper.Map<ProductTileViewModel[]>(products);
            foreach (var productTileViewModel in bags)
                pictureResizer.Resize(productTileViewModel, Request.Browser.IsMobileDevice);
            return View("ProductsInCategory", bags);
        }

        [Route("dekoracje")]
        public ActionResult Decor()
        {
            var products = bus.RunQuery<Product[]>(new GetProductsFromCategoryQuery
            {
                CategoryId = new Guid(CategoriesConstants.Decor)
            });
            var decors = mapper.Map<ProductTileViewModel[]>(products);
            foreach (var productTileViewModel in decors)
                pictureResizer.Resize(productTileViewModel, Request.Browser.IsMobileDevice);
            return View("ProductsInCategory", decors);
        }

        [Route("sendquestion")]
        public async Task<JsonResult> SendProductQuestion(ProductQuestion productQuestion)
        {
            try
            {
                await emailSender.SendAsync(productQuestion);
                return Json(new {Success = "True"});
            }
            catch (Exception ex)
            {
                return Json(new {Success = "False"});
            }
        }
    }
}