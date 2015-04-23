using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Data.Entities
{
	public class UserGroup
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public virtual ICollection<User> Members { get; set; }

		public virtual ICollection<Privilege> Privileges { get; set; }
	}
}
