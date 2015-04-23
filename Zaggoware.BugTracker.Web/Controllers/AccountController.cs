using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zaggoware.BugTracker.Web.Models;
using Zaggoware.BugTracker.Locale;

namespace Zaggoware.BugTracker.Web.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
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
        public ActionResult Login(LoginModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Password = null;

                return this.View(model);
            }

            if (model.RememberUserName)
            {
                this.Request.Cookies.Add(new HttpCookie("RememberUserName", "True"));
            }
            else
            {
                this.Request.Cookies.Add(new HttpCookie("RememberUserName", "False"));
            }

            // TODO: Validate Login
            if (false)
            {
                this.NotifyUser(NotifyType.Error, Notifications.LoginErrorMessage);

                return this.View(model);
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