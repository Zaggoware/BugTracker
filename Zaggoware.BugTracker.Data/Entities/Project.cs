using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Data.Entities
{
	public class Project
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public virtual ICollection<BugReport> BugReports { get; set; }

		public virtual Organization Organization { get; set; }

        public int OrganizationId { get; set; }
	}
}
