using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Services
{
    using System.Security.Claims;

    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;

    using Zaggoware.BugTracker.Data.Entities;

    public class InternalSignInManager : SignInManager<User, string>
    {
        public InternalSignInManager(UserService userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return user.GenerateUserIdentityAsync((UserService)this.UserManager);
        }
    }
}
