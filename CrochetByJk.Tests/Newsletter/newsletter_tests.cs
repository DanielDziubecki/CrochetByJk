using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using CrochetByJk.Components.EmailSender;
using CrochetByJk.Components.ProductGalleryProvider;
using CrochetByJk.Model.Model;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture;
using Xunit;

namespace CrochetByJk.Tests.Newsletter
{
    public class newsletter_tests
    {
        [Fact]
        public void mail_message_should_contain_picture_source()
        {
            var pictureRes = Substitute.For<IPictureResizer>();
            var templateReader = Substitute.For<IMailTemplateReader>();

            var content = @"..\..\Newsletter\TestFiles\3.jpg";
            var resource = new LinkedResource(content);


            const string template = "<html><body><img id=\"newProductImage\" /><a id=\"cancelSubLink\" /><a id=\"goToProduct\"/></body></html>";
            templateReader.GetTemplate(MailTemplateType.Newsletter).Returns(x => template);

            var mailMessage = new NewsletterMessage
            {
                To = new []{"test1@wp.pl"},
                Body = "",
                From = "test2@wp.pl",
                Subject = "Subject",
                ProductUrl = "TestUri",
                NewsLetterPicture = new NewsletterPicture
                {
                    LinkedResource = resource
                },
                NewsletterClients = new[]
                {
                    new NewsletterClient()
                    {
                        Email = "TestEmail@wp.pl",
                        Id = "TestKlient",
                        InsertDate = DateTime.Now
                    },
                }
            };

            var messageFactory = new MailMessageFactory(pictureRes, templateReader);
            var messages = messageFactory.GetMessages(mailMessage);

            messages.First().Body.Should().Contain($"src=\"cid:{resource.ContentId}\"");
        }

        [Fact]
        public void mail_message_should_contain_cancel_subs_href()
        {
            var pictureRes = Substitute.For<IPictureResizer>();
            var templateReader = Substitute.For<IMailTemplateReader>();

            var content = @"..\..\Newsletter\TestFiles\3.jpg";
            var resource = new LinkedResource(content);

            const string template = "<html><body><img id=\"newProductImage\" /><a id=\"cancelSubLink\" /><a id=\"goToProduct\"/></body></html>";
            templateReader.GetTemplate(MailTemplateType.Newsletter).Returns(x => template);

            var mailMessage = new NewsletterMessage
            {
                To = new[] { "test1@wp.pl" },
                Body = "",
                From = "test2@wp.pl",
                Subject = "Subject",
                ProductUrl = "TestUri",
                NewsLetterPicture = new NewsletterPicture
                {
                    LinkedResource = resource
                },
                NewsletterClients = new[]
                {
                    new NewsletterClient()
                    {
                        Email = "TestEmail@wp.pl",
                        Id = "TestKlient",
                        InsertDate = DateTime.Now
                    }, 
                }
            };

            var messageFactory = new MailMessageFactory(pictureRes, templateReader);
            var messages = messageFactory.GetMessages(mailMessage);

            messages.First().Body.Should().Contain($"href=\"www.crochetbyjk.pl/newsletter/usun/{mailMessage.NewsletterClients.First().Id}\"");
        }

        [Fact]
        public void mail_message_should_contain_button_href()
        {
            var pictureRes = Substitute.For<IPictureResizer>();
            var templateReader = Substitute.For<IMailTemplateReader>();

            var content = @"..\..\Newsletter\TestFiles\3.jpg";
            var resource = new LinkedResource(content);

            const string template = "<html><body><img id=\"newProductImage\" /><a id=\"cancelSubLink\" /><a id=\"goToProduct\"/></body></html>";
            templateReader.GetTemplate(MailTemplateType.Newsletter).Returns(x => template);

            var mailMessage = new NewsletterMessage
            {
                To = new[] { "test1@wp.pl" },
                Body = "",
                From = "test2@wp.pl",
                Subject = "Subject",
                ProductUrl = "TestUri",
                NewsLetterPicture = new NewsletterPicture
                {
                    LinkedResource = resource
                },
                NewsletterClients = new[]
                {
                    new NewsletterClient()
                    {
                        Email = "TestEmail@wp.pl",
                        Id = "TestKlient",
                        InsertDate = DateTime.Now
                    },
                }
            };

            var messageFactory = new MailMessageFactory(pictureRes, templateReader);
            var messages = messageFactory.GetMessages(mailMessage);

            messages.First().Body.Should().Contain($"href=\"{mailMessage.ProductUrl}\"");
        }
    }
}