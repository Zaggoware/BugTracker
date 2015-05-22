using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Zaggoware.BugTracker.Common;
using Zaggoware.BugTracker.Web.Models;

namespace Zaggoware.BugTracker.Web.Controllers
{
	using Zaggoware.BugTracker.Services;

	//[Authorize]
    public abstract class BaseController : Controller
    {
        protected User CurrentUser { get; private set; }

		protected IUserService UserService { get; private set; }

	    protected BaseController(IUserService userService)
	    {
		    this.UserService = userService;
	    }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            if (this.Request.IsAuthenticated)
            {
                var userId = this.User.Identity.GetUserId();

                this.CurrentUser = this.UserService.GetUserById(userId);
            }
            else
            {
                //Fake login
                this.CurrentUser = this.CurrentUser = this.UserService.GetUserByUserName("Admin");
            }

            this.ViewBag.CurrentUser = this.CurrentUser;
        }

        protected void NotifyUser(NotifyType type, string message)
        {
            var notifications = this.TempData.Peek(Notification.IndexKey) as List<Notification>;

            if (notifications == null)
            {
                notifications = new List<Notification>();
                this.TempData[Notification.IndexKey] = notifications;
            }

            notifications.Add(new Notification(type, message));
        }

        protected JsonResult JsonSuccess(object data, bool allowGet = false)
        {
            var behavior = allowGet ? JsonRequestBehavior.AllowGet : JsonRequestBehavior.DenyGet;

            return this.Json(new { success = true, data = data }, behavior);
        }

        protected JsonResult JsonError(string errorMessage, bool allowGet = false)
        {
            var behavior = allowGet ? JsonRequestBehavior.AllowGet : JsonRequestBehavior.DenyGet;

            return this.Json(new { success = false, errorMessage = errorMessage }, behavior);
        }
    }
}