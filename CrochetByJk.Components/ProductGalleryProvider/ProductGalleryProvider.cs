using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
            return uri.Replace("\\", "/").Replace("~", "");
        }

        public void DeleteProductGallery(Guid productId)
        {
            if (!IsDirectoryExists(ServerRoot))
                CreateGalleriesRootDirectory();
            var galleryUri = Path.Combine(ServerRoot, productId.ToString());
            if (!Directory.Exists(galleryUri)) return;
            var files = Directory.GetFiles(galleryUri);
            foreach (var file in files)
                File.Delete(file);

            Directory.Delete(galleryUri);
        }

        public IEnumerable<Picture> SaveProductGallery(Gallery gallery)
        {
            if (!IsDirectoryExists(ServerRoot))
                CreateGalleriesRootDirectory();
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
                    var img = Image.FromStream(stream);
                    img.Save(physicalPath, ImageFormat.Jpeg);

                    var galleryUrl = Path.Combine(WebSiteRoot, galleryId);
                    var webPath = ConvertUriToUrl(Path.Combine(galleryUrl, fileName));
                    pictures.Add(new Picture
                    {
                        IdPicture = Guid.NewGuid(),
                        IdProduct = gallery.GalleryId,
                        Uri = webPath,
                        Name = fileName,
                        Height = img.Height,
                        Width = img.Width,
                        IsMainPhoto = file == PicturesConstants.MainPhoto
                    });
                }
            }
            validator.Validate(pictures);
            return pictures;
        }
    }
}