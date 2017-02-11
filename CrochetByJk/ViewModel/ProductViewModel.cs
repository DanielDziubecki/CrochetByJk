using System;
using System.Collections.Generic;

namespace CrochetByJk.ViewModel
{
    public class ProductViewModel
    {
        public Guid IdProduct { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> PictureUrls { get; set; }
    }
}