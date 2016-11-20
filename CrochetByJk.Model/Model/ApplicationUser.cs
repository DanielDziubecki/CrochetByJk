using Microsoft.AspNet.Identity.EntityFramework;

namespace CrochetByJk.Model.Model
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
            : base("CrochetByJk")
        {
        }
    }
}