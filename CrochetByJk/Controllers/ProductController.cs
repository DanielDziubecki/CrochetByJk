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
            var dresses = PrepareTilesViewModel(CategoriesConstants.Dresses);
            return View("ProductsInCategory", dresses);
        }

        [Route("sukienki/{name}")]
        public ActionResult Dresses(string name)
        {
            var viewModel = PrepareProductViewModel(name, CategoriesConstants.Dresses);
            return View("ProductDetails", viewModel);
        }

        [Route("dladzieci")]
        public ActionResult ForChildren()
        {
            var forChildren = PrepareTilesViewModel(CategoriesConstants.ForChildren);
            return forChildren.Length > 0 ? View("ProductsInCategory", forChildren) : View("NoProducts");
        }

        [Route("dladzieci/{name}")]
        public ActionResult ForChildren(string name)
        {
            var productDetails = PrepareProductViewModel(name, CategoriesConstants.ForChildren);
            return View("ProductDetails", productDetails);
        }

        [Route("swetry")]
        public ActionResult Sweaters()
        {
            var sweaters = PrepareTilesViewModel(CategoriesConstants.Sweaters);
            return sweaters.Length > 0 ? View("ProductsInCategory", sweaters) : View("NoProducts");
        }

        [Route("swetry/{name}")]
        public ActionResult Sweaters(string name)
        {
            var productDetails = PrepareProductViewModel(name, CategoriesConstants.Sweaters);
            return View("ProductDetails", productDetails);
        }

        [Route("torebki")]
        public ActionResult Bags()
        {
            var bags = PrepareTilesViewModel(CategoriesConstants.Bags);
            return bags.Length > 0 ? View("ProductsInCategory", bags) : View("NoProducts");
        }

        [Route("torebki/{name}")]
        public ActionResult Bags(string name)
        {
            var productDetails = PrepareProductViewModel(name, CategoriesConstants.Bags);
            return View("ProductDetails", productDetails);
        }

        [Route("dekoracje")]
        public ActionResult Decor()
        {
            var decors = PrepareTilesViewModel(CategoriesConstants.Decor);
            return decors.Length > 0 ? View("ProductsInCategory", decors) : View("NoProducts");
        }

        [Route("dekoracje/{name}")]
        public ActionResult Decor(string name)
        {
            var productDetails = PrepareProductViewModel(name, CategoriesConstants.Decor);
            return View("ProductDetails", productDetails);
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

        private ProductTileViewModel[] PrepareTilesViewModel(string categoryId)
        {
            var products = bus.RunQuery<Product[]>(new GetProductsFromCategoryQuery
            {
                CategoryId = new Guid(categoryId)
            });
            var viewModel = mapper.Map<ProductTileViewModel[]>(products);
            foreach (var productTileViewModel in viewModel)
                pictureResizer.Resize(productTileViewModel, Request.Browser.IsMobileDevice);
            return viewModel;
        }

        private ProductWithSeeAlsoProductsViewModel PrepareProductViewModel(string name, string categoryId)
        {
            var selectedProduct = bus.RunQuery<Product>(new GetProductQuery
            {
                Id = new Guid(categoryId),
                ProductName = name
            });
            var viewModel = mapper.Map<ProductWithSeeAlsoProductsViewModel>(selectedProduct);
            var seeAlsoProducts = bus.RunQuery<Product[]>(new GetRandomProductsQuery {Amount = 3});
            var seeAlsoVm = mapper.Map<ProductTileViewModel[]>(seeAlsoProducts);
            foreach (var productTileViewModel in seeAlsoVm)
            {
                pictureResizer.Resize(productTileViewModel, Request.Browser.IsMobileDevice);
            }
            viewModel.SeeAlsoProducts = seeAlsoVm;
            return viewModel;
        }
    }
}