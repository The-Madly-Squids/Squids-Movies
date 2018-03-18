using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bytes2you.Validation;
using SquidsMovieApp.Core.Factories.Contracts;
using SquidsMovieApp.DTO;
using SquidsMovieApp.Logic.Contracts;

namespace SquidsMovieApp.Program.Controllers
{
    public class MovieController
    {
        private readonly IMovieService movieService;
        private readonly IMapper mapper;
        private readonly IMovieModelFactory factory;

        public MovieController(IMovieService movieService, IMapper mapper,
            IMovieModelFactory factory)
        {
            this.movieService = movieService;
            this.mapper = mapper;
            this.factory = factory;
        }

        public void CreateMovie(string name, string description, int year,
            int runningTime)
        {
            Guard.WhenArgument(name, "movie name")
                .IsNullOrEmpty()
                .Throw();

            Guard.WhenArgument(name, "movie description")
                .IsNullOrEmpty()
                .Throw();

            Guard.WhenArgument(year, "movie year")
                .IsLessThan(1850)
                .IsGreaterThan(2030)
                .Throw();

            Guard.WhenArgument(year, "movie running time")
                .IsLessThan(0)
                .IsGreaterThan(3600)
                .Throw();

            var movie = this.factory.CreateMovieModel(name, description, year, runningTime);
            this.movieService.AddMovie(movie);
        }

        public IEnumerable<MovieModel> GetAllMovies()
        {
            var movies = this.movieService.GetAllMovies();
            return movies;
        }
    }
}
