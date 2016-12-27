using System;
using System.Collections.Generic;
using CrochetByJk.Model.Model;

namespace CrochetByJk.Components
{
    public interface IProductGalleryProvider
    {
        string RootDirectory { get; }
        void CreateGalleryDirectory(Guid productId);
        void CreateGalleriesRootDirectory();
        void DeleteProductGallery(Guid productId);
        IEnumerable<Picture> SaveProductPictures(Guid productId, object requestPictures);
    }
}