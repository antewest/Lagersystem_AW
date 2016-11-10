using LagerSystem1.Models;

namespace LagerSystem1.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LagerSystem1.DataAccessLayer.StoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LagerSystem1.DataAccessLayer.StoreContext context)
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

            context.Items.AddOrUpdate(
                i => i.Name,
                new StockItem {Name = "Dvärgbjörk", Description = "Liten björk", Price = 220, Shelf = "Björk"},
                new StockItem {Name = "Everestbjörk", Description = "Stor björk", Price = 420, Shelf = "Björk"},
                new StockItem {Name = "Glasbjörk", Description = "Vanlig björk", Price = 320, Shelf = "Björk"}
            );

        }
    }
}
