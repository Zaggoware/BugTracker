using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Services
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    using Zaggoware.BugTracker.Common;
    using Zaggoware.BugTracker.Common.Paging;
	using Zaggoware.BugTracker.Data;

    public class UserService : UserManager<Data.Entities.User>, IUserService
	{
        public override bool SupportsUserPassword
        {
            get
            {
                return true;
            }
        }

		private readonly IDbContext context;

        public UserService(
            IUserStore<Data.Entities.User> userStore,
            IDbContext context)
            : base(userStore)
		{
			this.context = context;
		}

	    public User CreateUser(
	        string userName,
	        string password,
	        string emailAddress,
	        string firstName = null,
	        string lastName = null)
        {
	        var user = this.context.Users.Create();
	        user.UserName = userName;
	        user.Email = emailAddress;
	        user.FirstName = firstName;
	        user.LastName = lastName;

	        this.Create(user, password);

            context.Users.Add(user);
            context.SaveChanges();

	        return user.Map();
        }

        public IPagedList<User> GetUsers(int page, int itemsPerPage = PagedList.DefaultItemsPerPage)
        {
            return this.context.Users.ToPagedList(page, itemsPerPage).Map();
        }

		public User GetUserById(string id)
		{
		    return this.context.Users.SingleOrDefault(u => u.Id == id).Map();
		}

		public User GetUserByUserName(string userName)
		{
		    return
		        this.context.Users.SingleOrDefault(
		            u => u.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase)).Map();
		}

		public User GetUserByEmailAddress(string emailAddress)
		{
		    return
		        this.context.Users.SingleOrDefault(
		            u => u.Email.Equals(emailAddress, StringComparison.InvariantCultureIgnoreCase)).Map();
		}

        public ClaimsIdentity GenerateUserIdentity(string userId)
        {
            var user = this.context.Users.SingleOrDefault(u => u.Id == userId);

            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = this.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

            // TODO: Add custom user claims here

            return userIdentity;
        }

        public void UpdateUser(string userId, string email, string firstName, string lastName)
        {
            var user = this.context.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return;
            }

            user.Email = email;
            user.FirstName = firstName;
            user.LastName = lastName;

            this.context.SaveChanges();
        }

        public bool UpdatePassword(string userId, string currentPassword, string newPassword)
        {
            var user = this.context.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return false;
            }

            return this.ChangePassword(userId, currentPassword, newPassword).Succeeded;
        }
	}
}
