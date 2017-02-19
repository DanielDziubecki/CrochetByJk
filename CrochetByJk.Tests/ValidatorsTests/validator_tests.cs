using System.Collections.Generic;
using System.Linq;
using CrochetByJk.Components.Validators;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Messaging.Queries;
using CrochetByJk.Model.Model;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CrochetByJk.Tests.ValidatorsTests
{
    public class validator_tests
    {
        [Fact]
        public void picture_validator_should_change_picture_name_on_duplicate()
        {
            var duplicatedNames = new List<Picture>
            {
                new Picture {Name = "Name1"},
                new Picture {Name = "Name1"},
                new Picture {Name = "Name2"},
                new Picture {Name = "Name2"},
                new Picture {Name = "Name3"},
            };

            var pictureVal = new PictureValidator();
            pictureVal.Validate(duplicatedNames);

            var isAnyDuplicated = duplicatedNames
                                     .GroupBy(x => x.Name)
                                     .Where(x => x.Count() > 1)
                                     .ToList();

            isAnyDuplicated.Should().BeEmpty();
        }

        [Fact]
        public void product_validator_should_change_product_name_on_duplicate()
        {
            var duplicatedProduct = new Product {UrlFriendlyName = "Name1"};

            var cqrsBusMock = Substitute.For<ICqrsBus>();

            cqrsBusMock.RunQuery<IEnumerable<string>>(Arg.Any<GetAllProductNamesFromCategoryQuery>())
            .Returns(info => new List<string>
            {
                "Name1"
            });
            var pictureVal = new ProductValidator(cqrsBusMock);
            pictureVal.Validate(duplicatedProduct);

            duplicatedProduct.Name.Should().NotBe("Name1");
        }
    }
}
