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

            // with AutommaperEF6
            var movies = this.movieAppDbContext.Movies.ProjectTo<MovieModel>();
            return movies;
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

            var movieToRemove = this.mapper.Map<Movie>(movie);

            this.movieAppDbContext.Movies.Remove(movieToRemove);
            this.movieAppDbContext.SaveChanges();
        }

        public IEnumerable<ParticipantModel> GetAllParticipantsPerMovie(MovieModel movie)
        {
            // how to do it with AutoMapper - cant use ProjecTo<> because it is not a
            // DbSet object - but an inner table(ICollection) between movie and partcipant 
            var participantsModelsList = new List<ParticipantModel>();
            var participants = movie.Participants;

            foreach (var particpant in participants)
            {
                var participantModel = new ParticipantModel()
                {
                    FirstName = particpant.FirstName
                    // add more properties to DTO
                };
                participantsModelsList.Add(participantModel);
            }

            //var movies = this.movieAppDbContext.Movies.ProjectTo<MovieModel>();

            return participantsModelsList;


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

            // object from DTO - possible? will it be in the DB?
            var movieObject = this.mapper.Map<Movie>(movie);
            var participantObject = this.mapper.Map<Participant>(participant);

            var actorRole = new Role()
            {
                Movie = movieObject,
                Participant = participantObject,
                RoleName = roleName
            };
            this.movieAppDbContext.Roles.Add(actorRole);
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
                        .Select(a => a.Participant).ProjectTo<ParticipantModel>()
                        .ToList();

            return actorsRoles;

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
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetUsersWhoBoughtIt(MovieModel movie)
        {
            if (movie == null)
            {
                throw new ArgumentNullException("Movie cannot be null!");
            }

            var userModelsList = new List<UserModel>();
            var users = movie.BoughtBy;

            foreach (var user in users)
            {
                var userModel = new UserModel()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Age = user.Age
                };
                userModelsList.Add(userModel);
            }

            return userModelsList;
        }

        public IEnumerable<UserModel> GetUsersWhoLikedtIt(MovieModel movie)
        {
            if (movie == null)
            {
                throw new ArgumentNullException("Movie cannot be null!");
            }

            var userModelsList = new List<UserModel>();
            var users = movie.LikedBy;

            foreach (var user in users)
            {
                var userModel = new UserModel()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Age = user.Age
                };
                userModelsList.Add(userModel);
            }

            return userModelsList;
        }
    }
}
