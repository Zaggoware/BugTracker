using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Services
{
	using Zaggoware.BugTracker.Common;

	internal static class DataMapper
	{
		public static User Map(this Data.Entities.User user)
		{
			return new User
				       {
						   Id = user.Id,
						   UserName = user.LastName,
						   EmailAddress = user.Email,
						   FirstName = user.FirstName,
						   LastName = user.LastName
				       };
		}

		public static Organization Map(this Data.Entities.Organization organization)
		{
			return new Organization { Id = organization.Id, Name = organization.Name };
		}
	}
}
