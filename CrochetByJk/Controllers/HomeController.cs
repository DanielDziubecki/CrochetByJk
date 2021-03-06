﻿using System.Web.Mvc;
using System.Web.UI;
using AutoMapper;
using CrochetByJk.Components.ProductGalleryProvider;
using CrochetByJk.ErrorHandlers;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Messaging.Queries;
using CrochetByJk.ViewModel;
using DevTrends.MvcDonutCaching;

namespace CrochetByJk.Controllers
{
    [AllowAnonymous]
    [NlogHandleError(View = "Error")]
  //  [DonutOutputCache(Duration = 60 * 600, Location = OutputCacheLocation.ServerAndClient)]
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
            var products = bus.RunQuery(new GetLastProductsQuery { Amount = 4 });
            var viewModel = mapper.Map<ProductTileViewModel[]>(products);
            foreach (var productTileViewModel in viewModel)
                pictureResizer.Resize(productTileViewModel, Request.Browser.IsMobileDevice);
            return View("Index" , viewModel);
        }

        [Route("kontakt")]
        public ActionResult Contact()
        {
            return View("Contact");
        }

        [Route("blad")]
        public ActionResult Error()
        {
            return View("Error");
        }
    }
}