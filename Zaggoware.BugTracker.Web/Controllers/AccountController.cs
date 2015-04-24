using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zaggoware.BugTracker.Web.Models;
using Zaggoware.BugTracker.Locale;

namespace Zaggoware.BugTracker.Web.Controllers
{
    using System.Web.Security;

    using Zaggoware.BugTracker.Services;

	public class AccountController : BaseController
    {
	    public AccountController(Lazy<IUserService> userService)
			: base(userService)
	    {
	    }

        // GET: Account
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string redirectUrl)
        {
            var model = new LoginModel();
            var cookie = Request.Cookies.Get("RememberUserName");

            if (cookie != null)
            {
                bool rememberUserName;
                bool.TryParse(cookie.Value, out rememberUserName);

                model.RememberUserName = rememberUserName;
            }

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model, string redirectUrl)
        {
            if (!this.ModelState.IsValid)
            {
                model.Password = null;

                return this.View(model);
            }

            var cookie = this.Response.Cookies.Get("RememberUserName");
            if (cookie == null)
            {
                cookie = new HttpCookie("RememberUserName", model.RememberUserName.ToString());

                this.Request.Cookies.Add(cookie);
            }
            cookie.Expires = DateTime.Now.AddYears(1);

            if (!this.UserService.Value.ValidatePassword(model.UserName, model.Password))
            {
                this.NotifyUser(NotifyType.Error, Notifications.LoginErrorMessage);

                return this.View(model);
            }

            FormsAuthentication.SetAuthCookie(model.UserName, true);

            if (Url.IsLocalUrl(redirectUrl))
            {
                return this.Redirect(redirectUrl);
            }

            return this.RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Only listen to POST request when logging out.
        /// </summary>
        /// <returns>ActionResult</returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Logout()
        {
            return this.RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ActionName("Logout")]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult LogoutPost()
        {
            // TODO: Logout
            this.NotifyUser(NotifyType.Success, Notifications.LogoutSuccessMessage);

            return this.RedirectToAction("Login");
        }
    }
}