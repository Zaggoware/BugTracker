using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Zaggoware.BugTracker.Web
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.LowercaseUrls = true;

			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

		    routes.MapRoute(
		        "ProjectDetails",
		        "organizations/{organizationId}/{url}",
		        new { controller = "Projects", action = "Details" });

            //routes.MapRoute(
            //    "CreateProject",
            //    "organizations/{organizationId}/create",
            //    new { controller = "Projects", action = "Create" });

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
