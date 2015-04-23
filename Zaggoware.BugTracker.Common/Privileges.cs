using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Common
{
	public class Privilege
	{
		private static readonly Dictionary<string, Privilege> Cache = new Dictionary<string, Privilege>();

		public string Subject { get; set; }

		public string Name { get; set; }

		internal static Privilege Get(string subject, string name)
		{
			if (string.IsNullOrEmpty(subject))
			{
				throw new ArgumentException("Subject cannot be empty.", "subject");
			}

			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("Name cannot be empty", "name");
			}

			if (!subject.EndsWith("_"))
			{
				subject = subject + "_";
			}

			var fullName = subject + name;

			if (Cache.ContainsKey(fullName))
			{
				return Cache[fullName];
			}

			var privilege = new Privilege { Subject = subject, Name = name };
			Cache.Add(fullName, privilege);

			return privilege;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Privilege))
			{
				return false;
			}

			var other = (Privilege)obj;

			return other.Subject.Equals(this.Subject, StringComparison.InvariantCultureIgnoreCase)
			       && other.Name.Equals(this.Name, StringComparison.InvariantCultureIgnoreCase);
		}
	}

	public static class OrganizationPrivileges
	{
		public const string Subject = "Organization";

		public static Privilege Create 
		{
			get
			{
				return Privilege.Get(Subject, "Create");
			}
		}

		public static Privilege Modify
		{
			get
			{
				return Privilege.Get(Subject, "Modify");
			}
		}

		public static Privilege AddUsersToAllGroups
		{
			get
			{
				return Privilege.Get(Subject, "AddUsersToAllGroups");
			}
		}

		public static Privilege AddUsersFromAllGroups
		{
			get
			{
				return Privilege.Get(Subject, "AddUsersFromAllGroups");
			}
		}

		public static Privilege AddUsersToOwnGroup
		{
			get
			{
				return Privilege.Get(Subject, "AddUsersToOwnGroup");
			}
		}

		public static Privilege RemoveUsersFromOwnGroup
		{
			get
			{
				return Privilege.Get(Subject, "RemoveUsersFromOwnGroup");
			}
		}

		public static Privilege AddUsersToLowerGroups
		{
			get
			{
				return Privilege.Get(Subject, "AddUsersToLowerGroups");
			}
		}

		public static Privilege RemoveUsersFromLowerGroups
		{
			get
			{
				return Privilege.Get(Subject, "RemoveUsersFromLowerGroups");
			}
		}

		public static Privilege ModifyAllGroups
		{
			get
			{
				return Privilege.Get(Subject, "ModifyAllGroups");
			}
		}

		public static Privilege ModifyOwnGroup
		{
			get
			{
				return Privilege.Get(Subject, "ModifyOwnGroup");
			}
		}

		public static Privilege ModifyLowerGroups
		{
			get
			{
				return Privilege.Get(Subject, "ModifyLowerGroups");
			}
		}
	}
	
	public static class ProjectPrivileges
	{
		public const string Subject = "Project";

		public static Privilege Create 
		{
			get
			{
				return Privilege.Get(Subject, "Create");
			}
		}

		public static Privilege Modify
		{
			get
			{
				return Privilege.Get(Subject, "Modify");
			}
		}

		public static Privilege AddUsersToAllGroups
		{
			get
			{
				return Privilege.Get(Subject, "AddUsersToAllGroups");
			}
		}

		public static Privilege AddUsersFromAllGroups
		{
			get
			{
				return Privilege.Get(Subject, "AddUsersFromAllGroups");
			}
		}

		public static Privilege AddUsersToOwnGroup
		{
			get
			{
				return Privilege.Get(Subject, "AddUsersToOwnGroup");
			}
		}

		public static Privilege RemoveUsersFromOwnGroup
		{
			get
			{
				return Privilege.Get(Subject, "RemoveUsersFromOwnGroup");
			}
		}

		public static Privilege AddUsersToLowerGroups
		{
			get
			{
				return Privilege.Get(Subject, "AddUsersToLowerGroups");
			}
		}

		public static Privilege RemoveUsersFromLowerGroups
		{
			get
			{
				return Privilege.Get(Subject, "RemoveUsersFromLowerGroups");
			}
		}

		public static Privilege ModifyAllGroups
		{
			get
			{
				return Privilege.Get(Subject, "ModifyAllGroups");
			}
		}

		public static Privilege ModifyOwnGroup
		{
			get
			{
				return Privilege.Get(Subject, "ModifyOwnGroup");
			}
		}

		public static Privilege ModifyLowerGroups
		{
			get
			{
				return Privilege.Get(Subject, "ModifyLowerGroups");
			}
		}
	}

	public static class BugPrivileges
	{
		public const string Subject = "Bug";

		public static Privilege Create
		{
			get
			{
				return Privilege.Get(Subject, "Create");
			}
		}

		public static Privilege Modify
		{
			get
			{
				return Privilege.Get(Subject, "Modify");
			}
		}

		public static Privilege AssignToGroup
		{
			get
			{
				return Privilege.Get(Subject, "AssignToGroup");
			}
		}

		public static Privilege AssignToUser
		{
			get
			{
				return Privilege.Get(Subject, "AssignToUser");
			}
		}
	}
}
