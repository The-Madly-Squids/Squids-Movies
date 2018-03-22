using SquidsMovieApp.Data.Context;
using SquidsMovieApp.Data.Utilities.Converters;
using SquidsMovieApp.Data.Utilities.Parsers;

namespace SquidsMovieApp.Program
{
    class Program
    {
        static void Main()
        {
            string moviesPath = @"..\..\..\SquidsMovieApp.Utilities\JsonData\movies.json";
            var parser = new Parser();
            var webConverter = new WebConverter();
            var movies = parser.ParseMovies(moviesPath);


            var ctx = new MovieAppDBContext();
            var converter = new MovieConverter(ctx, webConverter, parser);
            converter.AddOrUpdateMovies(movies);
        }
    }
}
