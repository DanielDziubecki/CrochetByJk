using System;
using System.Linq;
using System.Web.Mvc;
using CrochetByJk.Common.Roles;
using CrochetByJk.Common.ShortGuid;
using CrochetByJk.Components;
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
        private readonly IProductGalleryProvider galleryProvider;

        public AdminController(ICqrsBus bus, IProductGalleryProvider galleryProvider)
        {
            this.bus = bus;
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
                return Json(new {Success = "False", responseText = "Wprowadź poprawne dane."});
            
            var productId = Guid.NewGuid();
            try
            {
                var pictures = galleryProvider.SaveProductPictures(productId, Request.Files);
                var enumerable = pictures as Picture[] ?? pictures.ToArray();
                var mainPicture = enumerable.Single(x => x.Name == productDto.MainPhoto.FileName);
                mainPicture.IsMainPhoto = true;
                var product = new Product
                {
                    IdProduct = productId,
                    Name = productDto.Name,
                    IdCategory = productDto.IdCategory,
                    Description = productDto.Description,
                    ProductUrl = $"Produkty/{productDto.CategoryName}/{new ShortGuid(productId)}",
                    IdMainPicture = mainPicture.IdPicture,
                    InsertDate = DateTime.Now,
                    ProductGallery = enumerable.ToArray()
                };
                bus.ExecuteCommand(new SaveProductCommand(product));
                return Json(new { Success = "True", responseText = "Dodano produkt.", Url = product.ProductUrl });
            }
            catch (Exception ex)
            {
                galleryProvider.DeleteProductGallery(productId);
                return Json(new {Success = "False", responseText = "Wystąpił błąd."});
            }
           
        }
    }
}