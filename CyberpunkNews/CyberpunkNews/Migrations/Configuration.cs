namespace CyberpunkNews.Migrations
{
    using CyberpunkNews.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CyberpunkNews.Models.DBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CyberpunkNews.Models.DBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.topics.AddOrUpdate(
              t => t.url,
              new topic { title = "title 1", url = "http://www.google.com", submit_date = DateTimeOffset.Now }
            );
            
        }
    }
}
