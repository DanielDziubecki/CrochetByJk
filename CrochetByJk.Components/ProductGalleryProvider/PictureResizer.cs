namespace CrochetByJk.Components.ProductGalleryProvider
{
    public class PictureResizer : IPictureResizer
    {
        public void Resize(IPicture picture, bool isMobile)
        {
            var maxHeight = isMobile ? 200 : 350;
            if (picture.Height != 0 && picture.Width != 0)
            {
                picture.Width = picture.Width*maxHeight/picture.Height;
                picture.Height = maxHeight;
            }
        }
    }
}