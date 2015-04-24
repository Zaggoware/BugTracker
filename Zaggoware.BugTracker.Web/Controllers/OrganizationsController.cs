using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zaggoware.BugTracker.Web.Controllers
{
	using Zaggoware.BugTracker.Services;

	public class OrganizationsController : BaseController
    {
		public OrganizationsController(Lazy<IUserService> userService)
			: base(userService)
	    {
	    }

        // GET: Organization
        public ActionResult Index()
        {
            return this.View();
        }
    }
}