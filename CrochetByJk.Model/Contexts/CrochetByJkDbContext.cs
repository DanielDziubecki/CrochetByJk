using System.Data.Entity;
using CrochetByJk.Model.Model;

namespace CrochetByJk.Model.Contexts
{
    public class CrochetByJkDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Picture> Pictures { get; set; }

        public CrochetByJkDbContext():base("CrochetByJk")
        {
            
        }
    }
}
