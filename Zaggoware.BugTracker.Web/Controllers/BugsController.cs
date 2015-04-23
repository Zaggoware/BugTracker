using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zaggoware.BugTracker.Controllers
{
    public class BugsController : Controller
    {
        // GET: Bug
        public ActionResult Index()
        {
            return View();
        }
    }
}