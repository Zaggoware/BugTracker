using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zaggoware.BugTracker.Web.Controllers
{
    using System.Threading.Tasks;

    using Zaggoware.BugTracker.Locale;
    using Zaggoware.BugTracker.Services;
    using Zaggoware.BugTracker.Web.Models;

    public class UsersController : BaseController
    {
        public UsersController(IUserService userService)
            : base(userService)
        {
        }

        public ActionResult Index(int page)
        {
            var users = this.UserService.GetUsers(page);

            return View(users);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return this.View(new UserModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserModel model)
        {
            // Only check if the user name or email address already exist when the model state is valid.
            if (ModelState.IsValid)
            {
                if (this.UserService.GetUserByUserName(model.UserName) != null)
                {
                    this.ModelState.AddModelError("UserName", Errors.UserNameAlreadyExists);
                }

                if (this.UserService.GetUserByEmailAddress(model.EmailAddress) != null)
                {
                    this.ModelState.AddModelError("EmailAddress", Errors.EmailAddressAlreadyExists);
                }
            }

            // When AddModelError is called, the model state automatically turns to invalid.
            if (!ModelState.IsValid)
            {
                model.Password = null;

                return this.View(model);
            }

            this.UserService.CreateUser(
                model.UserName,
                model.Password,
                model.EmailAddress,
                model.FirstName,
                model.LastName);

            this.NotifyUser(NotifyType.Success, Notifications.UserCreateSuccess);

            return this.RedirectToAction("Index");
        }
    }
}