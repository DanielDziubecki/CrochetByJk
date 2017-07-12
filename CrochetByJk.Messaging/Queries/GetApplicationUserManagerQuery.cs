using CrochetByJk.Messaging.Core;
using CrochetByJk.Model.Model;
using Microsoft.AspNet.Identity;

namespace CrochetByJk.Messaging.Queries
{
    public class GetApplicationUserManagerQuery : IQuery<UserManager<ApplicationUser>>
    {
    }
}
