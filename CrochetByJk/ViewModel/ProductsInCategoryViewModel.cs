using System.Collections.Generic;

namespace CrochetByJk.ViewModel
{
    public class ProductsInCategoryViewModel
    {
        public IEnumerable<ProductTileViewModel> Products { get; set; }
        public string CategoryName { get; set; }
    }
}