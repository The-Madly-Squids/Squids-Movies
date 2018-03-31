using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bytes2you.Validation;
using SquidsMovieApp.Data.Context;
using SquidsMovieApp.Models;
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

        public MovieModel GetMovieByTitle(string movieTitle)
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

        public MovieModel GetMovieById(int id)
        {
            var movie = this.movieAppDbContext.Movies
                                .Where(x => x.MovieId == id)
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

            if (movie.Title == null)
            {
                throw new ArgumentException();
            }

            var movieToAdd = this.mapper.Map<Movie>(movie);

            this.movieAppDbContext.Movies.Add(movieToAdd);
            this.movieAppDbContext.SaveChanges();
        }

        public void RemoveMovie(MovieModel movie)
        {
            // Attach/Detach / EntityState.Attach

            //this.movieAppDbContext.Entry<User>().Entity.

            if (movie == null)
            {
                throw new ArgumentException();
            }

            var movieToRemoveObject = this.movieAppDbContext.Movies
                .Where(x => x.MovieId == movie.MovieId)
                .FirstOrDefault();

            if (movieToRemoveObject == null)
            {
                throw new ArgumentNullException();
            }

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
            // possibly add to RoleService(AddRole)
            this.movieAppDbContext.Roles.Add(actorRole);
            this.movieAppDbContext.SaveChanges();
        }

        public double GetAverageRating(MovieModel movie)
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

        public IEnumerable<ReviewModel> GetMovieReviews(string title)
        {
            var movie = this.GetMovieByTitle(title);

            var reviews = this.movieAppDbContext.Reviews
                .Where(x => x.Movie.MovieId == movie.MovieId).ToList();

            var reviewsDto = mapper.Map<IList<ReviewModel>>(reviews);

            return reviewsDto;
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

        public IEnumerable<GenreModel> GetMovieGenres(MovieModel movie)
        {
            if (movie == null)
            {
                throw new ArgumentNullException("Movie cannot be null!");
            }
            
            var genres = movie.Genres;

            return genres;
           
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

        public IEnumerable<MovieModel> GetMoviesByTitleSearch(string pattern)
        {
            var moviesPoco = this.movieAppDbContext.Movies
                                .Where(x => x.Title.Contains(pattern))
                                .ToList();
            if (moviesPoco == null)
            {
                throw new ArgumentNullException("No movies found!");
            }
            
            var moviesDto = mapper.Map<IList<MovieModel>>(moviesPoco);
            return moviesDto;
        }

        public void PostMovieReview(ReviewModel review, int movieId, int userId)
        {
            if (review == null)
            {
                throw new ArgumentNullException();
            }
            
            var reviewFor = this.movieAppDbContext.Movies
                .Where(x => x.MovieId == movieId)
                .FirstOrDefault();

            if (reviewFor == null)
            {
                throw new ArgumentNullException("Movie not found");
            }

            var reviewFrom = this.movieAppDbContext.Users
                .Where(x => x.UserId == userId)
                .FirstOrDefault();

            if (reviewFrom == null)
            {
                throw new ArgumentNullException("User not found");
            }

            var reviewPoco = mapper.Map<Review>(review);

            reviewFor.Reviews.Add(reviewPoco);
            reviewFrom.Reviews.Add(reviewPoco);
            
            this.movieAppDbContext.SaveChanges();
        }
    }
}
