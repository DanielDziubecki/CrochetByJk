using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using CrochetByJk.Common.Constants;
using CrochetByJk.Common.Roles;
using CrochetByJk.Components.EmailSender;
using CrochetByJk.Components.ProductGalleryProvider;
using CrochetByJk.Components.Validators;
using CrochetByJk.Dto;
using CrochetByJk.ErrorHandlers;
using CrochetByJk.Messaging.Commands;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Messaging.Queries;
using CrochetByJk.Model.Model;
using CrochetByJk.ViewModel;
using Newtonsoft.Json;
using NLog;
using ILogger = NLog.ILogger;

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
        private readonly IValidator<Product> validator;
        private readonly ILogger logger;
        private readonly IProductGalleryProvider galleryProvider;

        public ProductController(ICqrsBus bus,
            IMapper mapper,
            IPictureResizer pictureResizer,
            IEmailSender emailSender,
            IProductGalleryProvider galleryProvider,
            IValidator<Product> validator)
        {
            this.bus = bus;
            this.mapper = mapper;
            this.pictureResizer = pictureResizer;
            this.emailSender = emailSender;
            this.galleryProvider = galleryProvider;
            this.validator = validator;
            logger = LogManager.GetLogger("crochetDbLogger");
        }

        [Route("sukienki")]
        public ActionResult Dresses()
        {
            var dresses = PrepareTilesViewModel(CategoriesConstants.Dresses);
            return dresses.Length > 0 ? View("ProductsInCategory", dresses) : View("NoProducts");
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

        [Route("newsletter/usun/{email}")]
        public ActionResult RemoveClientFromNewsletter(string id)
        {
            return View("DeletedFromNewsLetter");
        }

        [Authorize(Roles = ApplicationRoles.Administrator)]
        [Route("dodajnowy")]
        [HttpPost]
        public JsonResult AddNewProduct(ProductDto productDto)
        {
            if (!ModelState.IsValid)
                return Json(new {Success = "False", responseText = "Wystąpił błąd. Wprowadź poprawne dane."});

            var productId = Guid.NewGuid();
            try
            {
                var productGallery =
                    galleryProvider.SaveProductGallery(new Gallery(productId, Request.Files, productDto.MainPhoto))
                        .ToArray();
                var mainPicture = productGallery.Single(x => x.IsMainPhoto);
                var product = new Product
                {
                    IdProduct = productId,
                    Name = productDto.Name,
                    IdCategory = productDto.IdCategory,
                    Description = productDto.Description,
                    IdMainPicture = mainPicture.IdPicture,
                    InsertDate = DateTime.Now,
                    ProductGallery = productGallery,
                    UrlFriendlyName = productDto.Name
                };
                validator.Validate(product);
                if (Request.Url == null)
                    throw new ArgumentNullException(nameof(Request.Url));
                var baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
                product.ProductUrl = $"{baseUrl}/Produkty/{productDto.CategoryName}/{product.UrlFriendlyName}";
                bus.ExecuteCommand(new SaveProductCommand(product));
                SendEmailsAboutNewProduct(product);
                return Json(new {Success = "True", responseText = "Dodano produkt.", Url = product.ProductUrl});
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                galleryProvider.DeleteProductGallery(productId);
                return
                    Json(new {Success = "False", responseText = "Wystąpił błąd. Spróbuj ponownie lub odśwież stronę."});
            }
        }

        [Route("zadajpytanie")]
        public async Task<JsonResult> SendProductQuestion(ProductQuestionMessage productQuestionMessage)
        {
            if (productQuestionMessage.AddToNewsletter)
                AddClientToNewsletter(productQuestionMessage.From);
            try
            {
                await emailSender.SendAsync(productQuestionMessage);
                return Json(new {Success = "True"});
            }
            catch (Exception ex)
            {
                //var serializedQuestion = JsonConvert.SerializeObject(productQuestionMessage);
                logger.Error(ex);
                return Json(new {Success = "False"});
            }
        }

        public void SendEmailsAboutNewProduct(Product newProduct)
        {
            var newsletterClients = bus.RunQuery<NewsletterClient[]>(new GetNewletterClientsQuery());
            var mainPicture = newProduct.ProductGallery.ToArray().Single(x => x.IsMainPhoto);

            var newsLetter = new NewsletterMessage
            {
                To = newsletterClients.Select(x=>x.Email).ToArray(),
                Body = "",
                From = "joannakuczynska@crochetbyjk.pl",
                Subject = "Dodano nowy produkt na crochetbyjk.pl",
                ProductUrl = newProduct.ProductUrl,
                NewsLetterPicture = new NewsletterPicture
                {
                    Width = mainPicture.Width,
                    Height = mainPicture.Height,
                    LinkedResource = new LinkedResource(Path.Combine("wwww.crochetbyjk.pl",
                        System.Web.HttpContext.Current.Server.MapPath(Path.Combine("~", mainPicture.Uri))),MediaTypeNames.Image.Jpeg)
                },
                NewsletterClients = newsletterClients
            };

            try
            {
                emailSender.Send(newsLetter);
            }
            catch (Exception ex)
            {
                //var message = JsonConvert.SerializeObject(newsLetter,
                //  new JsonSerializerSettings() {ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
                logger.Error(ex);
            }
        }

        private void AddClientToNewsletter(string email)
        {
            bus.ExecuteCommand(new AddNewsletterClientCommand {ClientEmail = email});
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