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
                new Duration() {Length = "Monthly" },
                new Duration() {Length = "Quarterly" },
                new Duration() {Length = "Annually" }
                );

            context.Warnings.AddOrUpdate(x => x.Id,
                new Warning() {WarningLevel = 50 },
                new Warning() {WarningLevel = 100 },
                new Warning() {WarningLevel = 200 },
                new Warning() {WarningLevel = 300 },
                new Warning() {WarningLevel = 500 },
                new Warning() {WarningLevel = 750 },
                new Warning() {WarningLevel = 1000 },
                new Warning() {WarningLevel = 2000 },
                new Warning() {WarningLevel = 5000 }
                );

            context.CategoryStandards.AddOrUpdate(x => x.Id,
                new CategoryStandard() {Name = "Household" },
                new CategoryStandard() {Name = "Food" },
                new CategoryStandard() {Name = "Health/Medical" },
                new CategoryStandard() {Name = "Transportation" },
                new CategoryStandard() {Name = "Education" },
                new CategoryStandard() {Name = "Taxes/Fees" },
                new CategoryStandard() {Name = "Insurance" },
                new CategoryStandard() {Name = "Salary" },
                new CategoryStandard() {Name = "Investments/Interest" },
                new CategoryStandard() {Name = "Clothing" },
                new CategoryStandard() {Name = "Pet" },
                new CategoryStandard() {Name = "Travel" },
                new CategoryStandard() {Name = "Hobbies/Leisure" },
                new CategoryStandard() {Name = "Gifts/Charity" }
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
