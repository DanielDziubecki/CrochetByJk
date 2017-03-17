using System;
using System.Collections.Generic;

namespace CrochetByJk.ViewModel
{
    public class ProductWithSeeAlsoProductsViewModel
    {
        public Guid IdProduct { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> PictureUrls { get; set; }
        public IEnumerable<ProductTileViewModel> SeeAlsoProducts { get; set; }
    }
}