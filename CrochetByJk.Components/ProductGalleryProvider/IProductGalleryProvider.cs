using System;
using System.Collections.Generic;
using CrochetByJk.Model.Model;

namespace CrochetByJk.Components.ProductGalleryProvider
{
    public interface IProductGalleryProvider
    {
        void DeleteProductGallery(Guid productId);
        IEnumerable<Picture> SaveProductGallery(Gallery gallery);
    }
}