using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrochetByJk.Model.Model
{
    [Table("Picture")]
    public class Picture
    {
        [Key]
        public Guid IdPicture { get; set; }
        public string Uri { get; set; }
        public string Name { get; set; }
        public bool IsMainPhoto { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Guid IdProduct { get; set; }
        [ForeignKey("IdProduct")]
        public Product Product { get; set; }
    }
}