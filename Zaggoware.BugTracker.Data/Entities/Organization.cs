using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zaggoware.BugTracker.Data.Entities
{
	public class Organization
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public virtual ICollection<UserGroup> UserGroups { get; set; }

		public DateTime CreationDate { get; set; }

		public DateTime ModificationDate { get; set; }
	}
}
