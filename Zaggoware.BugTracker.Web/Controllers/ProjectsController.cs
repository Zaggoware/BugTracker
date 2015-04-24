using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zaggoware.BugTracker.Web.Controllers
{
	using Zaggoware.BugTracker.Services;

	public class ProjectsController : BaseController
    {
	    public ProjectsController(Lazy<IUserService> userService)
			: base(userService)
	    {
	    }

        // GET: Project
        public ActionResult Index()
        {
            return View();
        }
    }
}