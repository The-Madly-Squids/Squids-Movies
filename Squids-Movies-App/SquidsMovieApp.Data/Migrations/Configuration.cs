namespace SquidsMovieApp.Data.Migrations
{
    using SquidsMovieApp.Data.Context;
    using SquidsMovieApp.Data.Utilities.Converters;
    using SquidsMovieApp.Data.Utilities.Parsers;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Context.MovieAppDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MovieAppDBContext context)
        {
            
        }
    }
}
