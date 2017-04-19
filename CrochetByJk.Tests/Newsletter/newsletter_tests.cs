using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using CrochetByJk.Common.ShortGuid;
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

            messages.First().Body.Should().Contain($"href=\"https://www.crochetbyjk.pl/newsletter/potwierdz/{mailMessage.NewsletterClients.First().Id}\"");
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

            var productId = Guid.NewGuid();
            var mailMessage = new NewsletterMessage
            {
                To = new[] { "test1@wp.pl" },
                Body = "",
                From = "test2@wp.pl",
                Subject = "Subject",
                ProductUrl = "TestUri",
                ProductId = productId,
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

            messages.First().Body.Should().Contain($"<a id=\"goToProduct\" href=\"www.crochetbyjk.pl/newsletter/{ShortGuid.Encode(productId)}\"");
        }

        [Fact]
        public void each_message_should_have_only_one_target_client()
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
                    new NewsletterClient
                    {
                        Email = "TestEmail@wp.pl",
                        Id = "TestKlient1",
                        InsertDate = DateTime.Now
                    },
                       new NewsletterClient
                    {
                        Email = "TestEmail@wp.pl",
                        Id = "TestKlient2",
                        InsertDate = DateTime.Now
                    },
                          new NewsletterClient
                    {
                        Email = "TestEmail@wp.pl",
                        Id = "TestKlient3",
                        InsertDate = DateTime.Now
                    },
                     new NewsletterClient
                    {
                        Email = "TestEmail@wp.pl",
                        Id = "TestKlient4",
                        InsertDate = DateTime.Now
                    },
                }
            };

            var messageFactory = new MailMessageFactory(pictureRes, templateReader);
            var messages = messageFactory.GetMessages(mailMessage);

            foreach (var message in messages)
                message.To.Count.Should().Be(1);
            
        }


        [Fact]
        public void every_client_should_have_his_own_cancel_sub_link()
        {
            var pictureRes = Substitute.For<IPictureResizer>();
            var templateReader = Substitute.For<IMailTemplateReader>();

            var content = @"..\..\Newsletter\TestFiles\3.jpg";
            var resource = new LinkedResource(content);

            const string template = "<html><body><img id=\"newProductImage\" /><a id=\"cancelSubLink\" /><a id=\"goToProduct\"/></body></html>";
            templateReader.GetTemplate(MailTemplateType.Newsletter).Returns(x => template);

            var newsClients = new[]
            {
                new NewsletterClient
                {
                    Email = "TestEmail1@wp.pl",
                    Id = "TestKlient1",
                    InsertDate = DateTime.Now
                },
                new NewsletterClient
                {
                    Email = "TestEmail2@wp.pl",
                    Id = "TestKlient2",
                    InsertDate = DateTime.Now
                },
                new NewsletterClient
                {
                    Email = "TestEmail3@wp.pl",
                    Id = "TestKlient3",
                    InsertDate = DateTime.Now
                },
                new NewsletterClient
                {
                    Email = "TestEmail4@wp.pl",
                    Id = "TestKlient4",
                    InsertDate = DateTime.Now
                },
                new NewsletterClient
                {
                    Email = "TestEmail5@wp.pl",
                    Id = "TestKlient5",
                    InsertDate = DateTime.Now
                },
            };
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
                NewsletterClients = newsClients
            };

            var messageFactory = new MailMessageFactory(pictureRes, templateReader);
            var messages = messageFactory.GetMessages(mailMessage);

            foreach (var message in messages)
            {
                var client = newsClients.Single(x => x.Email == message.To.Single().Address);
                message.Body.Should()
                    .Contain($"href=\"https://www.crochetbyjk.pl/newsletter/potwierdz/{client.Id}\"");

            }
        }
    }
}