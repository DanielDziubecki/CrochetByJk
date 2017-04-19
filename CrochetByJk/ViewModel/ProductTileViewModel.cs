using System;
using CrochetByJk.Components.ProductGalleryProvider;

namespace CrochetByJk.ViewModel
{
    public class ProductTileViewModel : IPicture
    {
        public Guid IdProduct { get; set; }
        public string Name { get; set; }
        public string PictureUri { get; set; }
        public string ProductUrl { get; set; }
        public string Description { get; set; }
        public Guid IdCategory { get; set; }
        public DateTime InsertDate { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}