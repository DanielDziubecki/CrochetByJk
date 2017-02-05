using System;
using CrochetByJk.Messaging.Core;

namespace CrochetByJk.Messaging.Queries
{
    public class GetAllProductNamesFromCategoryQuery : IQuery
    {
        public Guid CategoryId { get; set; }
    }
}