namespace Microsoft.CGC.DataAccess.DotNetStd.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.CGC.DataAccess.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Microsoft.CGC.DataAccess.DotNetStd.DataContext.EntityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(Microsoft.CGC.DataAccess.DotNetStd.DataContext.EntityContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var rights = new List<Right>
                             {
                                 new Right()
                                     {
                                         Name = "Create",
                                         Description = "Right to create resource"
                                     },
                                 new Right()
                                     {
                                         Name = "Update",
                                         Description = "Right to update resource"
                                     },
                                 new Right()
                                     {
                                         Name = "Delete",
                                         Description = "Right to delete resource"
                                     }
                             };

            foreach (var right in rights)
            {
                context.Rights.AddOrUpdate(right);
            }

            context.SaveChanges();
        }
    }
}
