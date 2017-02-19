using System;
using System.Linq;
using System.Web.Mvc;
using CrochetByJk.Common.Roles;
using CrochetByJk.Components.ProductGalleryProvider;
using CrochetByJk.Components.Validators;
using CrochetByJk.Dto;
using CrochetByJk.Messaging.Commands;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Messaging.Queries;
using CrochetByJk.Model.Model;

namespace CrochetByJk.Controllers
{
    [Authorize(Roles = ApplicationRoles.Administrator)]
    public class AdminController : Controller
    {
        private readonly ICqrsBus bus;
        private readonly IValidator<Product> validator;
        private readonly IProductGalleryProvider galleryProvider;

        public AdminController(ICqrsBus bus, IValidator<Product> validator, IProductGalleryProvider galleryProvider)
        {
            this.bus = bus;
            this.validator = validator;
            this.galleryProvider = galleryProvider;
        }

        [Route("admin")]
        public ActionResult Panel()
        {
            return View();
        }

        [Route("categories")]
        public JsonResult GetCategories()
        {
            var categories = bus.RunQuery<Category[]>(new GetAllCategoriesQuery());
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        [Route("addnewproduct")]
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
                return Json(new {Success = "True", responseText = "Dodano produkt.", Url = product.ProductUrl});
            }
            catch (Exception ex)
            {
                galleryProvider.DeleteProductGallery(productId);
                return
                    Json(new {Success = "False", responseText = "Wystąpił błąd. Spróbuj ponownie lub odśwież stronę."});
            }
        }
    }
}