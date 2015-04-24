using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Services
{
	using Zaggoware.BugTracker.Common;

	public interface IUserService : IDisposable
	{
		User CreateUser(
			string userName,
			string password,
			string emailAddress,
			string firstName = null,
			string lastName = null);

		User GetUserById(int id);

		User GetUserByUserName(string userName);

		User GetUserByEmailAddress(string emailAddress);

		string HashPassword(string password);

		bool ValidatePassword(string userName, string password);
	}
}
