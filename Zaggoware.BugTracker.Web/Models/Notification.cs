using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Web.Models
{
    public class Notification
    {
        public const string IndexKey = "Notifications";

        public NotifyType Type { get; set; }

        public string Message { get; set; }

        public Notification()
        {
        }

        public Notification(NotifyType type, string message)
            : this()
        {
            this.Type = type;
            this.Message = message;
        }
    }
}
