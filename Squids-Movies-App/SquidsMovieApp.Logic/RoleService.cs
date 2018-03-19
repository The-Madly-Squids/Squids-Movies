using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SquidsMovieApp.Data.Context;
using SquidsMovieApp.Data.Models;
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

        public void AddRole(MovieModel movie, ParticipantModel participant, string roleName)
        {
            // use a factory?
            var actorRole = new Role()
            {
                MovieId = movie.MovieId,
                ParticipantId = participant.ParticipantId,
                RoleName = roleName
            };
            this.movieAppDbContext.Roles.Add(actorRole);
        }

        public IEnumerable<Role> ParticipantRolesPerMovie(ParticipantModel participant,
            MovieModel movie)
        {
            var pRoles = this.movieAppDbContext.Roles
                .Where(x => x.ParticipantId == participant.ParticipantId &&
                       x.MovieId == movie.MovieId);

            return pRoles;
        }
    }
}
