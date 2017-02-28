using CrochetByJk.Components.ProductGalleryProvider;
using CrochetByJk.ViewModel;
using FluentAssertions;
using Xunit;

namespace CrochetByJk.Tests.PictureResizerTests
{
    public class product_resizer_tests
    {
        [Fact]
        public void resizer_resize_picture_correctly()
        {
            var pictureResizer = new PictureResizer();
            var pictrue = new ProductTileViewModel()
            {
                Height = 500,
                Width = 1000
            };
            pictureResizer.Resize(pictrue);
            pictrue.Width.Should().Be(800);
        }
    }
}