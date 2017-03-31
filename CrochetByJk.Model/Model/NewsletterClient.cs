using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrochetByJk.Model.Model
{
    [Table("NewsletterClient")]
    public class NewsletterClient
    {
        [Key]
        public string Id { get; set; }
        public string Email { get; set; }
        public DateTime InsertDate { get; set; }
    }
}