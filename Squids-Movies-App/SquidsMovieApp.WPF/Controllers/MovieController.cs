using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bytes2you.Validation;
using SquidsMovieApp.Core.Factories.Contracts;
using SquidsMovieApp.Models;
using SquidsMovieApp.DTO;
using SquidsMovieApp.Logic.Contracts;

namespace SquidsMovieApp.WPF.Controllers
{
    public class MovieController
    {
        private readonly IMovieService movieService;
        private readonly IRoleService roleService;
        private readonly IMapper mapper;
        private readonly IMovieModelFactory factory;

        public MovieController(IMovieService movieService, IRoleService roleService,
            IMapper mapper, IMovieModelFactory factory)
        {
            this.movieService = movieService;
            this.roleService = roleService;
            this.mapper = mapper;
            this.factory = factory;
        }

        public IEnumerable<MovieModel> GetAllMovies()
        {
            var movies = this.movieService.GetAllMovies();
            return movies;
        }

        public MovieModel GetMovieByTitle(string movieTitle)
        {
            Guard.WhenArgument(movieTitle, "movie name")
                .IsNullOrEmpty()
                .Throw();

            var movie = this.movieService.GetMovieByTitle(movieTitle);
            return movie;
        }

        public MovieModel GetMovieById(int id)
        {
            Guard.WhenArgument(id, "movie id")
               .IsLessThan(1)
               .Throw();

            var movie = this.movieService.GetMovieById(id);
            return movie;
        }

        public void AddMovie(string name, string description, int year,
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

            // create MovieModel(DTO) or Movie(Table) here?
            var movie = this.factory.CreateMovieModel(name, description, year, runningTime);
            this.movieService.AddMovie(movie);
        }

        public void RemoveMovie(string movieName)
        {
            Guard.WhenArgument(movieName, "movie name")
                .IsNullOrEmpty()
                .Throw();

            var movieToDelete = this.movieService.GetMovieByTitle(movieName);

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

            var movie = this.movieService.GetMovieByTitle(movieName);

            var movieParticipants = this.movieService.GetAllParticipantsPerMovie(movie);

            var participant = movieParticipants.Where(x => x.FirstName == pFirstName
            && x.LastName == pLastName).FirstOrDefault();

            if (participant == null)
            {
                throw new ArgumentNullException("This participant is not in the DB." +
                    "Add it first!");
            }

            var pRoles = this.roleService.ParticipantRolesPerMovie(participant, movie);
            // possibly get this code out in a separate method that checks if
            // participant already exisits
            foreach (var r in pRoles)
            {
                if (r.RoleName == role)
                {
                    throw new ArgumentException("Participant with same name and role in the movie" +
                        " already in the DB");
                }
            }

            this.movieService.AddMovieParticipant(movie, participant, role);
        }

        public double GetRating(string movieName)
        {
            Guard.WhenArgument(movieName, "movie name")
                .IsNullOrEmpty()
                .Throw();

            var movie = this.movieService.GetMovieByTitle(movieName);

            return this.movieService.GetRating(movie);
        }

        public IEnumerable<ParticipantModel> GetActors(string movieName)
        {
            Guard.WhenArgument(movieName, "movie name")
                .IsNullOrEmpty()
                .Throw();

            var movieModel = this.movieService.GetMovieByTitle(movieName);

            return this.movieService.GetActors(movieModel);
        }

        public IEnumerable<ParticipantModel> GetDirectors(string movieName)
        {
            Guard.WhenArgument(movieName, "movie name")
              .IsNullOrEmpty()
              .Throw();

            var movieModel = this.movieService.GetMovieByTitle(movieName);

            return this.movieService.GetDirectors(movieModel);
        }

        public IEnumerable<UserModel> GetUsersWhoBoughtIt(string movieName)
        {
            Guard.WhenArgument(movieName, "movie name")
            .IsNullOrEmpty()
            .Throw();

            var movieModel = this.movieService.GetMovieByTitle(movieName);

            return this.movieService.GetUsersWhoBoughtIt(movieModel);
        }

        public IEnumerable<UserModel> GetUsersWhoLikedIt(string movieName)
        {
            Guard.WhenArgument(movieName, "movie name")
            .IsNullOrEmpty()
            .Throw();

            var movieModel = this.movieService.GetMovieByTitle(movieName);

            return this.movieService.GetUsersWhoLikedtIt(movieModel);
        }

        public IEnumerable<ParticipantModel> GetAllParticipants(string movieName)
        {
            Guard.WhenArgument(movieName, "movie name")
            .IsNullOrEmpty()
            .Throw();

            var movieModel = this.movieService.GetMovieByTitle(movieName);

            return this.movieService.GetAllParticipantsPerMovie(movieModel);
        }

        public IEnumerable<MovieModel> SearchForMoviesByTitle(string pattern)
        {
            Guard.WhenArgument(pattern, "search pattern")
               .IsNullOrEmpty()
               .IsNullOrWhiteSpace()
               .Throw();

            var movies = this.movieService.GetMoviesByTitleSearch(pattern);

            return movies;
        }

        public IEnumerable<GenreModel> GetMovieGenres(MovieModel movie)
        {
            if (movie == null)
            {
                throw new ArgumentNullException("Movie cannot be null!");
            }

            var genres = this.movieService.GetMovieGenres(movie);

            return genres;
        }
    }
}
