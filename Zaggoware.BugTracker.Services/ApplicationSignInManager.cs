using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Services
{
    using System.Security.Claims;

    using Microsoft.AspNet.Identity.Owin;

    using Zaggoware.BugTracker.Data.Entities;

    public class ApplicationSignInManager
    {
        private readonly InternalSignInManager signInManager;

        public ApplicationSignInManager(InternalSignInManager signInManager)
        {
            this.signInManager = signInManager;
        }

        public SignInStatus SignIn(string userName, string password, bool isPersistent)
        {
            return this.signInManager.PasswordSignIn(userName, password, isPersistent, false);
        }

        public async Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return await this.signInManager.CreateUserIdentityAsync(user);
        }
    }
}
