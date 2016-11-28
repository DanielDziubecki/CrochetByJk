using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrochetByJk.Model.Model
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public Guid IdCategory { get; set; }   
        public string Name { get; set; }   
    }
}