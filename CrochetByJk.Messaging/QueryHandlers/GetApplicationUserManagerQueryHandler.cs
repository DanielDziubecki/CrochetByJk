using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Messaging.Queries;
using CrochetByJk.Model.Contexts;
using CrochetByJk.Model.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CrochetByJk.Messaging.QueryHandlers
{
    public class GetApplicationUserManagerQueryHandler :
        IQueryHandler<GetApplicationUserManagerQuery, UserManager<ApplicationUser>>
    {
        public Task<UserManager<ApplicationUser>> HandleAsync(GetApplicationUserManagerQuery query)
        {
            var usermanager = new UserManager<ApplicationUser>(
                              new UserStore<ApplicationUser>(
                              new CrochetByJkDbContext()));
            usermanager.UserValidator = new UserValidator<ApplicationUser>(usermanager)
            {
                AllowOnlyAlphanumericUserNames = false
            };
            return Task.FromResult(usermanager);
        }
        public void Dispose()
        {
        }
    }
}