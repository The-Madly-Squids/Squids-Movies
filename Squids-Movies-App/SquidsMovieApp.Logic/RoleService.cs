using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SquidsMovieApp.Data.Context;
using SquidsMovieApp.Models;
using SquidsMovieApp.DTO;
using SquidsMovieApp.Logic.Contracts;

namespace SquidsMovieApp.Logic
{
    public class RoleService : IRoleService
    {
        private readonly IMovieAppDBContext movieAppDbContext;
        private readonly IMapper mapper;

        public RoleService(IMovieAppDBContext movieAppDbContext, IMapper mapper)
        {
            this.movieAppDbContext = movieAppDbContext;
            this.mapper = mapper;
        }

        public void AddRole(MovieModel movieModel, ParticipantModel participantModel,
            string roleName)
        {
            // use a factory?
            var movie = this.mapper.Map<Movie>(movieModel);
            var participant = this.mapper.Map<Participant>(participantModel);
            var actorRole = new Role()
            {
                Movie = movie,
                Participant = participant,
                RoleName = roleName
            };
            this.movieAppDbContext.Roles.Add(actorRole);
        }

        public IEnumerable<Role> ParticipantRolesPerMovie(ParticipantModel participant,
            MovieModel movie)
        {
            var pRoles = this.movieAppDbContext.Roles
                .Where(x => x.Participant.ParticipantId == participant.ParticipantId &&
                       x.Movie.MovieId == movie.MovieId);

            return pRoles;
        }
    }
}
