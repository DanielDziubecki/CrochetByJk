using System;
using System.Web;

namespace CrochetByJk.Model.Model
{
    public class Gallery
    {
        public Gallery(Guid galleryId, HttpFileCollectionBase pictures, HttpPostedFileBase mainPicture)
        {
            GalleryId = galleryId;
            Pictures = pictures;
            MainPicture = mainPicture;
        }
        public Guid GalleryId { get; private set; }
        public HttpFileCollectionBase Pictures { get; private set; }
        public HttpPostedFileBase MainPicture { get; private set; }
    }
}