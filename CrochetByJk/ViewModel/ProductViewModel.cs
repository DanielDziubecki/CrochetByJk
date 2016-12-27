using System;

namespace CrochetByJk.ViewModel
{
    public class ProductViewModel
    {
        public Guid IdProduct { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUri { get; set; }
    }
}