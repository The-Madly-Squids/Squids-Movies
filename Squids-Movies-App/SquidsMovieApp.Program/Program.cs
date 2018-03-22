using SquidsMovieApp.Data.Context;
using SquidsMovieApp.Data.Utilities.Converters;
using SquidsMovieApp.Data.Utilities.Parsers;
using System.Linq;

namespace SquidsMovieApp.Program
{
    public class Program
    {
        public static void Main()
        {
            var context = new MovieAppDBContext();

            if (!context.Movies.Any())
            {
                string moviesPath = @"..\..\..\SquidsMovieApp.Data\Utilies\JsonData\movies.json";
                         var parser = new Parser();
                var webConverter = new WebConverter();
                var movies = parser.ParseMovies(moviesPath);

                var converter = new MovieConverter(context, webConverter, parser);
                converter.AddOrUpdateMovies(movies);
            }
        }
    }
}
