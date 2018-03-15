namespace SquidsMovieApp.Data.Migrations
{
    using SquidsMovieApp.Data.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SquidsMovieApp.Data.Context.DBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "SquidsMovieApp.Data.Context.DBContext";
        }

        protected override void Seed(SquidsMovieApp.Data.Context.DBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
