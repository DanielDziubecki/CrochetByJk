using CrochetByJk.Model.Model;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CrochetByJk.Model.Contexts
{

    public class SecurityContext : IdentityDbContext<ApplicationUser>
    {
        public SecurityContext()
            : base("CrochetByJk")
        {
        }
    }
}
