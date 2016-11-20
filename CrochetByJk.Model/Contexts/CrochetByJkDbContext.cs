using System.Data.Entity;
using CrochetByJk.Model.Model;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CrochetByJk.Model.Contexts
{

    public class CrochetByJkDbContext : IdentityDbContext<ApplicationUser>
    {
        public CrochetByJkDbContext()
            : base("CrochetByJk")
        {
        }
    }
}
