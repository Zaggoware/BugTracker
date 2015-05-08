using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Data.Entities
{
	public class BugReport
	{
		public int Id { get; set; }

		public string Title { get; set; }

        public virtual UserGroup GroupAssignee { get; set; }

	    public virtual User UserAssignee { get; set; }

		public virtual User Reporter { get; set; }

		public DateTime CreationDate { get; set; }

		public DateTime ModificationDate { get; set; }
	}
}
