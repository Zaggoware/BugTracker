using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Common
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public AppType AppType { get; set; }

        //public IEnumerable<UserGroup> Groups { get; set; }

        public IEnumerable<BugReport> BugReports { get; set; }

        public Organization Organization { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }
    }
}
