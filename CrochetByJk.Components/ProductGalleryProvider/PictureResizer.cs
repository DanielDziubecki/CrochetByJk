namespace CrochetByJk.Components.ProductGalleryProvider
{
    public class PictureResizer : IPictureResizer
    {
        private const int MaxHeight = 250;

        public void Resize(IPicture picture)
        {
            picture.Width = picture.Width * MaxHeight / picture.Height;
            picture.Height = MaxHeight;
        }
    }
}