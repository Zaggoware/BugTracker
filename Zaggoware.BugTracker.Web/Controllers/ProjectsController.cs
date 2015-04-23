using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zaggoware.BugTracker.Web.Controllers
{
    public class ProjectsController : BaseController
    {
        // GET: Project
        public ActionResult Index()
        {
            return View();
        }
    }
}