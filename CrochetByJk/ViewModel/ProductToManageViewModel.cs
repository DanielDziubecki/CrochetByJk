using System;

namespace CrochetByJk.ViewModel
{
    public class ProductToManageViewModel
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string MainPictureUri { get; set; }
        public DateTime InsertDate { get; set; }
    }
}