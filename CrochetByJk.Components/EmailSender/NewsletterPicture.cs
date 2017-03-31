using System.Net.Mail;
using CrochetByJk.Components.ProductGalleryProvider;

namespace CrochetByJk.Components.EmailSender
{
    public class NewsletterPicture : IPicture
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public LinkedResource LinkedResource { get; set; }
    }
}