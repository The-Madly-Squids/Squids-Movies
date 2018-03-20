using SquidsMovieApp.Data.Context;
using SquidsMovieApp.Data.Models;
using SquidsMovieApp.Utilities.Parsers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.Utilities.Converters
{
    public class MovieConverter
    {
        private readonly MovieAppDBContext ctx;

        public MovieConverter(MovieAppDBContext ctx)
        {
            this.ctx = ctx;
        }

        public void AddOrUpdateMovie(MovieParsedModel movie)
        {
            var movieFound = this.ctx.Movies.FirstOrDefault(m => m.Title == movie.Title);

            if (movieFound == null)
            {
                var newMovie = new Movie
                {
                    Title = movie.Title,
                    Plot = movie.Plot,
                    Year = movie.Year,
                    Rated = movie.Rated,
                    Price = 30,
                    ImdbRating = double.Parse(movie.ImdbRating),
                    Runtime = ParseRuntime(movie.Runtime)
                };
            }
        }

        private int ParseRuntime(string runtime)
        {
            int time = int.Parse(runtime.Split(' ')[0]);

            return time;
        }
    }
}
