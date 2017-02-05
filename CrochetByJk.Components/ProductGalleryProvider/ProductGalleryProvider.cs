using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using CrochetByJk.Common.Constants;
using CrochetByJk.Common.Utils;
using CrochetByJk.Components.Validators;
using CrochetByJk.Model.Model;

namespace CrochetByJk.Components.ProductGalleryProvider
{
    public class ProductGalleryProvider : IProductGalleryProvider
    {
        private readonly IValidator<IEnumerable<Picture>> validator;

        public ProductGalleryProvider(IValidator<IEnumerable<Picture>> validator)
        {
            this.validator = validator;
            if (!IsDirectoryExists(ServerRoot))
                CreateGalleriesRootDirectory();
        }

        private string ServerRoot { get; } = HttpContext.Current.Server.MapPath(WebSiteRoot);

        private static string WebSiteRoot { get; } = @"~\Content\ProductsGalleries";

        private bool IsDirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        private void CreateGalleryDirectory(string directoryId)
        {
            var galleryUri = Path.Combine(ServerRoot, directoryId);
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

        public IEnumerable<Picture> SaveProductGallery(Gallery gallery)
        {
            var files = gallery.Pictures;
            var pictures = new List<Picture>();
            var galleryId = gallery.GalleryId.ToString();
            var galleryUri = Path.Combine(ServerRoot, galleryId);

            if (!IsDirectoryExists(galleryUri))
                CreateGalleryDirectory(galleryId);

            foreach (string file in files)
            {
                var fileContent = files[file];
                if (fileContent != null && fileContent.ContentLength > 0)
                {
                    var stream = fileContent.InputStream;
                    var fileName = fileContent.FileName.RemoveSpecialCharacters()
                                                       .RemoveWhiteSpace();
                    var physicalPath = Path.Combine(galleryUri, fileName);
                    using (var fileStream = File.Create(physicalPath))
                    {
                        stream.CopyTo(fileStream);
                    }
                    var galleryUrl = Path.Combine(WebSiteRoot, galleryId);
                    var webPath = ConvertUriToUrl(Path.Combine(galleryUrl,fileName));
                    pictures.Add(new Picture
                    {
                        IdPicture = Guid.NewGuid(),
                        IdProduct = gallery.GalleryId,
                        Url = webPath,
                        Name = fileName,
                        IsMainPhoto = file == PicturesConstants.MainPhoto
                    });
                }
            }
            validator.Validate(pictures);
            return pictures;
        }
    }
}