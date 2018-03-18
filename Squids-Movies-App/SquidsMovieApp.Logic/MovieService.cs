using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Bytes2you.Validation;
using SquidsMovieApp.Data.Context;
using SquidsMovieApp.Data.Models;
using SquidsMovieApp.DTO;

namespace SquidsMovieApp.Logic
{
    public class MovieService
    {
        private readonly IMovieAppDBContext movieAppDbContext;

        public MovieService(IMovieAppDBContext movieAppDbContext)
        {
            this.movieAppDbContext = movieAppDbContext;
        }

        public IEnumerable<MovieModel> GetAllMovies()
        {
            // manual mapping between objects

            // without AutommaperEF6 - you create a list yourself
            //var movieModels = new List<MovieModel>();
            //var movies = dBContext.Movies;

            //foreach (var movie in movies)
            //{
            //    movieModels.Add(new MovieModel()
            //    {
            //        Description = movie.Description
            //    });
            //}

            //return movieModels;

            // with AutommaperEF6
            var movies = this.movieAppDbContext.Movies.ProjectTo<MovieModel>();
            return movies;
        }


        public void AddMovie(string name, string description, int year, int runningTime)
        {
            // validation and construction should not happen in this method
            // you should pass a ready IMovie object to this and method
            // and it should only add ?? 
            Guard.WhenArgument(name, "movie name")
                .IsNotNullOrEmpty()
                .Throw();
            // more validations.. 
            // call factory here
            var movieToAdd = new Movie()
            {
                Name = name,
                Description = description,
                Year = year,
                RunningTime = runningTime
            };

            this.movieAppDbContext.Movies.Add(movieToAdd);
            this.movieAppDbContext.SaveChanges();
        }
    }
}
