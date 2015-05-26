using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zaggoware.BugTracker.Web.Controllers
{
    using Zaggoware.BugTracker.Common.Tagging;
    using Zaggoware.BugTracker.Services;
	using Zaggoware.BugTracker.Web.Models;

    public class OrganizationsController : BaseController
    {
	    private readonly IOrganizationService organizationService;

	    public OrganizationsController(IUserService userService, IOrganizationService organizationService)
			: base(userService)
		{
		    this.organizationService = organizationService;
		}

	    [HttpGet]
        public ActionResult Index()
        {
            var organizations = this.organizationService.GetOrganizations();

            return this.View(organizations);
        }

        [HttpGet]
	    public ActionResult Create()
        {
            return
                this.View(
                    new OrganizationModel
                        {
                            Managers =
                                new TagList
                                    {
                                        new TagListItem { HiddenValue = 1, Label = "Patrick" },
                                        new TagListItem { HiddenValue = 3, Label = "Joeri" }
                                    }
                        });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
	    public ActionResult Create(OrganizationModel model)
        {
            if (ModelState.IsValid)
            {
                var organization = this.organizationService.CreateOrganization(model.Name, true);

                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }
    }
}