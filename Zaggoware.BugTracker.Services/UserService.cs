using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Services
{
    using Microsoft.AspNet.Identity;

    using Zaggoware.BugTracker.Common;
    using Zaggoware.BugTracker.Common.Paging;
	using Zaggoware.BugTracker.Data;

    public class UserService : UserManager<Data.Entities.User>, IUserService
	{
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

		public string HashPassword(string password)
		{
			// TODO: Use Crypto

			return password;
		}

		public bool ValidatePassword(string userName, string password)
		{
			var user =
				this.context.Users.SingleOrDefault(u => u.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase));

			if (!string.IsNullOrEmpty(password) && user != null)
			{
				password = this.HashPassword(password);

				return user.PasswordHash == password;
			}

			// Prevent timing attacks
			this.HashPassword("__DUMMYPASSWORD__");

			return false;
		}
	}
}
