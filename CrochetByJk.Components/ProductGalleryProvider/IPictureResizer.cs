﻿namespace CrochetByJk.Components.ProductGalleryProvider
{
    public interface IPictureResizer
    {
        void Resize(IPicture picture,bool isMobile =false);
    }
}