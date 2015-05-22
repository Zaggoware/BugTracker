using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Common
{
	public class BugReport
	{
		public int Id { get; set; }

		public string Title { get; set; }

        //public virtual UserGroup GroupAssignee { get; set; }

	    public User UserAssignee { get; set; }

		public User Reporter { get; set; }

		public DateTime CreationDate { get; set; }

		public DateTime ModificationDate { get; set; }
	}
}
