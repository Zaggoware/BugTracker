using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zaggoware.BugTracker.Web.Controllers
{
    public class OrganizationsController : Controller
    {
        // GET: Organization
        public ActionResult Index()
        {
            return View();
        }
    }
}