using CrochetByJk.Components.ProductGalleryProvider;
using CrochetByJk.ViewModel;
using FluentAssertions;
using Xunit;

namespace CrochetByJk.Tests.PictureResizerTests
{
    public class picture_resizer_tests
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
            pictureResizer.Resize(pictrue, false);
            pictrue.Width.Should().Be(700);
        }

        [Fact]
        public void resizer_returns_0_on_0_width()
        {
            var pictureResizer = new PictureResizer();
            var pictrue = new ProductTileViewModel()
            {
                Height = 200,
                Width = 0
            };
            pictureResizer.Resize(pictrue,false);
            pictrue.Width.Should().Be(0);
        }

        [Fact]
        public void resizer_returns_0_on_0_height()
        {
            var pictureResizer = new PictureResizer();
            var pictrue = new ProductTileViewModel()
            {
                Height = 0,
                Width = 150
            };
            pictureResizer.Resize(pictrue,false);
            pictrue.Height.Should().Be(0);
        }

        [Fact]
        public void resizer_returns_0_on_0_height_and_0_width()
        {
            var pictureResizer = new PictureResizer();
            var pictrue = new ProductTileViewModel()
            {
                Height = 0,
                Width = 0
            };
            pictureResizer.Resize(pictrue,false);
            pictrue.Width.Should().Be(0);
            pictrue.Height.Should().Be(0);
        }
    }
}