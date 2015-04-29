using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Zaggoware.BugTracker.Web.Models;
using Zaggoware.BugTracker.Locale;
using Zaggoware.BugTracker.Services;

namespace Zaggoware.BugTracker.Web.Controllers
{
	public class AccountController : BaseController
    {
	    public AccountController(IUserService userService)
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
        public ActionResult Login(string returnUrl)
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
        public ActionResult Login(LoginModel model, string returnUrl)
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

            if (!this.UserService.ValidatePassword(model.UserName, model.Password))
            {
                this.NotifyUser(NotifyType.Error, Notifications.LoginError);

                return this.View(model);
            }

            // TODO: owin login

            if (Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
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
            this.NotifyUser(NotifyType.Success, Notifications.LogoutSuccess);

            return this.RedirectToAction("Login");
        }
    }
}