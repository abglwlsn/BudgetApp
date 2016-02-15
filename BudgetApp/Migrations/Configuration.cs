namespace BudgetApp.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BudgetApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BudgetApp.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            context.Durations.AddOrUpdate(x => x.Id,
                new Duration() { Id = 1, Length = "Monthly" },
                new Duration() { Id = 2, Length = "Quarterly" },
                new Duration() { Id = 3, Length = "Annually" }
                );

            context.Warnings.AddOrUpdate(x => x.Id,
                new Warning() { Id = 1, WarningLevel = 50 },
                new Warning() { Id = 2, WarningLevel = 100 },
                new Warning() { Id = 3, WarningLevel = 200 },
                new Warning() { Id = 4, WarningLevel = 300 },
                new Warning() { Id = 5, WarningLevel = 500 },
                new Warning() { Id = 6, WarningLevel = 750 },
                new Warning() { Id = 7, WarningLevel = 1000 },
                new Warning() { Id = 8, WarningLevel = 2000 },
                new Warning() { Id = 9, WarningLevel = 5000 }
                );


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
        }
    }
}
