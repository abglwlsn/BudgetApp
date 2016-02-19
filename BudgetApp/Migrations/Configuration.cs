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

            context.Warnings.AddOrUpdate(x => x.WarningLevel,
                new Warning() {WarningLevel = "None" },
                new Warning() {WarningLevel = "50" },
                new Warning() {WarningLevel = "100" },
                new Warning() {WarningLevel = "250" },
                new Warning() {WarningLevel = "500" },
                new Warning() {WarningLevel = "750" },
                new Warning() {WarningLevel = "1000" }
                );

            context.CategoryStandards.AddOrUpdate(x => x.Name,
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
                new CategoryStandard() {Name = "Gifts/Charity" },
                new CategoryStandard() {Name = "Miscellaneous" }
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
