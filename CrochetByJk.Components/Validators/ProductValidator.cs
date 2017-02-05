using System;
using System.Collections.Generic;
using System.Linq;
using CrochetByJk.Common.ShortGuid;
using CrochetByJk.Common.Utils;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Messaging.Queries;
using CrochetByJk.Model.Model;

namespace CrochetByJk.Components.Validators
{
    public class ProductValidator : IValidator<Product> 
    {
        private readonly ICqrsBus bus;

        public ProductValidator(ICqrsBus bus)
        {
            this.bus = bus;
        }

        public void Validate(Product objectToValidate)
        {
            var productNames = bus.RunQuery<IEnumerable<string>>(new GetAllProductNamesFromCategoryQuery {CategoryId = objectToValidate.IdCategory});
            var name = objectToValidate.Name
                                       .RemoveWhiteSpace()
                                       .RemoveSpecialCharacters();
            var isNameExists = productNames.Any(x => string.Equals(x.Trim(), name.Trim(), StringComparison.InvariantCultureIgnoreCase));

            if (isNameExists)
                objectToValidate.Name = name + ShortGuid.NewGuid();
        }
    }
}