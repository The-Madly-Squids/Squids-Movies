using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bytes2you.Validation;
using SquidsMovieApp.Core.Factories.Contracts;
using SquidsMovieApp.Data.Models;
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

        public void DeleteMovie(string movieName)
        {
            Guard.WhenArgument(movieName, "movie name")
                .IsNullOrEmpty()
                .Throw();

            var movieToDelete = this.movieService.GetAllMovies().FirstOrDefault(
                x => x.Name == movieName);

            this.movieService.RemoveMovie(movieToDelete);
        }

        public void AddMovieParticipant(string movieName, string pFirstName,
            string pLastName, string role)
        {
            Guard.WhenArgument(movieName, "movie name")
                .IsNullOrEmpty()
                .Throw();

            Guard.WhenArgument(pFirstName, "participant first name")
                .IsNullOrEmpty()
                .Throw();

            Guard.WhenArgument(pLastName, "participant last name")
               .IsNullOrEmpty()
               .Throw();

            Guard.WhenArgument(role, "participant role")
               .IsNullOrEmpty()
               .Throw();

            var movie = this.movieService.GetAllMovies()
                .FirstOrDefault(x => x.Name == movieName);

            if (movie == null)
            {
                throw new ArgumentNullException("Movie not found! Add it to DB first!");
            }

            var movieParticipants = this.movieService.GetAllParticipantsPerMovie(movie);

            var participant = movieParticipants.Where(x => x.FirstName == pFirstName
            && x.LastName == pLastName).FirstOrDefault();

            if (participant == null)
            {
                throw new ArgumentNullException("This participant is not in the DB." +
                    "Add it first!");
            }

            var partRole = participant.Roles
                .Where(x => x.ParticipantId == participant.ParticipantId
                && movie.MovieId == x.MovieId)
                .FirstOrDefault().RoleName;

            if (partRole == role)
            {
                throw new ArgumentException("Participant with same name and role already in " +
                    "the DB");
            }

            // move to AddRole() in  controller layer( RoleController)
            // AddRole(MovieModel movie, Participant participant, string roleName);
            // return objects in role to old properties;
            var roleObject = new Role()
            {
                MovieId = movie.MovieId,
                ParticipantId = participant.ParticipantId,
                RoleName = role
            };

            // TO-DO must finish Partcipant class and DTO first 
            // need to check if name && role are already existin
            // if not break;

            throw new NotImplementedException();
        }

        public IEnumerable<MovieModel> GetAllMovies()
        {
            var movies = this.movieService.GetAllMovies();
            return movies;
        }

    }
}
