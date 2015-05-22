using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zaggoware.BugTracker.Web.Controllers
{
	using Zaggoware.BugTracker.Services;
	using Zaggoware.BugTracker.Web.Models;

    public class ProjectsController : BaseController
    {
        private readonly IProjectService projectService;

        public ProjectsController(IUserService userService, IProjectService projectService)
			: base(userService)
	    {
	        this.projectService = projectService;
	    }

        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
	    public ActionResult Create(int organizationId)
        {
            return this.View(new ProjectModel());
        }

	    [HttpPost]
	    [ValidateAntiForgeryToken]
	    public ActionResult Create(int organizationId, ProjectModel model)
	    {
	        if (ModelState.IsValid)
	        {
	            var project = this.projectService.CreateProject(organizationId, model.Name, model.AppType, model.CopyGroupTemplates);

	            if (project != null)
	            {
	                return this.RedirectToAction("Details", new { organizationId = organizationId, url = project.Url });
	            }
	        }

	        return this.View(model);
	    }

        [HttpGet]
        public ActionResult Details(int organizationId, string url)
        {
            var project = this.projectService.GetProjectByUrl(organizationId, url);

            if (project == null)
            {
                return this.HttpNotFound();
            }

            return this.View(project);
        }
    }
}