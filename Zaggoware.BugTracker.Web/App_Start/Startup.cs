using Microsoft.Owin;

[assembly: OwinStartup(typeof(Zaggoware.BugTracker.Web.Startup))]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(Zaggoware.BugTracker.Web.Startup), "Shutdown")]
namespace Zaggoware.BugTracker.Web
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Mvc;

    using Owin;

    using Zaggoware.BugTracker.Services;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            UnityConfig.RegisterAppBuilder(app);

            var container = UnityConfig.GetConfiguredContainer();

            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(container));
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // TODO: Uncomment if you want to use PerRequestLifetimeManager
            // Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));

            this.ConfigureAuth(app, container);
        }

        public void ConfigureAuth(IAppBuilder app, IUnityContainer container)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(() => container.Resolve<IUserService>());
            
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
                                            {
                                                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                                                LoginPath = new PathString("/account/login"),
                                                CookieName = "Authentication",
                                                CookieHttpOnly = true,
                                                CookieSecure = CookieSecureOption.Always,
                                                Provider = Services.UnityConfig.CreateCookieAuthenticationProvider()
                                            });
        }

        public static void Shutdown()
        {
            var container = UnityConfig.GetConfiguredContainer();
            container.Dispose();
        }

    }
}
