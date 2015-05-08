using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public virtual User Author { get; set; }

        public int AuthorId { get; set; }

        public virtual BugReport BugReport { get; set; }

        public int BugReportId { get; set; }

        public DateTime SubmissionDate { get; set; }
    }
}
