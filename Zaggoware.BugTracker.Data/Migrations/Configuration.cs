namespace Zaggoware.BugTracker.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Zaggoware.BugTracker.Data.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.DbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Data.DbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (
                context.Users.SingleOrDefault(
                    u => u.UserName.Equals("Admin", StringComparison.InvariantCultureIgnoreCase)) == null)
            {
                var userManager = this.ResolveUserManager(context);
                var admin = context.Users.Create();
                admin.UserName = "Admin";
                admin.Email = "admin@zaggoware.nl";
                admin.EmailConfirmed = true;
                admin.PasswordHash = userManager.PasswordHasher.HashPassword("Admin");

                userManager.Create(admin, "Admin");

                context.Users.Add(admin);
                context.SaveChanges();

                userManager.AddToRole(admin.Id, "Admin");
            }
        }

        private UserManager<User> ResolveUserManager(Data.DbContext context)
        {
            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);

            // Configure validation logic for usernames
            userManager.UserValidator = new UserValidator<User>(userManager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 8,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            return userManager;
        }
    }
}
