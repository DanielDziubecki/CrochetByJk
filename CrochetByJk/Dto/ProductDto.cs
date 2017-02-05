using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace CrochetByJk.Dto
{
    public class ProductDto
    {
        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        [Required]
        public HttpPostedFileWrapper MainPhoto { get; set; }

        [Required]
        public Guid IdCategory { get; set; }

        [Required]
        public string CategoryName { get; set; }
    }
}