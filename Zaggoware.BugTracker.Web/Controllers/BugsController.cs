using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zaggoware.BugTracker.Web.Controllers
{
	using Zaggoware.BugTracker.Services;

	public class BugsController : BaseController
    {
		public BugsController(Lazy<IUserService> userService)
			: base(userService)
	    {
	    }

        // GET: Bug
        public ActionResult Index()
        {
            return View();
        }
    }
}