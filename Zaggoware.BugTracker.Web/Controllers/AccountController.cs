using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Zaggoware.BugTracker.Web.Models;
using Zaggoware.BugTracker.Locale;
using Zaggoware.BugTracker.Services;
using Zaggoware.BugTracker.Web.Helpers;

namespace Zaggoware.BugTracker.Web.Controllers
{
    using Microsoft.Ajax.Utilities;
    using Microsoft.AspNet.Identity.Owin;

    public class AccountController : BaseController
    {
        private readonly ApplicationSignInManager signInManager;

        public AccountController(IUserService userService, ApplicationSignInManager signInManager)
            : base(userService)
        {
            this.signInManager = signInManager;
        }

        // GET: Account
        public ActionResult Index()
        {
            return this.RedirectToAction("Login");
        }

        [HttpGet]
	    public ActionResult Edit()
	    {
	        return this.View(new AccountModel(this.CurrentUser));
	    }

        [HttpPost]
        [ValidateAntiForgeryToken]
	    public ActionResult Edit(AccountModel model)
        {
            if (ModelState.IsValid)
            {
                this.UserService.UpdateUser(
                    this.CurrentUser.Id,
                    model.EmailAddress,
                    model.FirstName,
                    model.LastName
                );

                if (!string.IsNullOrEmpty(model.CurrentPassword))
                {
                    if (string.IsNullOrEmpty(model.NewPassword))
                    {
                        this.ModelState.AddModelError<AccountModel>(m => m.NewPassword, Errors.NewPasswordRequired);

                        return this.View(model);
                    }

                    if (model.NewPassword != model.NewPasswordConfirm)
                    {
                        this.ModelState.AddModelError<AccountModel>(m => m.NewPasswordConfirm, Errors.NewPasswordsNotEqual);

                        return this.View(model);
                    }

                    if (!this.UserService.UpdatePassword(this.CurrentUser.Id, model.CurrentPassword, model.NewPassword))
                    {
                        this.ModelState.AddModelError<AccountModel>(m => m.CurrentPassword, Errors.CurrentPasswordInvalid);

                        return this.View(model);
                    }
                }

                return this.RedirectToAction("Edit");
            }

            return this.View(model);
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

            var result = this.signInManager.SignIn(model.UserName, model.Password, true);

            switch (result)
            {
                case SignInStatus.Success:
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return this.Redirect(returnUrl);
                    }

                    return this.RedirectToAction("Index", "Home");
                    break;

                default:
                    this.NotifyUser(NotifyType.Error, Notifications.LoginError);

                    return this.View(model);
            }

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