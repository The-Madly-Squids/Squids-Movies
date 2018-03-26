using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bytes2you.Validation;
using SquidsMovieApp.Data.Context;
using SquidsMovieApp.Data.Models;
using SquidsMovieApp.DTO;
using SquidsMovieApp.Logic.Contracts;

namespace SquidsMovieApp.Logic
{
    public class MovieService : IMovieService
    {
        private readonly IMovieAppDBContext movieAppDbContext;
        private readonly IMapper mapper;

        public MovieService(IMovieAppDBContext movieAppDbContext, IMapper mapper)
        {
            this.movieAppDbContext = movieAppDbContext;
            this.mapper = mapper;
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


            // with AutommaperEF6/AutoMapper.QueryableExtensions;
            // cannot test ProjectTo<MovieModel>() method
            // it emits a SELECT query which gives the dual initialization error
            // ask for help
            //var movies = this.movieAppDbContext.Movies.ProjectTo<MovieModel>();

            var moviesPoco = this.movieAppDbContext.Movies.ToList();
            var moviesDto = mapper.Map<IList<MovieModel>>(moviesPoco);

            return moviesDto;
        }

        public MovieModel GetMovie(string movieTitle)
        {
            var movie = this.movieAppDbContext.Movies
                                .Where(x => x.Title == movieTitle)
                                .FirstOrDefault();
            if (movie == null)
            {
                throw new ArgumentNullException("Movie not found!");
            }

            var movieDto = mapper.Map<MovieModel>(movie);
            return movieDto;
        }

        public void AddMovie(MovieModel movie)
        {
            if (movie == null)
            {
                throw new ArgumentException();
            }

            var movieToAdd = this.mapper.Map<Movie>(movie);

            this.movieAppDbContext.Movies.Add(movieToAdd);
            this.movieAppDbContext.SaveChanges();
        }

        public void RemoveMovie(MovieModel movie)
        {
            if (movie == null)
            {
                throw new ArgumentException();
            }

            var movieToRemoveObject = this.movieAppDbContext.Movies
                .Where(x => x.MovieId == movie.MovieId)
                .FirstOrDefault();

            this.movieAppDbContext.Movies.Remove(movieToRemoveObject);
            this.movieAppDbContext.SaveChanges();
        }

        public IEnumerable<ParticipantModel> GetAllParticipantsPerMovie(MovieModel movie)
        {
            return movie.Participants;
        }

        public void AddMovieParticipant(MovieModel movie, ParticipantModel participant,
            string roleName)
        {
            Guard.WhenArgument(movie, "movie")
                .IsNull()
                .Throw();

            Guard.WhenArgument(participant, "participant")
                .IsNull()
                .Throw();

            Guard.WhenArgument(roleName, "role name")
                .IsNullOrEmpty()
                .Throw();

            var movieObject = this.movieAppDbContext.Movies
                              .Where(x => x.MovieId == movie.MovieId)
                              .FirstOrDefault();

            var participantObject = this.movieAppDbContext.Participants
                                    .Where(x => x.ParticipantId == participant.ParticipantId)
                                    .FirstOrDefault();


            // currently the int FK is redundant
            var actorRole = new Role()
            {
                Movie = movieObject,
                Participant = participantObject,
                RoleName = roleName
            };

            // is this single responsability?
            // method adds participant to movie
            // movie to participant
            // creates a role
            // create only role and use it to find movies-to-participants? 
            movieObject.Participants.Add(participantObject);
            participantObject.Movies.Add(movieObject);
            this.movieAppDbContext.Roles.Add(actorRole);
            this.movieAppDbContext.SaveChanges();
        }

        public double GetRating(MovieModel movie)
        {
            if (movie == null)
            {
                throw new ArgumentNullException("Movie not found!");
            }

            var ratingCollection = this.movieAppDbContext.Reviews
                .Where(x => x.Movie.MovieId == movie.MovieId);

            var averageRating = ratingCollection.Count() > 0 ?
                 ratingCollection.Average(x => x.Rating) : 0.0;

            return averageRating;
        }

        public IEnumerable<ParticipantModel> GetActors(MovieModel movie)
        {
            if (movie == null)
            {
                throw new ArgumentNullException("No such movie!");
            }

            var actorsRoles = this.movieAppDbContext.Roles
                .Where(x => x.Movie.MovieId == movie.MovieId &&
                                                    x.RoleName == "Actor")
            .Select(a => a.Participant)
            .ToList();

            var actorsDtos = this.mapper.Map<IList<ParticipantModel>>(actorsRoles);
            return actorsDtos;
        }

        public IEnumerable<ParticipantModel> GetDirectors(MovieModel movie)
        {
            if (movie == null)
            {
                throw new ArgumentNullException("No such movie!");
            }

            var actorsRoles = this.movieAppDbContext.Roles
                .Where(x => x.Movie.MovieId == movie.MovieId &&
                                x.RoleName == "Director")
                .Select(a => a.Participant).ProjectTo<ParticipantModel>()
                .ToList();

            return actorsRoles;
        }

        public IEnumerable<string> GetMovieGenres(MovieModel movie)
        {
            if (movie == null)
            {
                throw new ArgumentNullException("Movie cannot be null!");
            }

            // doesnt exist yet - implement it
            //var movieGenres = movie.Genres;

            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetUsersWhoBoughtIt(MovieModel movie)
        {
            if (movie == null)
            {
                throw new ArgumentNullException("Movie cannot be null!");
            }

            var usersThatBoughtMovieList = movie.BoughtBy;

            return usersThatBoughtMovieList;
        }

        public IEnumerable<UserModel> GetUsersWhoLikedtIt(MovieModel movie)
        {
            if (movie == null)
            {
                throw new ArgumentNullException("Movie cannot be null!");
            }

            var users = movie.LikedBy;

            return users;
        }
    }
}
