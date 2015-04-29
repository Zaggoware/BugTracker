using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Zaggoware.BugTracker.Services
{
	using Zaggoware.BugTracker.Common;
	using Zaggoware.BugTracker.Common.Paging;

    public interface IUserService : IDisposable
	{
	    User CreateUser(
	        string userName,
	        string password,
	        string emailAddress,
	        string firstName = null,
	        string lastName = null);

        IPagedList<User> GetUsers(int page, int itemsPerPage = PagedList.DefaultItemsPerPage);

		User GetUserById(string id);

		User GetUserByUserName(string userName);

		User GetUserByEmailAddress(string emailAddress);

		bool ValidatePassword(string userName, string password);

        ClaimsIdentity GenerateUserIdentity(string userId);
	}
}
