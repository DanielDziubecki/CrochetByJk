using System;
using System.Web;

namespace CrochetByJk.Dto
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public HttpPostedFileWrapper MainPhoto { get; set; }
        public Guid IdCategory { get; set; }
        public string CategoryName { get; set; }
    }
}