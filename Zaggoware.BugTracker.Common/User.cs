using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Common
{
	public class User
	{
		public string Id { get; set; }

		public string UserName { get; set; }

		public string EmailAddress { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string FullName
		{
			get
			{
				return (FirstName + " " + LastName).Trim();
			}
		}

		public IEnumerable<Privilege> Privileges { get; set; }

		public bool HasPrivilege(string fullPrivilegeName)
		{
			if (this.Privileges == null || !this.Privileges.Any())
			{
				return false;
			}

			return
				this.Privileges.Any(
					p => (p.Subject + "_" + p.Name).Equals(fullPrivilegeName, StringComparison.InvariantCultureIgnoreCase));
		}

		public bool HasPrivilege(string subject, string privilegeName)
		{
			if (this.Privileges == null || !this.Privileges.Any())
			{
				return false;
			}

			return
				this.Privileges.Any(
					p =>
					p.Subject.Equals(subject, StringComparison.InvariantCultureIgnoreCase)
					&& p.Name.Equals(privilegeName, StringComparison.InvariantCultureIgnoreCase));
		}

		public bool HasPrivilege(Privilege privilege)
		{
			if (this.Privileges == null || !this.Privileges.Any())
			{
				return false;
			}

			return this.Privileges.Any(p => p.Equals(privilege));
		}
	}
}
