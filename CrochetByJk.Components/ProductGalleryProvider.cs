using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using CrochetByJk.Model.Model;

namespace CrochetByJk.Components
{
    public class ProductGalleryProvider : IProductGalleryProvider
    {
        public ProductGalleryProvider()
        {
            if (!IsDirectoryExists(RootDirectory))
                CreateGalleriesRootDirectory();
        }

        public string RootDirectory { get; } = HttpContext.Current.Server.MapPath(@"~\Content\ProductsGalleries");

        public bool IsDirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public void CreateGalleryDirectory(Guid productId)
        {
            var galleryUri = Path.Combine(RootDirectory, productId.ToString());
            if (!IsDirectoryExists(galleryUri))
                Directory.CreateDirectory(galleryUri);
        }

        public void CreateGalleriesRootDirectory()
        {
            Directory.CreateDirectory(RootDirectory);
        }

        public void DeleteProductGallery(Guid productId)
        {
            var galleryUri = Path.Combine(RootDirectory, productId.ToString());
            if(Directory.Exists(galleryUri))
                Directory.Delete(galleryUri);
        }

        public IEnumerable<Picture> SaveProductPictures(Guid productId, object requestFiles)
        {
            var files = (HttpFileCollectionBase) requestFiles;
            var pictures = new List<Picture>();
            var galleryUri = Path.Combine(RootDirectory, productId.ToString());
            if (!IsDirectoryExists(galleryUri))
                CreateGalleryDirectory(productId);

            foreach (string file in files)
            {
                var fileContent = files[file];
                if (fileContent != null && fileContent.ContentLength > 0)
                {
                    var stream = fileContent.InputStream;
                    var fileName = fileContent.FileName;
                    var path = Path.Combine(galleryUri, fileName);
                    using (var fileStream = File.Create(path))
                    {
                        stream.CopyTo(fileStream);
                    }
                    pictures.Add(new Picture
                    {
                        IdPicture = Guid.NewGuid(),
                        IdProduct = productId,
                        Uri = path,
                        Name = fileName
                    });
                }
            }
            return pictures;
        }
    }
}