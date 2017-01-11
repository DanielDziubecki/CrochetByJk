using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrochetByJk.Model.Model
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public Guid IdProduct { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductUrl { get; set; }
        public DateTime InsertDate { get; set; }

        public Guid IdMainPicture { get; set; }
        public ICollection<Picture> ProductGallery { get; set; }

        public Guid IdCategory { get; set; }
        public Category Category { get; set; }
    }
}