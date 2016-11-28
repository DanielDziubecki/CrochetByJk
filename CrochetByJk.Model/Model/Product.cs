using System;
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
        public string Price { get; set; }
        public string WorkTime { get; set; }
        public string GalleryUri { get; set; }
        public string MainPhotoUri { get; set; }
        public Guid IdCategory { get; set; }
        public Category Category { get; set; }
    }
}
