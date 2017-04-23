using System;
using System.Collections.Generic;
using CrochetByJk.Model.Model;

namespace CrochetByJk.Components.ProductGalleryProvider
{
    public interface IProductGalleryProvider
    {
        IEnumerable<Picture> SaveProductGallery(Gallery gallery);
        void MarkAsToDeleteGallery(Guid productId);
    }
}