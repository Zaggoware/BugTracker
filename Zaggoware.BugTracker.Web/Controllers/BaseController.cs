using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Zaggoware.BugTracker.Common;
using Zaggoware.BugTracker.Web.Models;

namespace Zaggoware.BugTracker.Web.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        public User CurrentUser { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        protected void NotifyUser(NotifyType type, string message)
        {
            var notifications = this.TempData[Notification.IndexKey] as List<Notification>;

            if (notifications == null)
            {
                notifications = new List<Notification>();
                this.TempData[Notification.IndexKey] = notifications;
            }

            notifications.Add(new Notification(type, message));
        }
    }
}