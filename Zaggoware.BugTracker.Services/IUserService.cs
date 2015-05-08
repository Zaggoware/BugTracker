using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Zaggoware.BugTracker.Services
{
    using Microsoft.AspNet.Identity.Owin;

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

        ClaimsIdentity GenerateUserIdentity(string userId);

        void UpdateUser(string userId, string email, string firstName, string lastName);

        bool UpdatePassword(string userId, string currentPassword, string newPassword);
	}
}
