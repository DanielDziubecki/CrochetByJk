using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Hosting;
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

        private string ServerRoot { get; } = HostingEnvironment.MapPath(WebSiteRoot);

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
                    using (var stream = fileContent.InputStream)
                    {
                        
                        var fileName = fileContent.FileName.RemoveSpecialCharacters()
                            .RemoveWhiteSpace();
                        var physicalPath = Path.Combine(galleryUri, fileName);
                        Image img;
                        using (img = Image.FromStream(stream))
                        {
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
                            img.Save(physicalPath, ImageFormat.Jpeg);
                        }
                        stream.Close();
                    }
                }
            }
            validator.Validate(pictures);
            return pictures;
        }

        public void ClearProductGallery(Guid productId)
        {
            var galleryUri = Path.Combine(ServerRoot, productId.ToString());
            if (!Directory.Exists(galleryUri)) return;
            var newPath = galleryUri+"_d";
            var copyNumber = 0;

            while (Directory.Exists(newPath))
            {
                copyNumber++;
                newPath += $"({copyNumber})";
            }
            Directory.Move(galleryUri, newPath);
        }
    }
}