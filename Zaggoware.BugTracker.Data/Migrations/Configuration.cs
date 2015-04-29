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

            if (
                context.Users.SingleOrDefault(
                    u => u.UserName.Equals("Admin", StringComparison.InvariantCultureIgnoreCase)) == null)
            {
                var userManager = new UserManager<User>(new UserStore<User>(context));
                var admin = context.Users.Create();
                admin.UserName = "Admin";

                userManager.Create(admin, "Admin");

                context.Users.Add(admin);
                context.SaveChanges();
            }
        }
    }
}
