using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Services
{
	using Zaggoware.BugTracker.Common;
	using Zaggoware.BugTracker.Data;

	public class UserService : IUserService
	{
		private readonly IDbContext context;

		public UserService(IDbContext context)
		{
			this.context = context;
		}

	    public User CreateUser(string userName, string password, string emailAddress, string firstName = null, string lastName = null)
	    {
		    var user = new Data.Entities.User
			               {
				               UserName = userName,
				               EmailAddress = emailAddress,
				               FirstName = firstName,
				               LastName = lastName
			               };

		    this.context.Users.Add(user);
		    this.context.SaveChanges();

		    return user.Map();
	    }

		public User GetUserById(int id)
		{
			return this.GetMappedUser(u => u.Id == id);
		}

		public User GetUserByUserName(string userName)
		{
			return this.GetMappedUser(u => u.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase));
		}

		public User GetUserByEmailAddress(string emailAddress)
		{
			return this.GetMappedUser(u => u.EmailAddress.Equals(emailAddress, StringComparison.InvariantCultureIgnoreCase));
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

				return user.HashedPassword == password;
			}

			// Prevent timing attacks
			this.HashPassword("__DUMMYPASSWORD__");

			return false;
		}

		private User GetMappedUser(Func<Data.Entities.User, bool> predicate)
		{
			var user = this.context.Users.SingleOrDefault(predicate);

			if (user == null)
			{
				return null;
			}

			return user.Map();
		}
	}
}
