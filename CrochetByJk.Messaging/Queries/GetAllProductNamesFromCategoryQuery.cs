using System;
using CrochetByJk.Messaging.Core;
using System.Collections.Generic;

namespace CrochetByJk.Messaging.Queries
{
    public class GetAllProductNamesFromCategoryQuery : IQuery<IEnumerable<string>>
    {
        public Guid CategoryId { get; set; }
    }
}