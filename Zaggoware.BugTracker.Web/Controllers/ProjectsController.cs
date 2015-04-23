using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zaggoware.BugTracker.Controllers
{
    public class ProjectsController : Controller
    {
        // GET: Project
        public ActionResult Index()
        {
            return View();
        }
    }
}