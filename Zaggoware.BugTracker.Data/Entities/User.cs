using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Data.Entities
{
	using System.Collections.ObjectModel;

	using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
	{
		public User()
		{
			this.UserGroups = new Collection<UserGroup>();
			this.CreationDate = DateTime.Now;
			this.ModificationDate = DateTime.Now;
		}

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public virtual ICollection<UserGroup> UserGroups { get; set; }

		public DateTime CreationDate { get; set; }

		public DateTime ModificationDate { get; set; }
	}
}
