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

            return participantsModelsList;

            //var movies = this.movieAppDbContext.Movies.ProjectTo<MovieModel>();

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

            var actorRole = new Role()
            {
                MovieId = movie.MovieId,
                ParticipantId = participant.ParticipantId,
                RoleName = roleName
            };
            this.movieAppDbContext.Roles.Add(actorRole);
        }

    }
}
