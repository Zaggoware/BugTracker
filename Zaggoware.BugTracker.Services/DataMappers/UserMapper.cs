using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Services.DataMappers
{
	using Zaggoware.BugTracker.Common;
	using Zaggoware.BugTracker.Common.Paging;

    internal static class UserMapper
	{
		public static User Map(this Data.Entities.User user)
		{
		    if (user == null)
		    {
		        return null;
		    }

			return new User
				       {
						   Id = user.Id,
						   UserName = user.LastName,
						   EmailAddress = user.Email,
						   FirstName = user.FirstName,
						   LastName = user.LastName
				       };
		}

        public static IPagedList<User> Map(this IPagedList<Data.Entities.User> source)
        {
            return source != null
                       ? new PagedList<User>(source.Select(Map), source.Page, source.ItemsPerPage)
                       : new PagedList<User>(Enumerable.Empty<User>(), 1, PagedList.DefaultItemsPerPage);
        }
	}
}
