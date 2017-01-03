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
            if (!IsDirectoryExists(ServerRoot))
                CreateGalleriesRootDirectory();
        }

        private string ServerRoot { get; } = HttpContext.Current.Server.MapPath(WebSiteRoot);

        private static string WebSiteRoot { get; } = @"~\Content\ProductsGalleries";

        private bool IsDirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        private void CreateGalleryDirectory(Guid productId)
        {
            var galleryUri = Path.Combine(ServerRoot, productId.ToString());
            if (!IsDirectoryExists(galleryUri))
                Directory.CreateDirectory(galleryUri);
        }

        private void CreateGalleriesRootDirectory()
        {
            Directory.CreateDirectory(ServerRoot);
        }

        private string ConvertUriToUrl(string uri)
        {
            return uri.Replace("\\", "/").Replace("~","");
        }

        public void DeleteProductGallery(Guid productId)
        {
            var galleryUri = Path.Combine(ServerRoot, productId.ToString());
            if (Directory.Exists(galleryUri))
                Directory.Delete(galleryUri);
        }

        public IEnumerable<Picture> SaveProductPictures(Guid productId, object requestFiles)
        {
            var files = (HttpFileCollectionBase) requestFiles;
            var pictures = new List<Picture>();
            var galleryUri = Path.Combine(ServerRoot, productId.ToString());
            if (!IsDirectoryExists(galleryUri))
                CreateGalleryDirectory(productId);

            foreach (string file in files)
            {
                var fileContent = files[file];
                if (fileContent != null && fileContent.ContentLength > 0)
                {
                    var stream = fileContent.InputStream;
                    var fileName = fileContent.FileName;
                    var physicalPath = Path.Combine(galleryUri, fileName);
                    using (var fileStream = File.Create(physicalPath))
                    {
                        stream.CopyTo(fileStream);
                    }
                    var galleryUrl = Path.Combine(WebSiteRoot, productId.ToString());
                    var webPath = ConvertUriToUrl(Path.Combine(galleryUrl,fileName));
                    pictures.Add(new Picture
                    {
                        IdPicture = Guid.NewGuid(),
                        IdProduct = productId,
                        Url = webPath,
                        Name = fileName
                    });
                }
            }
            return pictures;
        }
    }
}