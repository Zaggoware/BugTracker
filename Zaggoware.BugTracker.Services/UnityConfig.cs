using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace Zaggoware.BugTracker.Services
{
    using System.Web;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.DataProtection;

    using Owin;

    using Zaggoware.BugTracker.Data;
    using Zaggoware.BugTracker.Data.Entities;

    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IAppBuilder app, IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            container.RegisterType<IDbContext, DbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<System.Data.Entity.DbContext, DbContext>(new HierarchicalLifetimeManager());
            container.RegisterInstance<IUserStore<User>>(
                new UserStore<User>(container.Resolve<IDbContext>() as DbContext), new ContainerControlledLifetimeManager());
            container.RegisterInstance(app.GetDataProtectionProvider());
            container.RegisterInstance(container.Resolve<IOwinContext>().Authentication);
            container.RegisterInstance<IUserService>(CreateUserService(container), new ContainerControlledLifetimeManager());
            container.RegisterType<InternalSignInManager>(new ContainerControlledLifetimeManager());
            container.RegisterType<ApplicationSignInManager>();

            container.RegisterType<IOrganizationService, OrganizationService>();
            container.RegisterType<IProjectService, ProjectService>();
        }


        /// <summary>
        /// The create cookie authentication provider.
        /// </summary>
        /// <returns>
        /// The <see cref="ICookieAuthenticationProvider"/>.
        /// </returns>
        public static ICookieAuthenticationProvider CreateCookieAuthenticationProvider()
        {
            // Enables the application to validate the security stamp when the user logs in.
            // This is a security feature which is used when you change a password or add an external login to your account.
            // NOTE: Custom OnApplyRedirect is needed to prevent OWIN from redirecting AJAX-requests to the login page. Instead
            // return a 401 response. See: http://brockallen.com/2013/10/27/using-cookie-authentication-middleware-with-web-api-and-401-response-codes/
            return new CookieAuthenticationProvider
            {
                OnValidateIdentity =
                    SecurityStampValidator
                    .OnValidateIdentity<UserManager<User>, User>(
                        TimeSpan.FromMinutes(30),
                        (manager, user) =>
                        user.GenerateUserIdentityAsync(manager)),
                OnApplyRedirect = context =>
                {
                    if (!IsAjaxRequest(context.Request))
                    {
                        context.Response.Redirect(context.RedirectUri);
                    }
                }
            };
        }

        /// <summary>
        /// The create user service,
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        private static UserService CreateUserService(IUnityContainer container)
        {
            var dbContext = container.Resolve<IDbContext>();
            var userStore = container.Resolve<IUserStore<User>>();
            var userService = new UserService(userStore, dbContext);

            // Configure validation logic for usernames
            userService.UserValidator = new UserValidator<User>(userService)
                                        {
                                            AllowOnlyAlphanumericUserNames = false, 
                                            RequireUniqueEmail = true
                                        };

            // Configure validation logic for passwords
            userService.PasswordValidator = new PasswordValidator
                                                {
                                                    RequiredLength = 8,
                                                    RequireNonLetterOrDigit = false,
                                                    RequireDigit = false,
                                                    RequireLowercase = false,
                                                    RequireUppercase = false,
                                                };

            // Configure user lockout defaults
            userService.UserLockoutEnabledByDefault = true;
            userService.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            userService.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            userService.RegisterTwoFactorProvider(
                "Email Code",
                new EmailTokenProvider<User> { Subject = "Security Code", BodyFormat = "Your security code is {0}" });

            // TODO: userService.EmailService = container.GetInstance<IMailerService>();

            var dataProtectionProvider = container.Resolve<IDataProtectionProvider>();
            if (dataProtectionProvider != null)
            {
                userService.UserTokenProvider =
                    new DataProtectorTokenProvider<User>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return userService;
        }

        /// <summary>
        /// The is ajax request.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool IsAjaxRequest(IOwinRequest request)
        {
            if ((request.Query != null) && (request.Query["X-Requested-With"] == "XMLHttpRequest"))
            {
                return true;
            }

            return (request.Headers != null) && (request.Headers["X-Requested-With"] == "XMLHttpRequest");
        }
    }
}
